using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Lords {
	public class Dropdown : MonoBehaviour {

		public event EventHandler OnChanged;

		private List<DropdownItem> items;
		private DropdownItem selectedItem;
		private GameObject itemContainer, popup;
		private Text label;
		private bool open = false;

		public GameObject itemTemplate;

		public object SelectedValue {
			get {
				return selectedItem.Value;
			}
		}

		public List<DropdownItem> Items {
			get { return items; }
		}

		void Awake() {
			items = new List<DropdownItem>();
			popup = transform.FindChild("Popup").gameObject;
			itemContainer = popup.transform.FindChild("ContentScroller/ContentContainer").gameObject;
			label = transform.FindChild("Text").GetComponent<Text>();
		}

		void Start() {
			popup.SetActive(false);

			GetComponent<Button>().onClick.AddListener(new UnityAction(() => { 
				open = !open;
				popup.SetActive(open);
			}));

		}

		public void AddItem(string text, object value, bool selected = false) {
			GameObject go = (GameObject) GameObject.Instantiate(itemTemplate);
			go.name = text;
			go.SetActive(true);
			go.transform.SetParent(itemContainer.transform, false);

			DropdownItem item = go.GetComponent<DropdownItem>();
			item.Text = text;
			item.Value = value;

			item.GetComponent<Button>().onClick.AddListener(new UnityAction(() => { 
				if(selectedItem != null) {
					selectedItem.Checked = false;
				}

				SelectItem(item);
				popup.SetActive(false);
				open = false;

				if(OnChanged != null) {
					OnChanged(this, EventArgs.Empty);
				}
			}));

			if(selected) {
				SelectItem(item);
			}
		}

		public void SelectItem(DropdownItem item) {
			foreach(DropdownItem i in items) {
				i.Checked = false;
			}

			if(item != null) {
				label.text = item.Text;
				item.Checked = true;
				selectedItem = item;
			}
			else {
				Debug.LogWarning("Attempting to select a null item in Dropdown");
			}
		}
	}
}
