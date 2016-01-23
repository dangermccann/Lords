﻿using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using Lords;

public class GameController : MonoBehaviour {
	const float AUTOSAVE_INTERVAL = 120f;

	bool clickStarted = false;
	GameObject mapRoot;
	float lastSaveTime;

	// Use this for initialization
	void Start () {
		mapRoot = GameObject.Find("Map");
		LoadLevel(Game.CurrentLevel ?? Levels.PortHenry);
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
		Map map = generator.GenerateMap(level.mapConfiguration.Radius, level.mapConfiguration.TileConfiguration, level.mapConfiguration.Seed);
		Game.CurrentCity.SetMap(map);
		
		foreach(Tile tile in Game.CurrentCity.Tiles.Values) {
			GameObject go = GameAssets.MakeTile(mapRoot.transform, tile);

			tile.TypeChanged += (Tile tt) => {
				GameAssets.RedrawTile(go, tt);
			};
			tile.BuildingChanged += (Tile tt) => {
				GameAssets.MakeBuilding(go, tt.Building);
			};
		}

		Game.Resume();
		SelectionController.Reset();

		lastSaveTime = Time.time;
	}

	public void UnloadLevel() {
		if(Game.CurrentCity != null) {
			foreach(Tile tile in Game.CurrentCity.Tiles.Values) {
				tile.RemoveAllListeners();
			}
		}

		foreach (Transform child in mapRoot.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	void Update () {
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

		if(ProcessEasterEggs()) {
			return;
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
			Debug.Log(String.Format("({0}, {1})", hex.q, hex.r));

			if(Game.CurrentCity.Tiles.ContainsKey(hex)) {
				Tile tile = Game.CurrentCity.Tiles[hex];

				if(SelectionController.selection.Operation == Operation.Build) {
					if(tile.CanBuildOn()) {
						if(Game.CurrentCity.CanBuild(SelectionController.selection.BuildingType)) {
							if(tile.Building != null) {
								Game.CurrentCity.RemoveBuilding(tile.Building);
							}

							Building building = new Building(tile, SelectionController.selection.BuildingType);
							Game.CurrentCity.AddBuilding(building);
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
					}
					else {
						Debug.Log("Nothing to destroy");
					}
				}
				else if(SelectionController.selection.Operation == Operation.Info) {
					SelectionController.selection.Tile = tile;
				}
			}
			else {
				Debug.Log("Not a tile");
			}
		}
	}

	void FixedUpdate() {
		Game.CurrentCity.UpdateEverything(Time.fixedDeltaTime);

		if(Input.GetKeyDown(KeyCode.V) || Game.CurrentCity.MeetsVictoryConditions()) {
			GameObject.Find("VictoryOverlay").GetComponent<VictoryOverlay>().FadeIn();
			Debug.Log("You win!");
			Game.Save();
			Game.Pause();
		}

		if(Game.CurrentCity.MeetsFailureConditions()) {
			Debug.Log("You lose");
			Game.Save();
			Game.Pause();
		}

		if(Time.time - lastSaveTime > AUTOSAVE_INTERVAL) {
			Game.Save();
			Debug.Log("Auto-saving game");
			lastSaveTime = Time.time;
		}
	}

	private bool ProcessEasterEggs() {

		if(Input.GetKeyDown(KeyCode.V)) {
			GameObject.Find("VictoryOverlay").GetComponent<VictoryOverlay>().FadeIn();
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
