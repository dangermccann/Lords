using UnityEngine;
using UnityEngine.UI;

namespace Lords {
	public class DropdownItem : MonoBehaviour {
		private Text _text;
		private GameObject _check;
		private bool _checked;
		
		public bool Checked {
			get {
				return _checked;
			}
			set {
				_checked = value;
				_check.SetActive(_checked);
			}
		}
		
		public string Text {
			get { return _text.text; }
			set { _text.text = value; }
		}
		
		public object Value;
		
		void Awake() {
			_text = transform.FindChild("Text").gameObject.GetComponent<Text>();
			_check = transform.FindChild("Check").gameObject;
		}

		void Start() {
			Checked = false;
		}
	}
}
