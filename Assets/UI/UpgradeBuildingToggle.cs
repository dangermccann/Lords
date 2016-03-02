using System;
using UnityEngine;

namespace Lords {
	public class UpgradeBuildingToggle : MonoBehaviour {

		BuildingType type;
		UIToggle toggle;
		UIButton button;

		void Start() {
			toggle = GetComponent<UIToggle>();
			button = GetComponent<UIButton>();

			EventDelegate.Add(toggle.onChange, OnChanged);
		}

		void OnChanged() {
			transform.FindChild("Cost").gameObject.GetComponent<UILabel>().alpha = toggle.value ? 0 : 1;
		}

		public BuildingType GetBuildingType() {
			return type;
		}

		public void SetBuildingType(BuildingType _type) {
			this.type = _type;

			transform.FindChild("Preview/Icon").gameObject.GetComponent<UI2DSprite>().sprite2D = GameAssets.GetSprite(type);
			transform.FindChild("Title").gameObject.GetComponent<UILabel>().text = Strings.BuildingTitle(type);
			transform.FindChild("Yield").gameObject.GetComponent<UILabel>().text = "Yield: " + Strings.BuildingYield(type);
			transform.FindChild("Cost").gameObject.GetComponent<UILabel>().text = "Cost: " + Strings.BuildingCost(type);
		}

		public void Select() {
			toggle.value = true;
			Enabled = true;
		}

		public bool Selected {
			get {
				return toggle.value;
			}
		}

		public bool Enabled {
			get {
				return button.isEnabled;
			}
			set {
				button.isEnabled = value;
			}
		}

	}
}
