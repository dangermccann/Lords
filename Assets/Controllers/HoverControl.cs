using UnityEngine;
using Lords;

public class HoverControl : MonoBehaviour {
	static Vector3 OFFSCREEN = new Vector3(0, 0, -100);

	SpriteRenderer sprite;
	public GameObject shovel;

	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		gameObject.transform.position = OFFSCREEN;
		shovel.transform.position = OFFSCREEN;

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
			gameObject.transform.position = OFFSCREEN;
			shovel.transform.position = OFFSCREEN;
		}
		else {
			if(SelectionController.selection.Operation == Operation.Build) {
				Screen.showCursor = true;
				shovel.transform.position = OFFSCREEN;

				if(tile.CanBuildOn()) {
					gameObject.transform.position = Hex.HexToWorld(hoverPointHex);
				}
				else {
					gameObject.transform.position = OFFSCREEN;
				}
			}
			else if(SelectionController.selection.Operation == Operation.Destroy) {
				Screen.showCursor = false;
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				shovel.transform.position = new Vector3(pos.x, pos.y, 0);
				gameObject.transform.position = Hex.HexToWorld(hoverPointHex);
			}
			else if(SelectionController.selection.Operation == Operation.Info) {
				Screen.showCursor = true;
				shovel.transform.position = OFFSCREEN;
				gameObject.transform.position = OFFSCREEN;
			}
		}
	}

	void SelectionChanged(Selection selection) {
		if(selection.Operation == Operation.Build) {
			sprite.sprite = GameAssets.GetSprite(selection.BuildingType);
		}
		else {
			sprite.sprite = GameAssets.GetDeleteTile();
		}
	}

	public static bool IsOverUI() { 
		return UICamera.hoveredObject != null && UICamera.hoveredObject.name != "UI Root";
	}
}
