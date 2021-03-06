﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableUnit : Draggable {
    [Tooltip("Reference to the gathering progress bar.")]
    public ProgressBar m_progressBar;
	private UnitStats m_unitStats;
	private UnitInventory m_unitInventory;
	// public GroundedCheck m_groundCheck;
	private Collider2D m_collider;
    [Tooltip("The layers considered DropAreas.")]
	public LayerMask m_dropArea;
	private ContactFilter2D m_dropAreaFilter;
	private Collider2D[] m_results;
	private bool m_gathering;
	private ResourceNode m_curNode;
	
	protected override void Start() {
		base.Start();
		m_unitStats = GetComponent<UnitStats>();
		m_unitInventory = GetComponent<UnitInventory>();
		m_collider = GetComponent<Collider2D>();
		m_dropAreaFilter = new ContactFilter2D(){};
		m_dropAreaFilter.SetLayerMask(m_dropArea);
		m_results = new Collider2D[10];
	}

	public override Draggable OnDrag() {
		StopTask();
		return base.OnDrag();
	}

    private void OnTriggerEnter2D(Collider2D other) {
        // m_groundCheck.isGrounded
        CheckForDropAreas();
    }

    // Checks for nearby DropAreas and uses them if there are any, returning true if successful, false if there are no DropAreas
	private bool CheckForDropAreas() {
        if (m_collider == null)
            return false;
		int numHits = m_collider.OverlapCollider(m_dropAreaFilter, m_results);
		if (numHits == 1) {
			UseDropArea(m_results[0].GetComponent<DropArea>());
            return true;
		} else if (numHits > 1) {
			// If this gatherer is touching more than one drop area, use the closest one
			Collider2D closestDropArea = m_results[0];
			float closestDistance = Vector2.Distance(closestDropArea.transform.position, transform.position);
			for (int i = 1; i < numHits; i++) {
				float currDistance = Vector2.Distance(m_results[i].transform.position, transform.position);
				if (currDistance < closestDistance) {
					closestDropArea = m_results[i];
					closestDistance = currDistance;
				}
			}
			UseDropArea(closestDropArea.GetComponent<DropArea>());
            return true;
		}
        return false;
	}

	private void UseDropArea(DropArea dropArea) {
		if(isGathering()) {
			return;
		}

		if (dropArea is ResourceNode) {
			StartCoroutine(GatherNode(dropArea.GetComponent<ResourceNode>()));
		} else if (dropArea is Chapel) {
            m_unitInventory.depositResources(dropArea.GetComponent<SummonerInventory>());
        } /*else if (dropArea is ) {
			// TODO dropping on turrets
		}*/
	}

	public void StopTask() {
        // m_anim.SetTrigger("Idle");
		m_gathering = false;
        if (m_curNode != null) {
            m_curNode.RemoveGatherer(this);
            m_curNode = null;
        }
        m_progressBar.gameObject.SetActive(false);
		// TODO: remove from turret
	}

	public bool isGathering() {
		return m_gathering;
	}

	private IEnumerator GatherNode(ResourceNode node) {
		Debug.Log("Now gathering " + node.resourceType);
        // m_anim.SetTrigger("Gather");
		m_gathering = true;
        m_curNode = node;
        node.AddGatherer(this);
        m_progressBar.gameObject.SetActive(true);
        m_progressBar.SetResourceIcon(node.resourceType);
        m_progressBar.SetFillAmount(0);
		float gatherProgress = 0;
		GatheringOccupation occupation = (GatheringOccupation) m_unitStats.GetOccupation(GatheringOccupation.GetOccupationFromResource(node.resourceType));
		while (m_gathering) {
            if (!m_unitInventory.inventoryFull()) {
                gatherProgress += occupation.GetGatherSpeed() * Time.deltaTime;
                int amount = (int) gatherProgress;
                if (amount >= 1) {
                        amount = node.Harvest(1);
                        if (amount == 0) {
                            StopTask();
                            break;
                        }
                        if (m_unitInventory.tryAddResource(node.resourceType, amount)) {
                            occupation.GiveGatherExp(amount);
                        }
                        gatherProgress -= amount;
                        Debug.Log(node.resourceType + " gathered. unit now has " + m_unitInventory.getResource(node.resourceType) + "\ngather speed is " + occupation.GetGatherSpeed() + ". exp is " + occupation.getCurrentExp());
                }
                m_progressBar.SetFillAmount(gatherProgress);
            }
            if (m_unitInventory.inventoryFull()) {
                // If inventory is full, cap gathering progress
                m_progressBar.SetFillAmount(1);
            }
			yield return null;
		}
	}
}
