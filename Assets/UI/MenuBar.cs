using UnityEngine;

namespace Lords {
	public class MenuBar : MonoBehaviour {

		public GameObject menuOverlay;

		void Start() { 
			if(Game.CurrentLevel != null) {
				LevelLoaded(Game.CurrentLevel);
			}

			Game.LevelLoaded += LevelLoaded;

			GameObject menu = transform.FindChild("Container/Menu").gameObject;
			EventDelegate.Add(menu.GetComponent<UIButton>().onClick, OnMenuClick);
		}

		void OnDestroy() {
			Game.LevelLoaded -= LevelLoaded;
		}

		void LevelLoaded(Level level) {
			UILabel cityName = transform.FindChild("CityName").gameObject.GetComponent<UILabel>();
			cityName.text = level.name;
		}

		public void OnMenuClick() {
			Debug.Log(UICamera.hoveredObject.name);
			menuOverlay.GetComponent<MenuOverlay>().FadeIn();
		}

		public void Menu() {
			Debug.Log("Opening menu");
		}

		public void Help() {
			Debug.Log("Opening help");
		}

	}
}
