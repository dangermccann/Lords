using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using Lords;

public class GameController : MonoBehaviour {
	bool clickStarted = false;
	GameObject mapRoot;

	// Use this for initialization
	void Start () {
		mapRoot = GameObject.Find("Map");
		LoadLevel(Levels.Tutorial);
	}

	public void LoadLevel(Level level) {
		UnloadLevel();

		Game.CurrentCity = new City(level.mapConfiguration.Radius, level.initialFunds, level.initialRawMaterials);
		Game.CurrentLevel = level;
		
		MapGenerator generator = new MapGenerator();
		Map map = generator.GenerateMap(level.mapConfiguration.Radius, level.mapConfiguration.TileConfiguration, level.mapConfiguration.Seed);
		
		foreach(Tile tile in Game.CurrentCity.Tiles.Values) {
			tile.Type = map.GetTileTypeAt(tile.Position);
			
			GameObject go = GameAssets.MakeTile(mapRoot.transform, tile);

			tile.TypeChanged += (Tile tt) => {
				GameAssets.RedrawTile(go, tt);
			};
			tile.BuildingChanged += (Tile tt) => {
				GameAssets.MakeBuilding(go, tt.Building);
			};
		}
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
							tile.Building = building;
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

		if(Game.CurrentCity.MeetsVictoryConditions(Game.CurrentLevel.victoryConditions)) {
			Debug.Log("You win!");
		}
		if(Game.CurrentCity.MeetsFailureConditions()) {
			Debug.Log("You lose");
		}
	}

}
