using UnityEngine;
using Lords;

public class HoverControl : MonoBehaviour {
	Vector3 offscreen = new Vector3(0, 0, -100);
	SpriteRenderer sprite;

	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		gameObject.transform.position = offscreen;
		SelectionController.SelectionChanged += SelectionChanged;

		SelectionChanged(SelectionController.selection);
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

		if(IsOverUI() || tile == null || CameraControl.IsDragging) {
			Screen.showCursor = true;
			gameObject.transform.position = offscreen;
		}
		else {
			if(SelectionController.selection.Operation == Operation.Build) {
				Screen.showCursor = true;

				if(tile.CanBuildOn()) {
					gameObject.transform.position = Hex.HexToWorld(hoverPointHex);
				}
				else {
					gameObject.transform.position = offscreen;
				}
			}
			else {
				Screen.showCursor = false;
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
			}
		}
	}

	void SelectionChanged(Selection selection) {
		if(selection.Operation == Operation.Build) {
			sprite.sprite = GameAssets.GetSprite(selection.BuildingType);
		}
		else {
			sprite.sprite = GameAssets.GetShovel();
		}
	}

	public static bool IsOverUI() { 
		return UICamera.hoveredObject != null && UICamera.hoveredObject.name != "UI Root";
	}
}
