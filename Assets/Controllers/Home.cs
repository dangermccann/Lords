using UnityEngine;
using UnityEngine.UI;
using System;

namespace Lords {
	public class Home : MonoBehaviour {
		Transform mapTransform;
		Vector2 min, max, direction;
		float speed = 25f;

		void Start() {
			RectTransform mask = transform.FindChild("Panel/Mask").gameObject.GetComponent<RectTransform>();
			RectTransform map = transform.FindChild("Panel/Mask/Map").gameObject.GetComponent<RectTransform>();
			mapTransform = map.gameObject.transform;

			min = new Vector2(mask.rect.width - map.rect.width, mask.rect.height - map.rect.height);
			max = new Vector2(0, 0);
			mapTransform.position = new Vector3(0, 0, 0);

			Debug.Log(mapTransform.position);
			Debug.Log("min: " + min);

			direction = new Vector2(-0.5f, -0.5f);
		}

		void Update() {
			//Debug.Log(mapTransform.position);

			Vector3 pos = new Vector3(mapTransform.position.x, mapTransform.position.y, 0);
			pos.x = pos.x + direction.x * Time.deltaTime * speed;
			pos.y = pos.y + direction.y * Time.deltaTime * speed;

			mapTransform.position = pos;

			if(pos.x <= min.x && direction.x < 0) {
				Debug.Log("Right edge: " + pos);
				direction.x = UnityEngine.Random.Range(0.1f, 1);
			}
			else if(pos.x >= max.x && direction.x > 0) {
				Debug.Log("Left edge: " + pos);
				direction.x = UnityEngine.Random.Range(-1, -0.1f);
			}

			if(pos.y <= min.y && direction.y < 0) {
				Debug.Log("Top edge: " + pos);
				direction.y = UnityEngine.Random.Range(0.1f, 1);
			}
			else if(pos.y >= max.y && direction.y > 0) {
				Debug.Log("Bottom edge: " + pos);
				direction.y = UnityEngine.Random.Range(-1, -0.1f);
			}
		}
	}
}

