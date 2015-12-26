using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float intertiaTravelTime = 0.9f;
	public float minZoom = 2;
	public float maxZoom = 7.5f;
	public float mapRadius = 10;

	Vector3 lastMousePosition = Vector3.zero; 
	float currentVelocity, previousVelocity, inertiaTime;
	Vector3 inertiaVector;

	public Camera mainCamera;

	void Start () {
		if(mainCamera == null) {
			mainCamera = Camera.main;
		}
	}

	void Update () {
		if(EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

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
			mainCamera.orthographicSize = Mathf.Max(Mathf.Min(mainCamera.orthographicSize + zoomChange, maxZoom), minZoom);
		}
	}

	void UpdatePosition() {
		Vector3 currentPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButtonDown(1)) {
			lastMousePosition = currentPosition;
			currentVelocity = previousVelocity = inertiaTime = 0;
		}

		if(lastMousePosition != Vector3.zero) {
			Vector3 diff = lastMousePosition - currentPosition;
			MoveCamera(mainCamera.transform.position + diff);

			previousVelocity = currentVelocity;
			currentVelocity = Mathf.Abs(diff.magnitude) / Time.deltaTime;
			inertiaVector = diff.normalized;

			lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}
		else if(inertiaTime > 0) {

			float magnitude = (currentVelocity * 0.8f + previousVelocity * 0.2f) * Time.deltaTime;
			Vector3 diff = inertiaVector * magnitude * Mathf.Lerp(0, 1, Mathf.Pow(inertiaTime / intertiaTravelTime, 2));
			MoveCamera(mainCamera.transform.position + diff);
			inertiaTime -= Time.deltaTime;
		}
		
		if(Input.GetMouseButtonUp(1)) {
			lastMousePosition = Vector3.zero;

			if(currentVelocity > 0) {
				inertiaTime = intertiaTravelTime;
			}
		}
	}

	void MoveCamera(Vector3 diff) {
		float vertExtent = Camera.main.camera.orthographicSize;
		float horzExtent = vertExtent * Screen.width / Screen.height;
		float minX = horzExtent - mapRadius;
		float maxX = mapRadius - horzExtent;
		float minY = vertExtent - mapRadius;
		float maxY = mapRadius - vertExtent;

		Vector3 v3 = mainCamera.transform.position;
		v3.x = Mathf.Clamp(diff.x, minX, maxX);
		v3.y = Mathf.Clamp(diff.y, minY, maxY);
		mainCamera.transform.position = v3;

	}
}
