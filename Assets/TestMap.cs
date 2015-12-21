using UnityEngine;
using System;
using Lords;

public class TestMap : MonoBehaviour {
	public GameObject hoverTile;
	City city;
	BuildingType currentBuildingType = BuildingType.Villa;

	// Use this for initialization
	void Start () {
		if(hoverTile == null) {
			hoverTile = GameObject.Find("hover-hex");
		}
		hoverTile.SetActive(false);

		int radius = 4;
		city = new City(radius);


		foreach(Tile tile in city.Tiles.Values) {
			GameObject go = GameAssets.MakeTile(GameObject.Find("Map").transform, tile);
			tile.TypeChanged += (Tile tt) => {
				GameAssets.RedrawTile(go, tt);
			};
			tile.BuildingChanged += (Tile tt) => {
				GameAssets.MakeBuilding(go, tt.Building);
			};
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Hex hex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			Debug.Log(String.Format("({0}, {1})", hex.q, hex.r));

			Tile tile = city.Tiles[hex];
			Building building = new Building(tile, currentBuildingType);
			tile.Building = building;
			tile.Type = Building.BuildingTileMap[building.Type];
		}

		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			currentBuildingType = BuildingType.Villa;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			currentBuildingType = BuildingType.Slums;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			currentBuildingType = BuildingType.Church;
		}



		Hex hoverPointHex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		if(city.Tiles.ContainsKey(hoverPointHex)) {
			hoverTile.transform.position = Hex.HexToWorld(hoverPointHex);
			hoverTile.SetActive(true);
		}
		else {
			hoverTile.SetActive(false);
		}
	}



}
