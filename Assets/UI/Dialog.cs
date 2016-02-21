using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Dialog : MonoBehaviour {

		public float fadeSpeed = 8.0f;
		public GameObject shade = null;

		UIPanel thisPanel, shadePanel;

		public static Dialog current;

		protected EffectsController effects;

		protected virtual void Start() {
			current = null;
			thisPanel = this.GetComponent<UIPanel>();
			if(shade != null) {
				shadePanel = shade.GetComponent<UIPanel>();
			}

			effects = GameObject.Find("EffectsController").GetComponent<EffectsController>();
		}

		public virtual void FadeIn() {
			StartCoroutine(FadeGroupIn(thisPanel, 1));

			if(shadePanel != null) {
				StartCoroutine(FadeGroupIn(shadePanel, 0.85f));
			}

			current = this;
			effects.Open();
		}

		public bool Visible {
			get {
				return thisPanel.alpha > 0;
			}
		}

		public virtual bool IsDismissible() {
			return true;
		}

		public void Close() {
			FadeOut();
		}

		public virtual void FadeOut(bool dismissShade = true) {
			StartCoroutine(FadeGroupOut(thisPanel));
			
			if(dismissShade && shadePanel != null) {
				StartCoroutine(FadeGroupOut(shadePanel));
			}

			current = null;
		}

		IEnumerator FadeGroupIn(UIPanel panel, float targetAlpha) {
			while(panel.alpha < targetAlpha) {
				panel.alpha += Time.unscaledDeltaTime * fadeSpeed;
				yield return null;
			}
		}

		IEnumerator FadeGroupOut(UIPanel panel) {

			while(panel.alpha > 0) {
				panel.alpha -= Time.unscaledDeltaTime * fadeSpeed;
				yield return null;
			}
		}
	}
}
