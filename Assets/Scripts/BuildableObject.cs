using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildableObject : MonoBehaviour
{	
	[Serializable]
	public class Requirement {
		public Resource m_resource;
		public int m_cost;
	}

    public GameObject realObjectPrefab;
    public SpriteRenderer spriteRend;
    public Color goodPlacementColor;
    public Color badPlacementColor;
    public List<Requirement> m_requirements = new List<Requirement>();

    public Dictionary<Resource, int> m_costToBuild = new Dictionary<Resource, int>();

	MouseBehaviour mouse;
	[SerializeField]
	float yOffset = 0;
	float xExtent = 0;
	[SerializeField]
    bool canPlace = false;

    void Awake() {
    	foreach(Requirement req in m_requirements) {
    		m_costToBuild.Add(req.m_resource, req.m_cost);
    	}
    }

    // Start is called before the first frame update
    void Start()
    {
		mouse = Camera.main.GetComponent<MouseBehaviour>();
		yOffset = spriteRend.bounds.extents.y;
		xExtent = spriteRend.bounds.extents.x;
        Debug.Log(yOffset);
    }

    // Update is called once per frame
    void Update()
    {		
		UpdatePosition();
        if (canPlace) {
			spriteRend.color = goodPlacementColor;
			if (Input.GetMouseButtonDown(0)) {
				OnClickObject();
			}
        } else {
          spriteRend.color = badPlacementColor;
        }
		
    } 
	
	void FixedUpdate() {

	}

    void OnClickObject() {
		//TODO: Check if can build object at this location...
		// TODO: Spend resources
		if(!SummonerInventory.Instance.tryConsumeResources(m_costToBuild)) {
			return;
		}
		
		GameObject newObject = Instantiate(realObjectPrefab) as GameObject;
		newObject.transform.position = transform.position;
		HUDManager.Instance.ResetHUDState();
    }
	
	void SetCanPlace(bool place){
		canPlace = place;
	}
	
	Vector2 FindGroundBelow(Vector2 pos){
		int groundMask = LayerMask.GetMask("Ground");
		RaycastHit2D ray = Physics2D.Raycast(pos, Vector2.down, Mathf.Infinity, groundMask);
		return ray.point;
	}
	
	bool CheckSpace(Vector2 pos) {
		pos.x -= 2 * xExtent;
		int towerMask = LayerMask.GetMask("Tower", "Unit", "Enemy", "DropArea");
		RaycastHit2D ray = Physics2D.Raycast(pos, Vector2.right, xExtent * 2, towerMask);
		return ray.collider == null;
	}

    void UpdatePosition() {
		Vector3 newPos = FindGroundBelow(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		if (newPos == Vector3.zero){
			newPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			canPlace = false;
		}
		else{					
			//newPos.y += yOffset;
			newPos.z = 0;
			
			if (CheckSpace(newPos)){
				canPlace = true;
			}
			else {
				newPos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
				canPlace = false;
			}
		}
		transform.position = newPos;
    }
}
