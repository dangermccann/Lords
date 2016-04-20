using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using Lords;
using UnityEngine.Cloud.Analytics;

public class GameController : MonoBehaviour {
	const float AUTOSAVE_INTERVAL = 120f;

	bool clickStarted = false;
	GameObject mapRoot;
	float lastSaveTime;
	float levelLoadTime = 0;
	EffectsController effects;

	// Use this for initialization
	void Start () {
		mapRoot = GameObject.Find("Map");
		LoadLevel(Game.CurrentLevel ?? Levels.OldMilddleton);
		effects = GameObject.Find("EffectsController").GetComponent<EffectsController>();
	}

	public void LoadLevel(Level level) {
		UnloadLevel();

		SavedGame saved = Game.Load();
		if(saved != null) {
			SavedCity savedCity = saved.FindCity(level.name);
			if(savedCity != null) {
				Debug.Log("Found saved version of " + level.name);
				Game.CurrentCity = City.LoadCity(savedCity);
			}
			else {
				Game.CurrentCity = new City(level);
			}
		}
		else {
			Game.CurrentCity = new City(level);
		}

		Game.CurrentLevel = level;
		
		MapGenerator generator = new MapGenerator();
		Map map = generator.GenerateMap(level.mapConfiguration.Radius, level.mapConfiguration.TileConfiguration, 
		                                level.mapConfiguration.Seed, level.mapConfiguration.PerlinOctive);
		Game.CurrentCity.SetMap(map);
		
		foreach(Tile tile in Game.CurrentCity.Tiles.Values) {
			GameObject go = GameAssets.MakeTile(mapRoot.transform, tile);

			tile.TypeChanged += (Tile tt) => {
				GameAssets.RedrawTile(go, tt);
			};
			tile.BuildingChanged += (Tile tt) => {
				GameAssets.MakeBuilding(go, tt.Building);

				// If this tile is the selected tile, trigger a changed event so we redraw
				if(SelectionController.selection.Tile == tt) {
					SelectionController.selection.Tile = tt;
				}
			};
		}

		Game.Resume();
		SelectionController.Reset();

		lastSaveTime = Time.time;
		levelLoadTime = Time.time;
	}

	public void UnloadLevel() {
		if(Game.CurrentCity != null) {
			foreach(Tile tile in Game.CurrentCity.Tiles.Values) {
				tile.RemoveAllListeners();
			}
		}

		// TODO: is this right?
		while (mapRoot.transform.childCount > 0) {
			GameObject.DestroyImmediate(mapRoot.transform.GetChild(0).gameObject);
		}
	}

	void Update () {
		ProcessInputs();
		UpdateGame();
	}

	void ProcessInputs() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(Dialog.current != null) {
				if(Dialog.current.IsDismissible()) {
					Dialog.current.FadeOut();
				}
			}
			else {
				GameObject.Find("MenuOverlay").GetComponent<MenuOverlay>().FadeIn();
			}
		}

		// shortcut keys
		if(Dialog.current == null) {
			if(Input.GetKey(KeyCode.F1)) {
				HelpOverlay.Show(BuildingType.Villa);
			}

			if(Input.GetKey(KeyCode.F2)) {
				GameObject.Find("TradeOverlay").GetComponent<TradeOverlay>().FadeIn();
			}

			if(Input.GetKey(KeyCode.B)) {
				GameObject.Find("BuildingOverlay").GetComponent<BuildingDialog>().FadeIn();
			}

			if(Input.GetKey(KeyCode.T)) {
				SelectionController.selection.Operation = Operation.Destroy;
			}

			if(Input.GetKey(KeyCode.I)) {
				SelectionController.selection.Operation = Operation.Info;
				SelectionController.selection.Tile = null;
			}
		}

		if(Debug.isDebugBuild) {
			if(ProcessEasterEggs()) {
				return;
			}
		}

		if(HoverControl.IsOverUI()) {
			return;
		}

		if(Input.GetMouseButtonDown(0)) {
			clickStarted = true;
		}

		if(CameraControl.IsDragging) {
			clickStarted = false;
		}

		if(clickStarted && Input.GetMouseButtonUp(0)) {
			Hex hex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			if(Game.CurrentCity.Tiles.ContainsKey(hex)) {
				Tile tile = Game.CurrentCity.Tiles[hex];
				Debug.Log(TileDebugMessage(tile));

				if(SelectionController.selection.Operation == Operation.Build) {
					if(tile.CanBuildOn()) {
						if(Game.CurrentCity.CanBuild(SelectionController.selection.BuildingType)) {
							if(tile.Building != null) {
								Game.CurrentCity.RemoveBuilding(tile.Building);
							}

							Building building = new Building(tile, SelectionController.selection.BuildingType);
							Game.CurrentCity.AddBuilding(building);

							effects.Place();
						}
						else {
							Debug.Log("Insufficient funds or raw materials");
						}
					}
					else {
						Debug.Log("Can't build on this tile");
					}
				}
				else if(SelectionController.selection.Operation == Operation.Destroy) {
					if(tile.Building != null) {
						Game.CurrentCity.RemoveBuilding(tile.Building);
						tile.Building = null;

						effects.Place();
					}
					else {
						Debug.Log("Nothing to destroy");
					}
				}
				else if(SelectionController.selection.Operation == Operation.Info) {
					SelectionController.selection.Tile = tile;

					if(tile.Building != null) {
						if(Building.CanUpgrade(tile.Building.Type)) {
							UpgradeOverlay overlay = GameObject.Find("UpgradeOverlay").GetComponent<UpgradeOverlay>();
							overlay.SetBuilding(tile.Building);
						}
						else if(tile.Building.Type == BuildingType.Trading_Post) {
							GameObject.Find("TradeOverlay").GetComponent<TradeOverlay>().FadeIn();
						}
					}
				}
			}
			else {
				Debug.LogWarning(hex + " is not a tile");
			}
		}

		// Right-click for help
		if(Input.GetMouseButtonDown(1)) {
			Hex hex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			
			if(Game.CurrentCity.Tiles.ContainsKey(hex)) {
				Tile tile = Game.CurrentCity.Tiles[hex];
				if(tile.Building != null) {
					HelpOverlay.Show(tile.Building.Type);
				}
			}
		}
	}

	void UpdateGame() {
		Game.CurrentCity.UpdateEverything(Time.fixedDeltaTime);

		// It takes a few ticks to correctly calculate score
		float elapsedSinceLoad = Time.time - levelLoadTime;

		if(elapsedSinceLoad >= 1 && Game.CurrentCity.MeetsVictoryConditions()) {
			if(Dialog.current != null) {
				Dialog.current.FadeOut(false);
			}

			VictoryOverlay.Show(VictoryOverlay.Mode.Victory);

			Game.Save();
			Game.Pause();

			UnityAnalytics.CustomEvent("victory", new Dictionary<string, object> {
				{ "level", Game.CurrentLevel.name },
				{ "elapsedTime", Game.CurrentCity.ElapsedTime },
			});
		}

		if(elapsedSinceLoad >= 1 && Game.CurrentCity.MeetsFailureConditions()) {
			if(Dialog.current != null) {
				Dialog.current.FadeOut(false);
			}

			VictoryOverlay.FailureReason reason = VictoryOverlay.FailureReason.TimeLimit;
			float actualValue = Game.CurrentCity.ElapsedTime;

			if(Game.CurrentCity.Score.Happiness <= Game.CurrentLevel.failureConditions.Happiness) {
				reason = VictoryOverlay.FailureReason.Happiness;
				actualValue = Game.CurrentCity.Score.Happiness;
			}

			if(Game.CurrentCity.Score.Culture <= Game.CurrentLevel.failureConditions.Culture) {
				reason = VictoryOverlay.FailureReason.Culture;
				actualValue = Game.CurrentCity.Score.Culture;
			}

			if(Game.CurrentCity.Score.Prosperity <= Game.CurrentLevel.failureConditions.Prosperity) {
				reason = VictoryOverlay.FailureReason.Prosperity;
				actualValue = Game.CurrentCity.Score.Prosperity;
			}

			VictoryOverlay.Show(VictoryOverlay.Mode.Failure, reason);

			Game.ResetCurrentCity();
			Game.Pause();

			Debug.Log("Level Failed because " + reason.ToString() + " is " + actualValue);

			UnityAnalytics.CustomEvent("failure", new Dictionary<string, object> {
				{ "level", Game.CurrentLevel.name },
				{ "reason", reason.ToString() },
				{ "elapsedTime", Game.CurrentCity.ElapsedTime },
				{ "actualValue", actualValue },
			});
		}

		if(Time.time - lastSaveTime > AUTOSAVE_INTERVAL) {
			Game.Save();
			Debug.Log("Auto-saving game");
			lastSaveTime = Time.time;
		}
	}

	private string TileDebugMessage(Tile tile) {
		string msg = tile.Position.ToString() + " " + tile.Type + "; ";;

		if(tile.Building != null) {
			msg += tile.Building.Type + "; pop: [" + Game.CurrentCity.PopulationMultiplier(tile.Building) + "]\n";
			msg += "yield: [" + Game.CurrentCity.EffectiveYield(tile.Building) + "]\n";
			msg += "tile:  [" + Game.CurrentCity.TileModifier(tile.Building) + "]\n";
			msg += "near:  [" + Game.CurrentCity.NearbyBuildingModifier(tile.Building) + "]";
		}
		return msg;
	}

	private bool ProcessEasterEggs() {

		if(Dialog.current != null) {
			return false;
		}

		if(Input.GetKeyDown(KeyCode.V)) {
			VictoryOverlay.Show(VictoryOverlay.Mode.Victory);
			Game.Pause();
			return true;
		}

		if(Input.GetKeyDown(KeyCode.F)) {
			VictoryOverlay.Show(VictoryOverlay.Mode.Failure, VictoryOverlay.FailureReason.Happiness);
			Game.Pause();
			return true;
		}

		if(Input.GetKeyDown(KeyCode.Alpha0)) {
			LoadLevel(Levels.All[0]);
			return true;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			LoadLevel(Levels.All[1]);
			return true;
		}

		return false;
	}

}
