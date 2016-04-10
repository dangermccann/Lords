using UnityEngine;

namespace Lords {
	public class VictoryOverlay : Dialog {

		public enum Mode {
			Victory,
			Failure
		};

		public enum FailureReason {
			TimeLimit,
			Culture,
			Happiness,
			Prosperity
		}

		public Mode OverlayMode = Mode.Victory;
		public FailureReason Reason = FailureReason.TimeLimit;
		public GameObject sceneTransition;

		public static void Show(Mode mode, FailureReason reason = FailureReason.TimeLimit) {
			VictoryOverlay overlay = GameObject.Find("VictoryOverlay").GetComponent<VictoryOverlay>();
			overlay.OverlayMode = mode;
			overlay.Reason = reason;
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
				Description().text = Strings.FailureMessage(Game.CurrentLevel, Reason);
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

		public void ExitButtonClicked() {
			SceneTransition t = sceneTransition.GetComponent<SceneTransition>();

			if(OverlayMode == Mode.Victory) {
				if(Game.CurrentLevel.promotesTo != Ranks.None) {
					t.LoadPromotion();
				}
				else {
					t.LoadMap();
				}
			}
			else {
				t.LoadMap();
			}
		}
	}
}
