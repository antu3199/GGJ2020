using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
	bool dragged;
	Rigidbody2D rb;
	Collider2D[] colls;
    // Start is called before the first frame update
    protected virtual void Start()
    {
		dragged = false;
		rb = GetComponent<Rigidbody2D>();
		colls = GetComponents<Collider2D>();
    }
	
	void Update(){
		if (dragged){
			Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPos.z = transform.position.z;
			transform.position = newPos;
		}
	}

	public virtual bool OnDrop(){
		//TODO add dropped object into params & interact with dropped object
		//Debug.Log("Aah");
		dragged = false;
		rb.isKinematic = false;
		foreach (Collider2D c in colls){
			c.enabled = true;
		}
		return true;
	}
	
	public virtual Draggable OnDrag(){
		//Debug.Log("Ooh");
		dragged = true;
		rb.isKinematic = true;
		foreach (Collider2D c in colls){
			c.enabled = false;
		}
		return this;
	}
	
	void OnMouseEnter(){
		//Debug.Log("Being pointed at");
		Camera.main.GetComponent<MouseBehaviour>().PointedAtObject(gameObject);
	}
	
	void OnMouseExit(){
		//Debug.Log("No longer being pointed at");
		Camera.main.GetComponent<MouseBehaviour>().PointedAtObject(null);
	}

    void OnDisable()
    {
        foreach(Collider2D c in colls)
        {
            c.enabled = false;
        }
    }
	
	public bool isDragged(){
		return dragged;
	}
}
