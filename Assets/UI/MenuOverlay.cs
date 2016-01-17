using UnityEngine;

namespace Lords {
	public class MenuOverlay : Dialog {


		public override void FadeIn() {
			base.FadeIn();
			Game.Pause();
			Redraw();

			Game.Save();
		}

		public override void FadeOut() {
			base.FadeOut();
			Game.Resume();
		}

		public void Redraw() {
			transform.FindChild("Background/Title").GetComponent<UILabel>().text = Game.CurrentLevel.name;
			transform.FindChild("Background/Description").GetComponent<UILabel>().text = Strings.LevelDescription(Game.CurrentLevel);
			transform.FindChild("Background/VictoryValues").GetComponent<UILabel>().text = Strings.VictoryConditions(Game.CurrentLevel);
		}
	}
}


