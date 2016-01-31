using UnityEngine;
using System;

namespace Lords {
	public class HelpOverlay : Dialog {

		GameObject list;
		UILabel title, yields, details;
		UI2DSprite icon;

		BuildingType selectedBuildingType = BuildingType.Villa;
		public BuildingType SelectedBuildingType {
			get { return selectedBuildingType; }
			set { 
				selectedBuildingType = value;
				FindListItem(selectedBuildingType).value = true;
			}
		}

		protected override void Start() {
			base.Start();
			list = this.transform.FindChild("Content/Grid").gameObject;
			title = this.transform.FindChild("Content/Title").gameObject.GetComponent<UILabel>();
			yields = this.transform.FindChild("Content/Yields").gameObject.GetComponent<UILabel>();
			details = this.transform.FindChild("Content/Details").gameObject.GetComponent<UILabel>();
			icon = this.transform.FindChild("Content/Graphic/Icon").gameObject.GetComponent<UI2DSprite>();
			Redraw();
		}

		public override void FadeIn() {
			base.FadeIn();
		}


		void Redraw() {
			foreach (Transform child in list.transform) {
				GameObject.Destroy(child.gameObject);
			}

			foreach(BuildingType type in Building.Types) {
				GameObject item = (GameObject) Instantiate(Resources.Load("HelpListitem"), Vector3.zero, Quaternion.identity);
				item.name = type.ToString();
				item.transform.SetParent(list.transform);
				item.transform.localScale = Vector3.one;
				item.transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = Strings.BuildingTitle(type);
				item.transform.FindChild("Selected/Label").gameObject.GetComponent<UILabel>().text = Strings.BuildingTitle(type);
				if(SelectedBuildingType == type) {
					item.GetComponent<UIToggle>().value = true;
				}

				EventDelegate.Add(item.GetComponent<UIToggle>().onChange, OnToggleChanged);
			}
		}

		void OnToggleChanged() {
			if(UIToggle.current.value) {
				selectedBuildingType = (BuildingType) Enum.Parse(typeof(BuildingType), UIToggle.current.name);
				RedrawContents();
			}
		}

		void RedrawContents() {
			Debug.Log("Redrawing help contents for " + SelectedBuildingType.ToString());
			title.text = Strings.BuildingTitle(SelectedBuildingType);
			yields.text = Strings.BuildingYieldLong(SelectedBuildingType);
			details.text = "Cost:  " + Strings.BuildingCost(SelectedBuildingType) + "\n\n";
			details.text += Strings.BuildingHelp(SelectedBuildingType);
			icon.sprite2D = GameAssets.GetSprite(SelectedBuildingType);
		}

		UIToggle FindListItem(BuildingType type) {
			return list.transform.FindChild(type.ToString()).gameObject.GetComponent<UIToggle>();
		}


	}
}
