using System;
using UnityEngine;

namespace Lords {
	public class OptionsDialog : MonoBehaviour {
		Dropdown dropdown;

		void Start() {
			dropdown = transform.FindChild("ResolutionDropdown").GetComponent<Dropdown>();

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
					new Resolution() { width = 1920, height = 1080 },
				};
			}

			foreach(Resolution res in resolutions) {
				dropdown.AddItem(ResolutionString(res), res);

				Debug.Log("Adding " + ResolutionString(res));
			}

			dropdown.SelectItem(dropdown.FindItem(Screen.currentResolution));

			dropdown.OnChanged += (object sender, EventArgs e) => {
				Resolution res = (Resolution) dropdown.SelectedValue;
				Screen.SetResolution(res.width, res.height, false);
			};

		}

		string ResolutionString(Resolution res) {
			return res.width + " x " + res.height;
		}

		public void Close() {
			transform.parent.gameObject.SetActive(false);
		}
	}
}
