using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

namespace Lords {
	public class Slider : MonoBehaviour {
		float _value = 1;
		float max = 100;
		float padding = 9;

		public float Value {  
			get { return _value; }
			set { 
				if(_value != value) {
					_value = value; 
					_value = Mathf.Clamp(_value, 0, 1);
					Redraw();
				}
			} 
		}

		void Start() { 
			Redraw();
		}

		public void Redraw() { 
			float width = this.GetComponent<RectTransform>().rect.width;

			RectTransform knob = transform.FindChild("Image").gameObject.GetComponent<RectTransform>();
			knob.localPosition = new Vector3(Value / max * width + padding, knob.localPosition.y, knob.localPosition.z);
		}
	}
}

