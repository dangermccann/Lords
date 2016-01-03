using System;
using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour {

	public float fadeSpeed = 8.0f;
	public GameObject shade = null;

	UIPanel thisPanel, shadePanel;

	public static Dialog current = null;

	void Start() {
		thisPanel = this.GetComponent<UIPanel>();
		if(shade != null) {
			shadePanel = shade.GetComponent<UIPanel>();
		}
	}

	public void FadeIn() {
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

	public void FadeOut() {
		StartCoroutine(FadeGroupOut(thisPanel));
		
		if(shadePanel != null) {
			StartCoroutine(FadeGroupOut(shadePanel));
		}

		current = null;
	}

	IEnumerator FadeGroupIn(UIPanel panel, float targetAlpha) {
		while(panel.alpha < targetAlpha) {
			panel.alpha += Time.deltaTime * fadeSpeed;
			yield return null;
		}

		yield return null;
	}

	IEnumerator FadeGroupOut(UIPanel panel) {

		while(panel.alpha > 0) {
			panel.alpha -= Time.deltaTime * fadeSpeed;
			yield return null;
		}

		yield return null;
	}
}
