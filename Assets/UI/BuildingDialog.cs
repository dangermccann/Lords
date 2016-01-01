using UnityEngine;
using UnityEngine.UI;

namespace Lords {
	public class BuildingDialog : MonoBehaviour {

		Dialog dialog;

		void Start () {
			GameObject content = this.transform.FindChild("Scroller/Content").gameObject;
			dialog = GetComponent<Dialog>();

			foreach(BuildingType type in Building.Types) {
				GameObject item = (GameObject) Instantiate(Resources.Load("BuildingDialogItem"), Vector3.zero, Quaternion.identity);
				item.transform.SetParent(content.transform);
				item.transform.FindChild("Title").gameObject.GetComponent<Text>().text = Strings.BuildingTitle(type);
				item.transform.FindChild("Cost").gameObject.GetComponent<Text>().text = Strings.BuildingCost(type);
				item.transform.FindChild("Yield").gameObject.GetComponent<Text>().text = Strings.BuildingYield(type);
				item.transform.FindChild("Hex/Building").gameObject.GetComponent<Image>().sprite = GameAssets.GetSprite(type);
				item.transform.FindChild("Hex").GetComponent<Button>().onClick.AddListener(() => {
					dialog.FadeOut();
				});
			}

		}

		void Update () {
		}
	}
}