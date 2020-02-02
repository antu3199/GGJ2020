using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour {
    public float m_rotateSpeed;

    private void Update() {
        transform.Rotate(0, 0, -m_rotateSpeed * Time.deltaTime);
    }
}
