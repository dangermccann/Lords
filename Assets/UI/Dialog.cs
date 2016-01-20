using System;
using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour {

	public float fadeSpeed = 8.0f;
	public GameObject shade = null;

	UIPanel thisPanel, shadePanel;

	public static Dialog current;

	protected virtual void Start() {
		current = null;
		thisPanel = this.GetComponent<UIPanel>();
		if(shade != null) {
			shadePanel = shade.GetComponent<UIPanel>();
		}
	}

	public virtual void FadeIn() {
		StartCoroutine(FadeGroupIn(thisPanel, 1));

		if(shadePanel != null) {
			StartCoroutine(FadeGroupIn(shadePanel, 0.85f));
		}

		current = this;
	}

	public bool Visible {
		get {
			return thisPanel.alpha > 0;
		}
	}

	public virtual bool IsDismissible() {
		return true;
	}

	public virtual void FadeOut() {
		StartCoroutine(FadeGroupOut(thisPanel));
		
		if(shadePanel != null) {
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
