using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using Lords;

public class GameController : MonoBehaviour {
	float lastScoreUpdate = 0;
	public float updateInterval = 5f;
	bool clickStarted = false;

	// Use this for initialization
	void Start () {
		MapConfiguration mapConfig = Maps.Tundra;
		Game.CurrentCity = new City(mapConfig.Radius);

		MapGenerator generator = new MapGenerator();
		Map map = generator.GenerateMap(mapConfig.Radius, mapConfig.TileConfiguration, mapConfig.Seed);

		foreach(Tile tile in Game.CurrentCity.Tiles.Values) {
			tile.Type = map.GetTileTypeAt(tile.Position);

			GameObject go = GameAssets.MakeTile(GameObject.Find("Map").transform, tile);

			tile.TypeChanged += (Tile tt) => {
				GameAssets.RedrawTile(go, tt);
			};
			tile.BuildingChanged += (Tile tt) => {
				GameAssets.MakeBuilding(go, tt.Building);
			};
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
		}
	}

	void FixedUpdate() {
		Game.CurrentCity.UpdateEverything(Time.fixedDeltaTime);

		if(Time.time - lastScoreUpdate > updateInterval) {
			lastScoreUpdate = Time.time;

			Debug.Log(Game.CurrentCity.Primatives.ToString());
			Debug.Log(Game.CurrentCity.Score.ToString());
			Debug.Log("funds: " + Game.CurrentCity.Funds + " materials: " + Game.CurrentCity.RawMaterials);
		}
	}

}
