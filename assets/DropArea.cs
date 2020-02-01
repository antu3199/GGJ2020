using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
	MouseBehaviour mouse;
    // Start is called before the first frame update
    void Start()
    {
		mouse = Camera.main.GetComponent<MouseBehaviour>();
    }
	
	public bool Drop(Draggable thing){
		// TODO
		// Debug.Log("Dropped a " + thing.gameObject.name + " into " + this.gameObject.name);
		return true;
	}
}
