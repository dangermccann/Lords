using UnityEngine;

namespace Lords {
	public class InventoryLabel : MonoBehaviour {
		UILabel label;

		void Start() {
			label = GetComponent<UILabel>();
		}

		void FixedUpdate() {
			label.text = Strings.CityInventory(Game.CurrentCity);
		}
	}
}


