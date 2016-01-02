using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using Lords;

public class TestMap : MonoBehaviour {
	float lastScoreUpdate = 0;
	public float updateInterval = 5f;

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
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			SelectionController.selection.BuildingType = BuildingType.Villa;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			SelectionController.selection.BuildingType = BuildingType.Slums;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			SelectionController.selection.BuildingType = BuildingType.Church;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)) {
			SelectionController.selection.BuildingType = BuildingType.Wheat_Farm;
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)) {
			SelectionController.selection.BuildingType = BuildingType.Tavern;
		}


		if(HoverControl.IsOverUI()) {
			return;
		}

		if(Input.GetMouseButtonDown(0)) {
			Hex hex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			Debug.Log(String.Format("({0}, {1})", hex.q, hex.r));

			Tile tile = Game.CurrentCity.Tiles[hex];

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
