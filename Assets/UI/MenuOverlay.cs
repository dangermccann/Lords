using UnityEngine;

namespace Lords {
	public class MenuOverlay : Dialog {


		public override void FadeIn() {
			base.FadeIn();
			Game.Pause();
			Redraw();
		}

		public override void FadeOut() {
			base.FadeOut();
			Game.Resume();
		}

		public void Redraw() {
			transform.FindChild("Background/Title").GetComponent<UILabel>().text = Game.CurrentLevel.name;
			transform.FindChild("Background/Description").GetComponent<UILabel>().text = Strings.LevelDescription(Game.CurrentLevel);


			string victory = Game.CurrentLevel.victoryConditions.Population + "\n"
					+ Game.CurrentLevel.victoryConditions.Happiness + "\n"
					+ Game.CurrentLevel.victoryConditions.Prosperity + "\n"
					+ Game.CurrentLevel.victoryConditions.Culture;

			transform.FindChild("Background/VictoryValues").GetComponent<UILabel>().text = victory;
		}
	}
}


