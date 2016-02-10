using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Lords {
	public class SceneTransition : MonoBehaviour {
		const float fadeSpeed = 2;
		private static bool fadeSceneIn = false;

		bool isTransitioning = false;

		private static SceneTransition Instance {
			get	{
				return GameObject.Find("Canvas").GetComponent<SceneTransition>();
			}
		}

		public static void LoadScene(string name) {
			Instance._LoadScene(name);
		}

		void _LoadScene(string name) {
			Game.Resume();

			if(!isTransitioning) {
				StartCoroutine(FadeOut(CreateShade(), name));
			}
		}

		public void LoadMap() {
			_LoadScene(Scenes.Map);
		}

		public void LoadMain() {
			_LoadScene(Scenes.Main);
		}

		public void LoadHome() {
			_LoadScene(Scenes.Home);
		}

		public void LoadInterstitial() {
			_LoadScene(Scenes.Interstitial);
		}

		public void LoadPromotion() {
			_LoadScene(Scenes.Promotion);
		}

		public void LevelComplete() {
			if(Game.CurrentLevel.promotesTo != Ranks.None) {
				LoadPromotion();
			}
			else {
				LoadMap();
			}
		}

		static Image CreateShade() {
			GameObject canvas = GameObject.Find("Canvas");
			GameObject shade = (GameObject) GameObject.Instantiate(Resources.Load ("transition"));
			shade.transform.SetParent(canvas.transform);
			shade.transform.localScale = Vector3.one;
			shade.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
			shade.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			Image image = shade.GetComponent<Image>();
			image.color = White(0);
			return image;
		}

		IEnumerator FadeOut(Image image, string sceneName) {
			isTransitioning = true;

			while(image.color.a < 1) {
				image.color = White(image.color.a + Time.unscaledDeltaTime * fadeSpeed);
				yield return null;
			}

			fadeSceneIn = true;
			isTransitioning = false;
			Application.LoadLevel(sceneName);
		}

		void OnLevelWasLoaded(int level) {
			Debug.Log("level loaded " + level);
			if(fadeSceneIn) {
				fadeSceneIn = false;
				Image newImage = CreateShade();
				newImage.color = White(1);
				StartCoroutine(FadeIn(newImage));
			}
		}

		IEnumerator FadeIn(Image image) {
			while(image.color.a > 0) {
				image.color = White(image.color.a - Time.unscaledDeltaTime * fadeSpeed);
				yield return null;
			}

			Destroy(image.gameObject);
		}

		static Color White(float alpha) {
			return new Color(Color.white.r, Color.white.g, Color.white.b, alpha);
		}
	}

	public class Scenes {
		public const string Home 			= "home";
		public const string Map 			= "map";
		public const string Main 			= "main";
		public const string Interstitial 	= "interstitial";
		public const string Promotion		= "promotion";
	}
}
