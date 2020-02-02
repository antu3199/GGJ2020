using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	
	[Tooltip("The leftmost the camera can move on the x-axis.")]
	public float leftBound;
	[Tooltip("The rightmost the camera can move on the x-axis.")]
	public float rightBound;
	[Tooltip("The lowest the camera can move on the y-axis.")]
	public float m_lowerBound;
	[Tooltip("The highest the camera can move on the y-axis.")]
	public float m_upperBound;
	[Tooltip("The proximity in pixels of the cursor to the edges of the screen that will make the camera scroll.")]
	public float m_scrollAreaWidth;
	public float m_scrollSpeed;
	private Camera m_cam;
	public CloudController m_cloudController;

	private void Start() {
		m_cam = Camera.main;
	}

	private void Update() {
		Vector3 mousePos = Input.mousePosition;
		Vector3 newPos = m_cam.transform.position;

		// X-axis movement
		if (mousePos.x <= m_scrollAreaWidth) {
			newPos += Vector3.left * m_scrollSpeed * Time.deltaTime;
			newPos.x = Mathf.Max(newPos.x, leftBound);
		} else if (mousePos.x >= Screen.width - m_scrollAreaWidth) {
			newPos += Vector3.right * m_scrollSpeed * Time.deltaTime;
			newPos.x = Mathf.Min(newPos.x, rightBound);
		}

		// Y-axis movement
		if (mousePos.y <= m_scrollAreaWidth) {
			newPos += Vector3.down * m_scrollSpeed * Time.deltaTime;
			newPos.y = Mathf.Max(newPos.y, m_lowerBound);
		} else if (mousePos.y >= Screen.height - m_scrollAreaWidth) {
			newPos += Vector3.up * m_scrollSpeed * Time.deltaTime;
			newPos.y = Mathf.Min(newPos.y, m_upperBound);
		}
		m_cloudController.SetCameraScroll(newPos.x - m_cam.transform.position.x);
		m_cam.transform.position = newPos;
	}
}
