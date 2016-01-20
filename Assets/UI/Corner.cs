using System;
using UnityEngine;

namespace Lords {
	public class Corner : MonoBehaviour {
		UI2DSprite icon;
		UILabel title, yield, details;
		UIToggle build, destroy, info;

		void Start() {
			SelectionController.SelectionChanged += SelectionChanged;
			icon = transform.FindChild("SelectedHex/Icon").GetComponent<UI2DSprite>();
			title = transform.FindChild("SelectedTitle").GetComponent<UILabel>();
			yield = transform.FindChild("SelectedYield").GetComponent<UILabel>();
			details = transform.FindChild("SelectedDetails").GetComponent<UILabel>();

			build = transform.FindChild("CreateButton").GetComponent<UIToggle>();
			destroy = transform.FindChild("DeleteButton").GetComponent<UIToggle>();
			info = transform.FindChild("InfoButton").GetComponent<UIToggle>();

			Redraw();
		}

		void Update() {
			if(SelectionController.selection.Operation == Operation.Info) {
				Tile tile = SelectionController.selection.Tile;
				if(tile != null) {
					if(tile.Building != null) {
						details.text = Strings.BuildingNotes(Game.CurrentCity, tile.Building);
					}
				}
			}
		}

		void OnDestroy() {
			SelectionController.SelectionChanged -= SelectionChanged;
		}

		void SelectionChanged(Selection selection) {
			Redraw();
		}

		void Redraw() {
			BuildingType type = SelectionController.selection.BuildingType;
			if(SelectionController.selection.Operation == Operation.Build) {
				build.value = true;

				icon.sprite2D = GameAssets.GetSprite(type);
				title.text = Strings.BuildingTitle(type);
				yield.text = "Cost: " + Strings.BuildingCost(type) + "\nYield: " + Strings.BuildingYield(type);
				details.text = "";
			}
			else if(SelectionController.selection.Operation == Operation.Destroy) {
				destroy.value = true;

				icon.sprite2D = null;
				title.text = "";
				yield.text = "Select a building to destroy";
				details.text = "";
			}
			else if(SelectionController.selection.Operation == Operation.Info) {
				info.value = true;

				Tile tile = SelectionController.selection.Tile;
				if(tile == null) {
					icon.sprite2D = null;
					title.text = "";
					yield.text = "Select a tile for info";
					details.text = "";
				}
				else { 
					if(tile.Building != null) {
						icon.sprite2D = GameAssets.GetSprite(tile.Building.Type);
						title.text = Strings.BuildingTitle(tile.Building.Type);
						yield.text = Strings.BuildingYield(tile.Building.Type) + "\n" + Strings.BuildingModifier(tile);
						details.text = Strings.BuildingNotes(Game.CurrentCity, tile.Building);
					}
					else {
						icon.sprite2D = GameAssets.GetSprite(tile.Type);
						title.text = Strings.TileTitle(tile.Type);
						yield.text = Strings.TileDescription(tile.Type);
						details.text = "";
					}
				}
			}
		}

		public void SelectionDestroy() {
			SelectionController.selection.Operation = Operation.Destroy;
		}

		public void SeletionInfo() {
			SelectionController.selection.Operation = Operation.Info;
			SelectionController.selection.Tile = null;
		}
	}
}

