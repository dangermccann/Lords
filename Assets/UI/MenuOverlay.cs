using UnityEngine;

namespace Lords {
	public class MenuOverlay : Dialog {


		protected override bool PauseWhileVisible {
			get { return true; }
		}

		public override void FadeIn() {
			base.FadeIn();
			Redraw();

			Game.Save();
		}

		public void Redraw() {
			transform.FindChild("Background/Title").GetComponent<UILabel>().text = Game.CurrentLevel.name;
			transform.FindChild("Background/Description").GetComponent<UILabel>().text = Strings.LevelDescription(Game.CurrentLevel);
			transform.FindChild("Background/VictoryValues").GetComponent<UILabel>().text = Strings.VictoryConditions(Game.CurrentLevel);
		}
	}
}


