using UnityEngine;

namespace Lords {
	public class MenuBar : MonoBehaviour {

		public GameObject menuOverlay, helpOverlay;
		UILabel dateLabel;

		void Start() { 
			if(Game.CurrentLevel != null) {
				LevelLoaded(Game.CurrentLevel);
			}

			Game.LevelLoaded += LevelLoaded;
			dateLabel = transform.FindChild("Container/Date").gameObject.GetComponent<UILabel>();
		}

		void FixedUpdate() {
			dateLabel.text = Strings.FormatElapsedTime(Game.CurrentCity.ElapsedTime);
		}

		void OnDestroy() {
			Game.LevelLoaded -= LevelLoaded;
		}

		void LevelLoaded(Level level) {
			UILabel cityName = transform.FindChild("CityName").gameObject.GetComponent<UILabel>();
			cityName.text = level.name;
		}

		public void Menu() {
			menuOverlay.GetComponent<MenuOverlay>().FadeIn();
		}

		public void Help() {
			helpOverlay.GetComponent<HelpOverlay>().FadeIn();
		}

	}
}
