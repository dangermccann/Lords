using UnityEngine;

namespace Lords {
	public class VictoryOverlay : Dialog {

		public enum Mode {
			Victory,
			Failure
		};

		public Mode OverlayMode = Mode.Victory;

		public static void Show(Mode mode) {
			VictoryOverlay overlay = GameObject.Find("VictoryOverlay").GetComponent<VictoryOverlay>();
			overlay.OverlayMode = mode;
			overlay.FadeIn();
		}

		protected override void Start() {
			base.Start();
		}

		public override void FadeIn() {
			if(OverlayMode == Mode.Victory) {
				Title().text = "Victory!";
				Description().text = Strings.VictoryMessage(Game.CurrentLevel);
			}
			else {
				Title().text = "Failure!";
				Description().text = Strings.FailureMessage(Game.CurrentLevel);
			}
			base.FadeIn();
		}

		public override bool IsDismissible() {
			return false;
		}

		UILabel Description() {
			return transform.FindChild("Background/Description").GetComponent<UILabel>();
		}

		UILabel Title() {
			return transform.FindChild("Background/Title").GetComponent<UILabel>();
		}
	}
}
