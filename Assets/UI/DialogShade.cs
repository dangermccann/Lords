using UnityEngine;

namespace Lords {
	public class DialogShade : MonoBehaviour {

		void OnPress (bool isPressed) {
			if(isPressed && Dialog.current != null) {
				Dialog.current.FadeOut();

				if(Game.IsPaused()) {
					Game.Resume();
				}
			}
		}
	}
}
