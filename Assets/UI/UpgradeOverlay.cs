using System;
using System.Collections.Generic;
using UnityEngine;


namespace Lords {
	public class UpgradeOverlay : Dialog {
		UpgradeBuildingToggle[] toggles;
		BuildingType[] types;
		Building currentBuilding;

		protected override void Start() {
			base.Start();

			toggles = new UpgradeBuildingToggle[3];
			types = new BuildingType[3];

			for(int i = 0; i < toggles.Length; i++) {
				toggles[i] = transform.FindChild("Background/UpgradeBuildingToggle-" + i).gameObject.GetComponent<UpgradeBuildingToggle>();
				EventDelegate.Add(toggles[i].GetComponent<UIToggle>().onChange, OnChanged);
			}
		}

		void Update() {
			UpdateEnabledButtons();
		}

		void UpdateEnabledButtons() {
			if(Visible && currentBuilding != null) {
				for(int i = 0; i < types.Length; i++) {
					if(!toggles[i].Selected) {
						toggles[i].Enabled = Game.CurrentCity.CanBuild(types[i]);
					}
				}
			}
		}

		void OnChanged() {
			if(currentBuilding == null) {
				return;
			}

			for(int i = 0; i < toggles.Length; i++) {
				if(UIToggle.current.name == toggles[i].name) {
					if(UIToggle.current.value) {
						Debug.Log("Selected " + types[i]);
						Game.CurrentCity.RemoveBuilding(currentBuilding);
						currentBuilding.ChangeType(types[i]);
						Game.CurrentCity.AddBuilding(currentBuilding);
					}
					else {
						Debug.Log("Deselected " + types[i]);
					}
				}
			}
		}

		public void SetBuilding(Building building) {
			this.currentBuilding = null;	// don't trigger changed events while binding data

			BuildingType baseType = Building.FindUpgradeBaseType(building.Type);
			toggles[0].SetBuildingType(baseType);
			types[0] = baseType;

			if(building.Type == baseType) {
				toggles[0].Select();
			}

			List<BuildingType> upgrades = Building.Upgrades[baseType];

			for(int i = 1; i < toggles.Length; i++) {
				if(i > upgrades.Count) {
					toggles[i].gameObject.SetActive(false);
					types[i] = BuildingType.Villa;	// because it can't be null
				}
				else {
					BuildingType t = upgrades[i-1];

					toggles[i].gameObject.SetActive(true);
					toggles[i].SetBuildingType(t);
					types[i] = t;

					if(building.Type == t) {
						toggles[i].Select();
					}
				}
			}

			UpdateEnabledButtons();

			this.currentBuilding = building;
		}

		public override void FadeIn() {
			base.FadeIn();
		}

		public override void FadeOut(bool dismissShade = true) {
			base.FadeOut(dismissShade);
			currentBuilding = null;
		}
	}
}