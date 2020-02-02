using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseFollowerObject : MonoBehaviour
{

    public UnityAction onClickObject{get; set;}

    void Start() {
      UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
			UpdatePosition();

      if (Input.GetMouseButtonDown(0)) {
        if (onClickObject != null) {
          onClickObject();
        }
      }
    }


    void UpdatePosition() {
			Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPos.z = transform.position.z;
			transform.position = newPos;
    }
}
