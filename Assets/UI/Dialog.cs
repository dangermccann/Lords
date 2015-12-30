using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

	public float fadeSpeed = 10.0f;
	public GameObject shade = null;

	CanvasGroup thisGroup, shadeGroup;

	void Start() {
		thisGroup = this.GetComponent<CanvasGroup>();
		if(shade != null) {
			shadeGroup = shade.GetComponent<CanvasGroup>();
		}
	}

	public void FadeIn() {
		StartCoroutine(FadeGroupIn(thisGroup));

		if(shadeGroup != null) {
			StartCoroutine(FadeGroupIn(shadeGroup));
		}
	}

	public void FadeOut() {
		StartCoroutine(FadeGroupOut(thisGroup));
		
		if(shadeGroup != null) {
			StartCoroutine(FadeGroupOut(shadeGroup));
		}
	}

	IEnumerator FadeGroupIn(CanvasGroup group) {
		while(group.alpha < 1) {
			group.alpha += Time.deltaTime * fadeSpeed;
			yield return null;
		}

		group.interactable = true;
		group.blocksRaycasts = true;
		yield return null;
	}

	IEnumerator FadeGroupOut(CanvasGroup group) {
		group.interactable = false;
		group.blocksRaycasts = false;

		while(group.alpha > 0) {
			group.alpha -= Time.deltaTime * fadeSpeed;
			yield return null;
		}

		yield return null;
	}
}
