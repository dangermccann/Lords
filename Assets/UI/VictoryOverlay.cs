using UnityEngine;

namespace Lords {
	public class VictoryOverlay : Dialog {

		protected override void Start() {
			base.Start();
			transform.FindChild("Background/Description").GetComponent<UILabel>().text = Strings.VictoryMessage(Game.CurrentLevel);
		}

		public override bool IsDismissible() {
			return false;
		}
	}
}
