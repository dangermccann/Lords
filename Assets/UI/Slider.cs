using UnityEngine;
using UnityEditor;
using System;

namespace Lords {
	public class Slider : MonoBehaviour {
		float _value = 0;
		float max = 100;
		float padding = 0;

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
			float width = this.GetComponent<UIWidget>().width;

			Transform knob = transform.FindChild("Knob");

			knob.localPosition = new Vector3(Value / max * width - width/2 + padding, knob.localPosition.y, knob.localPosition.z);
		}
	}
}

