using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
    public Transform[] m_cloudSets;
    public float m_scrollSpeed;
    public float m_parallaxFactor;
    private CameraControl camControl;
    private float m_currCameraScroll;

    private void Start() {
        camControl = Camera.main.GetComponent<CameraControl>();
    }

    private void Update() {
        foreach (Transform cloudSet in m_cloudSets) {
            cloudSet.position += Vector3.right * (m_scrollSpeed * Time.deltaTime - m_currCameraScroll *  1 / m_parallaxFactor);
        }
    }

    public void SetCameraScroll(float value) {
        m_currCameraScroll = value;
    }
}
