using UnityEngine;
using System;

namespace Lords {
	public class HelpOverlay : Dialog {

		GameObject list;
		UIScrollView listScroller, detailsScroller;
		UILabel title, yields, details;
		UI2DSprite icon;

		protected override bool PauseWhileVisible {
			get { return true; }
		}

		BuildingType selectedBuildingType = BuildingType.Villa;
		public BuildingType SelectedBuildingType {
			get { return selectedBuildingType; }
			set { 
				selectedBuildingType = value;
				FindListItem(selectedBuildingType).value = true;
			}
		}

		public static void Show(BuildingType type) {
			HelpOverlay overlay = GameObject.Find("HelpOverlay").GetComponent<HelpOverlay>();
			overlay.SelectedBuildingType = type;
			overlay.FadeIn();
		}

		protected override void Start() {
			base.Start();
			list = this.transform.FindChild("Content/Scroller/Grid").gameObject;
			listScroller = this.transform.FindChild("Content/Scroller").gameObject.GetComponent<UIScrollView>();
			title = this.transform.FindChild("Content/Title").gameObject.GetComponent<UILabel>();
			yields = this.transform.FindChild("Content/Yields").gameObject.GetComponent<UILabel>();
			details = this.transform.FindChild("Content/DetailsScroller/Details").gameObject.GetComponent<UILabel>();
			detailsScroller = this.transform.FindChild("Content/DetailsScroller").gameObject.GetComponent<UIScrollView>();
			icon = this.transform.FindChild("Content/Graphic/Icon").gameObject.GetComponent<UI2DSprite>();
			Redraw();
		}

		public override void FadeIn() {
			base.FadeIn();
			UpdateListScroller();
		}

		void Redraw() {

			// TODO: is this right?
			while (list.transform.childCount > 0) {
				GameObject.DestroyImmediate(list.transform.GetChild(0).gameObject);
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

			list.GetComponent<UIGrid>().Reposition();
			UpdateListScroller();
		}

		void UpdateListScroller() {
			listScroller.ResetPosition();

			Transform centered = listScroller.transform.FindChild("Grid/" + SelectedBuildingType);
			if(centered != null) {
				UIPanel panel = NGUITools.FindInParents<UIPanel>(listScroller.gameObject);
				
				Vector3 offset = new Vector3(); //-panel.cachedTransform.InverseTransformPoint(centered.position);
				offset.x = panel.cachedTransform.localPosition.x;
				offset.y = -1 * panel.cachedTransform.InverseTransformPoint(centered.position).y;

				float min = panel.transform.localPosition.y + (list.transform.childCount * list.GetComponent<UIGrid>().cellHeight - panel.height + 8);
				offset.y = Mathf.Max(panel.transform.localPosition.y, Mathf.Min(offset.y, min));
				SpringPanel.Begin(panel.cachedGameObject, offset, 10f);
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
			details.text = "Cost:  " + Strings.BuildingCost(SelectedBuildingType);
			if(Building.BuildingLimits.ContainsKey(SelectedBuildingType)) {
				details.text += String.Format("\nLimit {0} per city", Building.BuildingLimits[SelectedBuildingType]);
			}
			details.text += "\n\n";
			details.text += Strings.BuildingHelp(SelectedBuildingType);
			icon.sprite2D = GameAssets.GetSprite(SelectedBuildingType);
			detailsScroller.ResetPosition();
		}

		UIToggle FindListItem(BuildingType type) {
			return list.transform.FindChild(type.ToString()).gameObject.GetComponent<UIToggle>();
		}


	}
}
