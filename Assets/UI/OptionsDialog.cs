using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lords {
	public class OptionsDialog : MonoBehaviour {
		Dropdown dropdown;
		UnityEngine.UI.Slider musicVolume, effectsVolume;
		Toggle fullScreenToggle;

		void Awake() {
			dropdown = transform.FindChild("ResolutionDropdown").gameObject.GetComponent<Dropdown>();
			musicVolume = transform.FindChild("MusicVolumeSlider").gameObject.GetComponent<UnityEngine.UI.Slider>();
			effectsVolume = transform.FindChild("FXVolumeSlider").gameObject.GetComponent<UnityEngine.UI.Slider>();
			fullScreenToggle = transform.FindChild("FullScreenToggle").gameObject.GetComponent<Toggle>();
		}

		void Start() {

			Resolution[] resolutions = Screen.resolutions;

			if(Debug.isDebugBuild && resolutions.Length < 2) {
				resolutions = new Resolution[20] {
					new Resolution() { width = 640, height = 480 },
					new Resolution() { width = 800, height = 600 },
					new Resolution() { width = 1024, height = 768 },
					new Resolution() { width = 1024, height = 800 },
					new Resolution() { width = 1280, height = 720 },
					new Resolution() { width = 1280, height = 800 },
					new Resolution() { width = 1360, height = 720 },
					new Resolution() { width = 1360, height = 1100 },
					new Resolution() { width = 1920, height = 1200 },
					new Resolution() { width = 1920, height = 1080 },
					new Resolution() { width = 640, height = 480 },
					new Resolution() { width = 800, height = 600 },
					new Resolution() { width = 1024, height = 768 },
					new Resolution() { width = 1024, height = 800 },
					new Resolution() { width = 1280, height = 720 },
					new Resolution() { width = 1280, height = 800 },
					new Resolution() { width = 1360, height = 720 },
					new Resolution() { width = 1360, height = 1100 },
					new Resolution() { width = 1920, height = 1200 },
					new Resolution() { width = 1080, height = 1080 },
				};
			}

			Array.Reverse(resolutions);
			foreach(Resolution res in resolutions) {

				bool selected = false;
				if(Game.Options.resolutionWidth == res.width && Game.Options.resolutionHeight == res.height) {
					selected = true;
					Debug.Log("Found resolution " + ResolutionString(res));
				}

				dropdown.AddItem(ResolutionString(res), res, selected);
			}

			dropdown.OnChanged += (object sender, EventArgs e) => {
				Resolution res = (Resolution) dropdown.SelectedValue;
				Screen.SetResolution(res.width, res.height, Screen.fullScreen);
				Game.Options.resolutionWidth = res.width;
				Game.Options.resolutionHeight = res.height;
				Game.SaveOptions();
			};


			fullScreenToggle.isOn = Game.Options.fullScreen;
			fullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);

			musicVolume.value = Game.Options.musicVolume;
			effectsVolume.value = Game.Options.effectsVolume;
		}

		string ResolutionString(Resolution res) {
			return res.width + " x " + res.height;
		}

		public void ToggleFullScreen(bool fullscreen) {
			Screen.fullScreen = fullscreen;
			Game.Options.fullScreen = fullscreen;
		}

		public void MusicVolumeChanged() {
			Debug.Log(musicVolume.value);


			Game.Options.musicVolume = musicVolume.value;
			MusicController.Instance.Volume = musicVolume.value;
		}

		public void EffectsVolumeChanged() {
			Game.Options.effectsVolume = effectsVolume.value;
		}

		public void Close() {
			transform.parent.gameObject.SetActive(false);
			Game.SaveOptions();
		}
	}
}
