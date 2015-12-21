using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	Vector3 lastMousePosition = Vector3.zero; 
	public Camera mainCamera;

	void Start () {
		if(mainCamera == null) {
			mainCamera = Camera.main;
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
			zoomChange = -1 * mainCamera.orthographicSize / 10;
		}
		else if (zoom < 0) {
			zoomChange = mainCamera.orthographicSize / 10;
		}
		
		if(zoomChange != 0) {
			mainCamera.orthographicSize = Mathf.Max(Mathf.Min(mainCamera.orthographicSize + zoomChange, 30), 3);
		}
	}

	void UpdatePosition() {
		Vector3 currentPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		if(lastMousePosition != Vector3.zero) {
			Vector3 diff = lastMousePosition - currentPosition;
			mainCamera.transform.position += diff;
			lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}
		
		
		if(Input.GetMouseButtonDown(1)) {
			lastMousePosition = currentPosition;
		}
		
		if(Input.GetMouseButtonUp(1)) {
			lastMousePosition = Vector3.zero;
		}
	}
}
