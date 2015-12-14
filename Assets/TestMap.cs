using UnityEngine;
using System;
using Lords;

public class TestMap : MonoBehaviour {
	public GameObject hoverTile;
	City city;

	// Use this for initialization
	void Start () {
		if(hoverTile == null) {
			hoverTile = GameObject.Find("hover-hex");
		}
		hoverTile.SetActive(false);

		int radius = 4;
		city = new City(radius);


		foreach(Tile tile in city.Tiles.Values) {
			GameObject go = (GameObject) Instantiate(Resources.Load("empty-hex"), tile.Position.ToWorld(), Quaternion.identity);
			go.transform.parent = GameObject.Find("Map").transform;
			go.name = String.Format("Tile ({0}, {1})", tile.Position.q, tile.Position.r);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Hex hex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			Debug.Log(String.Format("({0}, {1})", hex.q, hex.r));
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
