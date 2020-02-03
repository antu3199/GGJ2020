using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garrison : MonoBehaviour
{
	public List<DraggableUnit> units;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        units = new List<DraggableUnit>();
    }

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Unit")){
			DraggableUnit incoming = other.gameObject.GetComponent<DraggableUnit>();
			if(transform.position.y < other.transform.position.y && !incoming.isDragged()){
				units.Add(incoming);
				//incoming.gameObject.SetActive(false);
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Unit")){
			DraggableUnit outgoing = other.gameObject.GetComponent<DraggableUnit>();
			if (units.Contains(outgoing)){
				units.Remove(outgoing);
			}
		}
	}

	void OnMouseEnter(){
		Camera.main.GetComponent<MouseBehaviour>().PointedAtObject(gameObject);
	}

	public void Purge(){
		foreach (DraggableUnit unit in units){
			unit.gameObject.SetActive(true);
		}
		units.Clear();
	}
}
