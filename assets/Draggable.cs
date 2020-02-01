using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
	bool dragged;
	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
		dragged = false;
		rb = GetComponent<Rigidbody2D>();
    }
	
	void Update(){
		if (dragged){
			Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPos.z = transform.position.z;
			transform.position = newPos;
		}
	}

	public bool OnDrop(){
		//TODO add dropped object into params & interact with dropped object
		//Debug.Log("Aah");
		dragged = false;
		rb.isKinematic = false;
		return true;
	}
	
	public bool OnDrag(){
		//Debug.Log("Ooh");
		dragged = true;
		rb.isKinematic = true;
		return true;
	}
	
	void OnMouseEnter(){
		//Debug.Log("Being pointed at");
		Camera.main.GetComponent<MouseBehaviour>().PointedAtObject(gameObject);
	}
	
	void OnMouseExit(){
		//Debug.Log("No longer being pointed at");
		Camera.main.GetComponent<MouseBehaviour>().PointedAtObject(null);
	}
}
