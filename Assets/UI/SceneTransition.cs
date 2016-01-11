using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Lords {
	public class SceneTransition : MonoBehaviour {
		private static float fadeSpeed = 2;
		private static bool fadeSceneIn = false;

		private static SceneTransition Instance {
			get	{
				return GameObject.Find("Canvas").GetComponent<SceneTransition>();
			}
		}

		public static void LoadScene(string name) {
			Instance.LoadLevel(name);
		}

		public void LoadLevel(string name) {
			StartCoroutine(FadeOut(CreateShade(), name));
		}

		public void LoadMap() {
			LoadLevel("map");
		}

		public void LoadMain() {
			LoadLevel("main");
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
			while(image.color.a < 1) {
				image.color = White(image.color.a + Time.unscaledDeltaTime * fadeSpeed);
				yield return null;
			}

			fadeSceneIn = true;
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
}
