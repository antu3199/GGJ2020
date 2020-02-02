using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
    public Transform[] m_cloudSets;
    public float m_scrollSpeed;
    [Tooltip("Value that dampens the effect of the camera scroll on the clouds' movement.")]
    public float m_parallaxFactor;
    [Tooltip("Distance to place cloud sets apart.")]
    public float m_cloudSetDistance;
    private CameraControl camControl;
    private float m_currCameraScroll;

    private void Start() {
        camControl = Camera.main.GetComponent<CameraControl>();
        if (camControl == null) {
            Debug.LogWarning("CameraControl not found. Must be enabled for cloud parallaxing.");
        }
    }

    private void Update() {
        for (int i = m_cloudSets.Length - 1; i >= 0; i--) {
            m_cloudSets[i].position += Vector3.right * (m_scrollSpeed * Time.deltaTime - m_currCameraScroll * -m_parallaxFactor);
            // Loop cloud set back to left if necessary
            if (m_cloudSets[i].position.x >= camControl.rightBound + m_cloudSetDistance * m_cloudSets.Length / 2) {
                Transform nextSet = m_cloudSets[(i + 1) % m_cloudSets.Length];
                m_cloudSets[i].position = nextSet.position - Vector3.right * m_cloudSetDistance;
            }
        }
    }

    public void SetCameraScroll(float value) {
        m_currCameraScroll = value;
    }
}
