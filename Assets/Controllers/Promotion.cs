using UnityEngine;
using UnityEngine.UI;

namespace Lords {
	public class Promotion : MonoBehaviour {
		float startTime;
		const float displayTime = 6f;
		string next = Scenes.Map;

		void Start() {
			startTime = Time.time;

			if(Game.CurrentLevel !=  null) {
				transform.FindChild("Rank").GetComponent<Text>().text = Strings.FormatRank(Game.CurrentLevel.promotesTo);

				if(Game.CurrentLevel.promotesTo == Ranks.Lord_Mayor) {

					// game complete, queue the credits
					next = Scenes.Credits;
				}
			}

			Game.Resume();
		}

		void Update() {
			if(Time.time > startTime + displayTime) {
				SceneTransition.LoadScene(next);
				return;
			}

			if(Input.GetMouseButtonUp(0)) {
				SceneTransition.LoadScene(next);
				return;
			}
		}

	}
}
