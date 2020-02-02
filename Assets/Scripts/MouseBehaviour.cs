using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
	enum Action {
		IDLE,
		DRAGGING,
		BUILDING,
		num_types
	}
	public int MouseButtonDrag = 0; // 0 = LMB, 1 = RMB
	public string buildButton = "Fire1";
	GameObject pointing = null;
	Draggable dragging;
	Action action = Action.IDLE;
	BuildableObject toBuild;
	public BuildableObject b;
	Vector2 targetPos;
    // Update is called once per frame
    void Update()
    {		
		if(Input.GetMouseButtonDown(MouseButtonDrag) && pointing != null){
			if (Drag(pointing.GetComponent<Draggable>())){
				action = Action.DRAGGING;
			}
		}
		else if (Input.GetMouseButtonUp(MouseButtonDrag)){
			if (Drop()){
				action = Action.IDLE;
			}
		}
		else if (Input.GetAxis("Jump") > 0){
			BeginBuild(Instantiate(b) as BuildableObject);
		}
    }
	
	bool Drag(Draggable target){
		//TODO add param
		if (dragging != null || target == null){
			return false;
		}
		dragging = target.OnDrag();
		// Disable colliders & gravity
		return dragging != null;
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
	
	public bool BeginBuild(BuildableObject building){
		if (!building){
			return false;
		}
		toBuild = building;
		return true;
	}
	
	public bool EndBuild(){
		toBuild = null;
		action = Action.IDLE;
		return true;
	}


}
