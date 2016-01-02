using UnityEngine;
using Lords;

public class HoverControl : MonoBehaviour {
	Vector3 offscreen = new Vector3(0, 0, -100);

	void Start () {
		gameObject.transform.position = offscreen;
	}

	void Update () {
		Hex hoverPointHex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		if(hoverPointHex == null) {	// why does this happen?
			return;
		}

		Tile tile = null;
		if(Game.CurrentCity.Tiles.ContainsKey(hoverPointHex)) {
			tile = Game.CurrentCity.Tiles[hoverPointHex];
		}

		if(tile != null && tile.CanBuildOn()) {
			gameObject.transform.position = Hex.HexToWorld(hoverPointHex);
		}
		else {
			gameObject.transform.position = offscreen;
		}
	}

	public static bool IsOverUI() { 
		return UICamera.hoveredObject != null && UICamera.hoveredObject.name != "UI Root";
	}
}
