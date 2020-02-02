﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    public GameObject realObjectPrefab;
    public SpriteRenderer spriteRend;
    public Color goodPlacementColor;
    public Color badPlacementColor;
	MouseBehaviour mouse;
	[SerializeField]
	float yOffset = 0;
	float xExtent = 0;
	[SerializeField]
    bool canPlace = false;
    // Start is called before the first frame update
    void Start()
    {
		mouse = Camera.main.GetComponent<MouseBehaviour>();
		yOffset = spriteRend.bounds.extents.y;
		xExtent = spriteRend.bounds.extents.x;
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
	
	void FixedUpdate(){
	}

    void OnClickObject() {
      //TODO: Check if can build object at this location...
      // TODO: Spend resources

      GameObject newObject = Instantiate(realObjectPrefab) as GameObject;
	  newObject.transform.position = transform.position;
      HUDManager.Instance.ResetHUDState();
	  mouse.EndBuild();
    }
	
	void SetCanPlace(bool place){
		canPlace = place;
	}
	
	Vector2 FindGroundBelow(Vector2 pos){
		int groundMask = LayerMask.GetMask("Ground");
		RaycastHit2D ray = Physics2D.Raycast(pos, Vector2.down, Mathf.Infinity, groundMask);
		return ray.point;
	}
	
	bool checkSpace(Vector2 pos) {
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
			newPos.y += yOffset;
			newPos.z = 0;
			
			if (checkSpace(newPos)){
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