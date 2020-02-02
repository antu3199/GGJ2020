using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
    public Transform[] m_cloudSets;
    public float m_scrollSpeed;
    public float m_parallaxFactor;
    [Tooltip("Distance to place cloud sets apart.")]
    public float m_cloudSetDistance;
    private CameraControl camControl;
    private float m_currCameraScroll;

    private void Start() {
        camControl = Camera.main.GetComponent<CameraControl>();
    }

    private void Update() {
        foreach (Transform cloudSet in m_cloudSets) {
            cloudSet.position += Vector3.right * (m_scrollSpeed * Time.deltaTime - m_currCameraScroll *  1 / m_parallaxFactor);
            // Move cloud set to left if necessary
            if (cloudSet.position.x >= camControl.rightBound + m_cloudSetDistance) {
                Debug.Log("true");
                cloudSet.position = transform.position - Vector3.right * m_cloudSetDistance * Mathf.Floor(m_cloudSets.Length / 2f);
            }
        }
    }

    public void SetCameraScroll(float value) {
        m_currCameraScroll = value;
    }
}
