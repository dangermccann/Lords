using UnityEngine;

namespace Lords {
	public class MenuOverlay : MonoBehaviour {


		public void Show() {
			Game.Pause();
			Redraw();
			GetComponent<Dialog>().FadeIn();
		}

		public void Hide() {
			Game.Resume();
			GetComponent<Dialog>().FadeOut();
		}

		public void Redraw() {
			transform.FindChild("Background/Title").GetComponent<UILabel>().text = Game.CurrentLevel.name;

			string victory = Game.CurrentLevel.victoryConditions.Population + "\n"
					+ Game.CurrentLevel.victoryConditions.Happiness + "\n"
					+ Game.CurrentLevel.victoryConditions.Prosperity + "\n"
					+ Game.CurrentLevel.victoryConditions.Culture;

			transform.FindChild("Background/VictoryValues").GetComponent<UILabel>().text = victory;
		}
	}
}


