using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	Vector3 lastMousePosition = Vector3.zero; 
	public Camera camera;

	void Start () {
		if(camera == null) {
			camera = Camera.main;
		}
	}

	void Update () {
		UpdateZoom();
		UpdatePosition();
	}

	void UpdateZoom() {
		float zoomChange = 0f;
		float zoom = Input.GetAxis("Mouse ScrollWheel");
		if (zoom > 0) {
			zoomChange = -1 * camera.orthographicSize / 10;
		}
		else if (zoom < 0) {
			zoomChange = camera.orthographicSize / 10;
		}
		
		if(zoomChange != 0) {
			camera.orthographicSize = Mathf.Max(Mathf.Min(camera.orthographicSize + zoomChange, 30), 4);
		}
	}

	void UpdatePosition() {
		Vector3 currentPosition = camera.ScreenToWorldPoint(Input.mousePosition);

		if(lastMousePosition != Vector3.zero) {
			Vector3 diff = lastMousePosition - currentPosition;
			camera.transform.position += diff;
			lastMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
		}
		
		
		if(Input.GetMouseButtonDown(1)) {
			lastMousePosition = currentPosition;
		}
		
		if(Input.GetMouseButtonUp(1)) {
			lastMousePosition = Vector3.zero;
		}
	}
}
