using UnityEngine;
using UnityEngine.UI;

namespace Lords {
	public class Promotion : MonoBehaviour {
		float startTime;
		const float displayTime = 6f;

		void Start() {
			startTime = Time.time;

			if(Game.CurrentLevel !=  null) {
				transform.FindChild("Rank").GetComponent<Text>().text = Strings.FormatRank(Game.CurrentLevel.promotesTo);
			}
		}

		void Update() {
			if(Time.time > startTime + displayTime) {
				SceneTransition.LoadScene(Scenes.Map);
				return;
			}

			if(Input.GetMouseButtonUp(0)) {
				SceneTransition.LoadScene(Scenes.Map);
				return;
			}
		}

	}
}
