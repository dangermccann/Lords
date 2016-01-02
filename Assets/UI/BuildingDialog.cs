using UnityEngine;
using System;

namespace Lords {
	public class BuildingDialog : MonoBehaviour {

		Dialog dialog;
		GameObject grid;

		void Start () {
			grid = this.transform.FindChild("ScrollView/Grid").gameObject;
			dialog = GetComponent<Dialog>();

			foreach(BuildingType type in Building.Types) {
				GameObject item = (GameObject) Instantiate(Resources.Load("BuildingOverlayItem"), Vector3.zero, Quaternion.identity);
				item.name = type.ToString();
				item.transform.SetParent(grid.transform);
				item.transform.localScale = Vector3.one;
				item.transform.FindChild("Title").gameObject.GetComponent<UILabel>().text = Strings.BuildingTitle(type);
				item.transform.FindChild("Cost").gameObject.GetComponent<UILabel>().text = Strings.BuildingCost(type);
				//item.transform.FindChild("Yield").gameObject.GetComponent<UILabel>().text = Strings.BuildingYield(type);
				item.transform.FindChild("Hex/Icon").gameObject.GetComponent<UI2DSprite>().sprite2D = GameAssets.GetSprite(type);

				EventDelegate del = new EventDelegate(this, "OnClick");
				del.parameters[0].value = type;
				EventDelegate.Add(item.transform.FindChild("Hex").GetComponent<UIButton>().onClick, del);
			}

			grid.GetComponent<UIGrid>().Reposition();

		}

		void OnClick(BuildingType type) {
			SelectionController.selection.BuildingType = type;
			SelectionController.selection.Operation = Operation.Build;
			dialog.FadeOut();
		}

		void FixedUpdate() {
			if(dialog.Visible) {
				for(int i = 0; i < grid.transform.childCount; i++) {
					Transform child = grid.transform.GetChild(i);
					BuildingType type = (BuildingType) Enum.Parse(typeof(BuildingType), child.name);
					UIButton button = child.FindChild("Hex").GetComponent<UIButton>();

					if(Game.CurrentCity.CanBuild(type)) {
						button.isEnabled = true;
					}
					else {
						button.isEnabled = false;
					}

				}
			}
		}
	}
}