using UnityEngine;
using System;

namespace Lords {
	public class Slider : MonoBehaviour {
		float _value = 0;
		float paddingLeft = 0, paddingRight = 10;
		float width;
		Transform knob;

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
			width = GetComponent<UIWidget>().width - paddingRight;
			knob = transform.FindChild("Knob");
			Redraw();
		}

		public void Redraw() { 
			knob.localPosition = new Vector3(Value * width + paddingLeft, knob.localPosition.y, knob.localPosition.z);
		}
	}
}

