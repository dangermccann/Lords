using UnityEngine;

namespace Lords {

	public class MultiSlider : MonoBehaviour {
		public GameObject[] sliders;
		bool changing = false;

		void Start() {
			RegisterListeners();
		}

		void RegisterListeners() {
			if(sliders != null) {
				foreach(GameObject go in sliders) {
					EventDelegate.Add(go.GetComponent<UISlider>().onChange, OnChanged);
				}
			}
		}

		void UnregisterListeners() {
			if(sliders != null) {
				foreach(GameObject go in sliders) {
					EventDelegate.Remove(go.GetComponent<UISlider>().onChange, OnChanged);
				}
			}
		}

		void OnChanged() {
			if(sliders.Length < 2) {
				Debug.LogWarning("MultiSlider requires two or more sliders");
				return;
			}

			if(changing) {
				return;
			}

			changing = true;

			try {
				string name = UISlider.current.name;
				float value = UISlider.current.value;
				float remaining = 1.0f - value;
				float otherTotals = 0;

				foreach(GameObject go in sliders) {
					if(go.name != name) {
						otherTotals += go.GetComponent<UISlider>().value;
					}
				}

				foreach(GameObject go in sliders) {
					if(go.name != name) {
						UISlider slider = go.GetComponent<UISlider>();
						if(otherTotals != 0)
							slider.value = remaining * slider.value / otherTotals;
						else
							slider.value = remaining / (sliders.Length - 1);
					}
				}
			}
			finally {
				changing = false;
			}
		}

		public void BulkUpdate(float[] values) {
			if(values.Length != sliders.Length) {
				Debug.LogWarning("MultiSlider - Invalid values");
				return;
			}

			changing = true;

			for(int i = 0; i < values.Length; i++) {
				sliders[i].GetComponent<UISlider>().value = values[i];
			}

			changing = false;
		}

		public void SetSliders(GameObject[] _sliders) {
			UnregisterListeners();
			sliders = _sliders;
			RegisterListeners();
		}
	}
}
