using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	public class BuildingDialog : Dialog {

		GameObject grid;
		Dictionary<BuildingType, UIButton> buttons;
		Dictionary<BuildingType, UILabel> costLabels;

		protected override void Start() {
			base.Start();
			grid = this.transform.FindChild("ScrollView/Grid").gameObject;

			buttons = new Dictionary<BuildingType, UIButton>();
			costLabels = new Dictionary<BuildingType, UILabel>();

			foreach(BuildingType type in Building.Types) {
				GameObject item = (GameObject) Instantiate(Resources.Load("BuildingOverlayItem"), Vector3.zero, Quaternion.identity);

				UILabel costLabel = item.transform.FindChild("Cost").gameObject.GetComponent<UILabel>();
				UIButton button = item.transform.FindChild("Hex").GetComponent<UIButton>();

				item.name = type.ToString();
				item.transform.SetParent(grid.transform);
				item.transform.localScale = Vector3.one;
				item.transform.FindChild("Title").gameObject.GetComponent<UILabel>().text = Strings.BuildingTitle(type);
				costLabel.text = Strings.BuildingYield(type);
				//item.transform.FindChild("Yield").gameObject.GetComponent<UILabel>().text = Strings.BuildingYield(type);
				item.transform.FindChild("Hex/Icon").gameObject.GetComponent<UI2DSprite>().sprite2D = GameAssets.GetSprite(type);

				EventDelegate del = new EventDelegate(this, "OnClick");
				del.parameters[0].value = type;
				EventDelegate.Add(button.onClick, del);

				buttons.Add(type, button);
				costLabels.Add(type, costLabel);
			}

			grid.GetComponent<UIGrid>().Reposition();

		}

		public override void FadeOut(bool dismissShade = true) {
			SelectionController.Changed();
			base.FadeOut(dismissShade);
		}

		void OnClick(BuildingType type) {
			if(UICamera.currentTouchID == -1) {
				SelectionController.selection.BuildingType = type;
				SelectionController.selection.Operation = Operation.Build;
				FadeOut();
			}
			else if(UICamera.currentTouchID == -2) {
				FadeOut(false);
				HelpOverlay.Show(type);
			}
		}

		void FixedUpdate() {
			if(Visible) {
				for(int i = 0; i < grid.transform.childCount; i++) {
					Transform child = grid.transform.GetChild(i);
					BuildingType type = (BuildingType) Enum.Parse(typeof(BuildingType), child.name);
					UIButton button = buttons[type];
					UILabel costLabel = costLabels[type];

					if(Game.CurrentCity.CanBuild(type)) {
						button.isEnabled = true;
						costLabel.text = Strings.BuildingYield(type);
					}
					else {
						button.isEnabled = false;
						costLabel.text = Strings.CanNotBuildMessage(Game.CurrentCity, type);
					}

				}
			}
		}
	}
}