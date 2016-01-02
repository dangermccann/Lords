using System;
using UnityEngine;

namespace Lords {
	public class Corner : MonoBehaviour {
		void Start() {
			SelectionController.SelectionChanged += SelectionChanged;
			Redraw();
		}

		void SelectionChanged(Selection selection) {
			Redraw();
		}

		void Redraw() {
			BuildingType type = SelectionController.selection.BuildingType;
			if(SelectionController.selection.Operation == Operation.Build) {
				transform.FindChild("SelectedHex/Icon").GetComponent<UI2DSprite>().sprite2D = GameAssets.GetSprite(type);
				transform.FindChild("SelectedTitle").GetComponent<UILabel>().text = Strings.BuildingTitle(type);
				transform.FindChild("SelectedYield").GetComponent<UILabel>().text = Strings.BuildingCost(type) + Strings.BuildingYield(type);
			}

		}

		public void SelectionDestroy() {
			SelectionController.selection.Operation = Operation.Destroy;
		}
	}
}

