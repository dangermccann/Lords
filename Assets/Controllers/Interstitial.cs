using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lords {
	public class Interstitial : MonoBehaviour {
		public float fadeSpeed = 1f;
		public float delay = 0.5f;

		List<CanvasGroup> items = new List<CanvasGroup>();
		GameObject continueButton;
		int current;

		void Start() {
			if(Game.CurrentLevel == null) {
				Game.CurrentLevel = Levels.Tutorial;
			}

			transform.FindChild("Panel/Border/Title").gameObject.GetComponent<Text>().text = Game.CurrentLevel.name;
			transform.FindChild("Panel/Border/Description").gameObject.GetComponent<Text>().text = Strings.LevelDescription(Game.CurrentLevel);
			transform.FindChild("Panel/Border/ContentContainer/Victory/Values").gameObject.GetComponent<Text>().text = Strings.VictoryConditions(Game.CurrentLevel);

			GameObject illustration = transform.FindChild("Panel/Border/ContentContainer/Illustration").gameObject;
			Sprite illustrationSprite = GameAssets.LevelIllustration(Game.CurrentLevel.illustration);
			illustration.GetComponent<Image>().sprite = illustrationSprite;
			illustration.GetComponent<RectTransform>().sizeDelta = new Vector2(illustrationSprite.rect.width, illustrationSprite.rect.height);

			continueButton = transform.FindChild("Panel/Border/ContinueButton/Inside").gameObject;


			Transform border = transform.FindChild("Panel/Border");
			for(int i = 0; i < border.childCount; i++) {
				CanvasGroup group = border.GetChild(i).gameObject.GetComponent<CanvasGroup>();
				if(group != null) {
					group.alpha = 0;
					items.Add(group);
				}
			}

			current = -1;

			StartCoroutine(FadeIn());
		}

		IEnumerator FadeIn() {
			if(current == -1) { 
				current++;
				yield return new WaitForSeconds(delay * 2);
			}

			while(current < items.Count) {
				if(items[current].alpha < 1) {
					items[current].alpha += Time.unscaledDeltaTime * fadeSpeed;
				}
				else {
					items[current].interactable = false;
					current++;
					yield return new WaitForSeconds(delay);
				}

				yield return null;
			}

			continueButton.GetComponent<CanvasGroup>().ignoreParentGroups = true;
		}
	}
}
