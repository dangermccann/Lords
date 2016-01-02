using System;
using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour {

	public float fadeSpeed = 10.0f;
	public GameObject shade = null;

	UIPanel thisGroup, shadeGroup;

	void Start() {
		thisGroup = this.GetComponent<UIPanel>();
		if(shade != null) {
			shadeGroup = shade.GetComponent<UIPanel>();
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

	IEnumerator FadeGroupIn(UIPanel group) {
		while(group.alpha < 1) {
			group.alpha += Time.deltaTime * fadeSpeed;
			yield return null;
		}

		yield return null;
	}

	IEnumerator FadeGroupOut(UIPanel group) {

		while(group.alpha > 0) {
			group.alpha -= Time.deltaTime * fadeSpeed;
			yield return null;
		}

		yield return null;
	}
}
