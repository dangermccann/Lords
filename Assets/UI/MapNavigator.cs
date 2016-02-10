using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Lords {
	public class MapNavigator : MonoBehaviour {
		EffectsController effects;
		public float wheelScrollAmount = 100;

		static Color completeColor = new Color(248f/256f, 222f/256f, 18f/256f, 1);
		static Color disabledColor = new Color(1, 1, 1, 98f/255f);
		static Color normalColor = Color.white;

		void Start () {

			SavedGame game = Game.Load();

			foreach(Level level in Levels.All) {
				GameObject cityGO = (GameObject) Instantiate(Resources.Load("City"));
				cityGO.transform.localPosition = new Vector3(level.mapLocation.x, level.mapLocation.y, 0);
				cityGO.transform.SetParent(this.transform, false);
				cityGO.name = level.name;

				Text label = cityGO.transform.FindChild("Text").GetComponent<Text>();
				Image icon = cityGO.transform.FindChild("Image").GetComponent<Image>();

				if(game.completedLevels.Contains(level.name)) {
					icon.color = completeColor;
				}
				else if(false) {
					// TODO: detect locked cities 
					icon.color = disabledColor;
					label.color = disabledColor;
				}
				else {
					icon.color = normalColor;

					// install click handler for icon
					EventTrigger trigger = icon.gameObject.GetComponent<EventTrigger>();
					
					EventTrigger.Entry entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.PointerClick;
					Level finalLevel = level;
					entry.callback.AddListener( (eventData) => {  
						CityClicked(finalLevel);
					});
					
					trigger.delegates.Add(entry);
				}

				label.text = level.name;
			}

			effects = GameObject.Find("EffectsController").GetComponent<EffectsController>();

		}
		

		void Update () {
			float wheelAmount = Input.GetAxis("Mouse ScrollWheel");
			if(wheelAmount != 0) {
				float tt = transform.localScale.x;
				tt += wheelAmount * Time.deltaTime * wheelScrollAmount;
				tt = Mathf.Clamp(tt, 1, 3);
				transform.localScale = new Vector3(tt, tt, tt);
			}
		}

		public void CityClicked(Level level) {
			Debug.Log ("clicked " + level.name);
			effects.Click2();

			Game.CurrentLevel = level;

			SavedGame saved = Game.Load();
			if(saved != null) {
				SavedCity city = saved.FindCity(level.name);
				if(city != null && city.elapsedTime > 0) {
					SceneTransition.LoadScene(Scenes.Main);
					return;
				}
			}

			SceneTransition.LoadScene(Scenes.Interstitial);
		}
	}
}
