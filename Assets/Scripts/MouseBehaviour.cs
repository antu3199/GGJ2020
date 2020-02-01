using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
	public int MouseButtonDrag = 0; // 0 = LMB, 1 = RMB
	GameObject pointing = null;
	Draggable dragging;
    // Update is called once per frame
    void Update()
    {		
		if(Input.GetMouseButtonDown(MouseButtonDrag) && pointing != null){
			Drag(pointing.GetComponent<Draggable>());
		}
		else if (Input.GetMouseButtonUp(MouseButtonDrag)){
			Drop();
		}
    }
	
	bool Drag(Draggable target){
		//TODO add param

		if (dragging != null || target == null){
			return false;
		}
		if (!target.OnDrag()){
			return false;
		}
		dragging = target;
		// Disable colliders & gravity
		return true;
	}
	
	bool Drop(){
		if (dragging == null){
			return false;
		}
		dragging.transform.parent = null;
		// Enable colliders & gravity
		if (dragging.OnDrop()){
			
			dragging = null;
			return true;
		}
		return false;
	}
	
	public void PointedAtObject(GameObject thing){
		pointing = thing;
		if (thing != null)
			Debug.Log("Pointed at " + thing.name);
	}
	
	public bool IsDragging(){
		return dragging != null;
	}
	
	public Draggable GetDragging(){
		return dragging;
	}
}
