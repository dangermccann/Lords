using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace Lords {
	public class CameraControl : MonoBehaviour {

		public float intertiaTravelTime = 0.9f;
		public float inertiaSpeed = 0.25f;
		public float minZoom = 2;
		public float maxZoom = 7.5f;
		public float pinchZoomAmount = 0.5f;
		float dragThreshold = 0.01f;

		public static bool IsDragging { get; protected set; }

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
			if(HoverControl.IsOverUI()) {
				return;
			}

			UpdateZoom();
			UpdatePosition();
		}

		void UpdateZoom() {
			float zoomChange = 0f;
			float zoom = Input.GetAxis("Mouse ScrollWheel");

			if(Input.GetKey(KeyCode.PageUp) || Input.GetKey(KeyCode.Q)) {
				zoom += 1 * Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.PageDown) || Input.GetKey(KeyCode.E)) {
				zoom -= 1 * Time.deltaTime;
			}

			if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
				zoom = zoom * 0.1f;
			}

			if (zoom != 0) {
				zoomChange = -1 * zoom * mainCamera.orthographicSize / 2;
			}

			// TODO: test this
			if (Input.touchCount == 2) {
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);
				
				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				
				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
				
				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				zoomChange = mainCamera.orthographicSize + (deltaMagnitudeDiff * pinchZoomAmount);
			}

			if(zoomChange != 0) {
				mainCamera.orthographicSize = Mathf.Max(Mathf.Min(mainCamera.orthographicSize + zoomChange, maxZoom), minZoom);
			}
		}

		void UpdatePosition() {
			Vector3 currentPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

			if(Input.GetMouseButtonDown(0)) {
				lastMousePosition = currentPosition;
				currentVelocity = previousVelocity = inertiaTime = 0;
			}

			if(lastMousePosition != Vector3.zero) {
				Vector3 diff = lastMousePosition - currentPosition;

				if(diff.magnitude > dragThreshold) {
					IsDragging = true;
				}

				MoveCamera(mainCamera.transform.position + diff);

				previousVelocity = currentVelocity;
				currentVelocity = Mathf.Abs(diff.magnitude) / Time.deltaTime;
				inertiaVector = diff.normalized;

				lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			}
			else if(inertiaTime > 0) {
				float magnitude = (currentVelocity * 0.8f + previousVelocity * 0.2f) * Time.deltaTime * inertiaSpeed;
				Vector3 diff = inertiaVector * magnitude * Mathf.Lerp(0, 1, Mathf.Pow(inertiaTime / intertiaTravelTime, 2));
				MoveCamera(mainCamera.transform.position + diff);
				inertiaTime -= Time.deltaTime;
			}
			
			if(Input.GetMouseButtonUp(0)) {
				IsDragging = false;

				lastMousePosition = Vector3.zero;

				if(currentVelocity > 0) {
					inertiaTime = intertiaTravelTime;
				}
			}

			// keyboard shortcuts
			if(IsDragging == false) {
				Vector3 keyDiff = new Vector3(0, 0, 0);
				if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
					keyDiff.y += 2 * Time.deltaTime;
				}
				if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
					keyDiff.y += -2 * Time.deltaTime;
				}
				if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					keyDiff.x += -2 * Time.deltaTime;
				}
				if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					keyDiff.x += 2 * Time.deltaTime;
				}

				if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
					keyDiff = keyDiff * 0.1f;
				}

				if(keyDiff != Vector3.zero) {
					MoveCamera(mainCamera.transform.position + keyDiff);
				}
			}

		}

		void MoveCamera(Vector3 dest) {
			if(Game.CurrentLevel == null) {
				return;
			}

			float mapRadius = Game.CurrentLevel.mapConfiguration.Radius;

			Vector3 left = mainCamera.WorldToScreenPoint(new Vector3(-1 * mapRadius, 0, 0));
			Vector3 right = mainCamera.WorldToScreenPoint(new Vector3(mapRadius, 0, 0));
			float boardWidth = right.x - left.x;

			float vertExtent = mainCamera.orthographicSize;
			float horzExtent = vertExtent * Screen.width / Screen.height;
			float minX = horzExtent - mapRadius;
			float maxX = mapRadius - horzExtent;
			float minY = vertExtent - mapRadius;
			float maxY = mapRadius - vertExtent;

			Vector3 v3 = mainCamera.transform.position;

			if(boardWidth < Screen.width) {
				v3.x = 0;
			}
			else {
				v3.x = Mathf.Clamp(dest.x, minX, maxX);
			}

			v3.y = Mathf.Clamp(dest.y, minY, maxY);
			mainCamera.transform.position = v3;

		}
	}
}
