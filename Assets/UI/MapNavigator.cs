using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Lords {
	public class MapNavigator : MonoBehaviour {
		void Start () {

			foreach(Level level in Levels.All) {
				GameObject cityGO = (GameObject) Instantiate(Resources.Load("City"));
				cityGO.transform.localPosition = new Vector3(level.mapLocation.x, level.mapLocation.y, 0);
				cityGO.transform.SetParent(this.transform, false);
				cityGO.name = level.name;
				cityGO.transform.FindChild("Text").GetComponent<Text>().text = level.name;

				EventTrigger trigger = cityGO.transform.FindChild("Image").gameObject.GetComponent<EventTrigger>();

				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				Level finalLevel = level;
				entry.callback.AddListener( (eventData) => {  
					CityClicked(finalLevel);
				});

				trigger.delegates.Add(entry);
			}

		}
		

		void Update () {
			float wheelAmount = Input.GetAxis("Mouse ScrollWheel");
			if(wheelAmount != 0) {
				float tt = transform.localScale.x;
				tt += wheelAmount * Time.deltaTime * 400;
				tt = Mathf.Clamp(tt, 1, 3);
				transform.localScale = new Vector3(tt, tt, tt);
			}
		}

		public void CityClicked(Level level) {
			Debug.Log ("clicked " + level.name);
			Game.CurrentLevel = level;
			SceneTransition.LoadScene("interstitial");
		}
	}
}
