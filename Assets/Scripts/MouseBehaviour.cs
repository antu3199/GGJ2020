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
    [SerializeField]
	GameObject pointing = null;
    [SerializeField]
	Draggable dragging;
	Action action = Action.IDLE;
	BuildableObject toBuild;
	Vector2 targetPos;
    public int dontClick;
    // Update is called once per frame
    void Start()
    {
        dontClick = LayerMask.GetMask("DropArea", "Tower");
    }
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
        if (action == Action.DRAGGING && !Input.GetMouseButton(MouseButtonDrag))
        {
            if (Drop())
            {
                action = Action.IDLE;
            }
            else
            {
                Debug.Log("Can't drop");
            }
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
		if (thing != null && ((1 << thing.layer) & dontClick) == 0) {
			pointing = thing;
			Debug.Log("Pointed at " + thing.name);
		}
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
		Destroy(toBuild.gameObject);
		toBuild = null;
		action = Action.IDLE;
		return true;
	}
}
