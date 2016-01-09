using UnityEngine;
using System.Collections;

namespace Lords {
	public class MapNavigator : MonoBehaviour {
		void Start () {
		
		}
		

		void Update () {
			float tt = transform.localScale.x;
			tt += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 400;
			tt = Mathf.Clamp(tt, 1, 3);
			transform.localScale = new Vector3(tt, tt, tt);
		}
	}
}
