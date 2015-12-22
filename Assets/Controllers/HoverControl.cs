using UnityEngine;
using Lords;

public class HoverControl : MonoBehaviour {
	Vector3 offscreen = new Vector3(0, 0, -100);

	void Start () {
		gameObject.transform.position = offscreen;
	}

	void Update () {
		Hex hoverPointHex = Hex.WorldToHex(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		
		if(Game.CurrentCity.Tiles.ContainsKey(hoverPointHex)) {
			gameObject.transform.position = Hex.HexToWorld(hoverPointHex);
		}
		else {
			gameObject.transform.position = offscreen;
		}
	}
}
