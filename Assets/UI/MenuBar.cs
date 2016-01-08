using UnityEngine;

namespace Lords {
	public class MenuBar : MonoBehaviour {

		public GameObject menuOverlay;

		void Start() { 
			if(Game.CurrentLevel != null) {
				LevelLoaded(Game.CurrentLevel);
			}

			Game.LevelLoaded += LevelLoaded;
		}

		void LevelLoaded(Level level) {
			UILabel cityName = transform.FindChild("CityName").gameObject.GetComponent<UILabel>();
			cityName.text = level.name;
		}

		public void OnClick() {
			Debug.Log(UICamera.hoveredObject.name);
			menuOverlay.GetComponent<Dialog>().FadeIn();
		}

		public void Menu() {
			Debug.Log("Opening menu");
		}

		public void Help() {
			Debug.Log("Opening help");
		}

	}
}
