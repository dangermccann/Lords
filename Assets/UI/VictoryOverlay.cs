using UnityEngine;

namespace Lords {
	public class VictoryOverlay : Dialog {

		protected override void Start() {
			base.Start();
			Game.LevelLoaded += LevelLoaded;
			LevelLoaded(Game.CurrentLevel);
		}

		void OnDestroy() {
			Game.LevelLoaded -= LevelLoaded;
		}

		void LevelLoaded(Level level) {
			if(level != null) {
				transform.FindChild("Background/Description").GetComponent<UILabel>().text = Strings.VictoryMessage(level);
			}
		}

		public override bool IsDismissible() {
			return false;
		}
	}
}
