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

		void Awake() {
			items = new List<DropdownItem>();
			popup = transform.FindChild("Popup").gameObject;
			itemContainer = popup.transform.FindChild("ContentScroller/ContentContainer").gameObject;
		}

		void Start() {
			popup.SetActive(false);

			label = transform.FindChild("Text").GetComponent<Text>();

			GetComponent<Button>().onClick.AddListener(new UnityAction(() => { 
				open = !open;
				popup.SetActive(open);
			}));

		}

		public void AddItem(string text, object value) {
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
		}

		public DropdownItem FindItem(object value) {
			foreach(DropdownItem item in items) {
				if(item.Value == value) {
					return item;
				}
			}
			return null;
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
		}
	}
}
