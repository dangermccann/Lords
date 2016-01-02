using UnityEngine;

namespace Lords {
	public class BuildingDialog : MonoBehaviour {

		Dialog dialog;

		void Start () {
			GameObject grid = this.transform.FindChild("ScrollView/Grid").gameObject;
			dialog = GetComponent<Dialog>();

			foreach(BuildingType type in Building.Types) {
				GameObject item = (GameObject) Instantiate(Resources.Load("BuildingOverlayItem"), Vector3.zero, Quaternion.identity);
				item.name = "BuildingOverlayItem-" + type.ToString();
				item.transform.SetParent(grid.transform);
				item.transform.localScale = Vector3.one;
				item.transform.FindChild("Title").gameObject.GetComponent<UILabel>().text = Strings.BuildingTitle(type);
				item.transform.FindChild("Cost").gameObject.GetComponent<UILabel>().text = Strings.BuildingCost(type);
				//item.transform.FindChild("Yield").gameObject.GetComponent<UILabel>().text = Strings.BuildingYield(type);
				item.transform.FindChild("Hex/Icon").gameObject.GetComponent<UI2DSprite>().sprite2D = GameAssets.GetSprite(type);
				EventDelegate.Add(item.transform.FindChild("Hex").GetComponent<UIButton>().onClick, delegate() {
					Debug.Log("here");
					dialog.FadeOut();
				});
			}

			grid.GetComponent<UIGrid>().Reposition();

		}

		void Update () {
		}
	}
}