using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableUnit : Draggable {
    [Tooltip("Reference to the gathering progress bar.")]
    public ProgressBar m_progressBar;
	private UnitStats m_unitStats;
	private UnitInventory m_unitInventory;
    private Animator m_anim;
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
        m_anim = GetComponent<Animator>();
		m_collider = GetComponent<Collider2D>();
		m_dropAreaFilter = new ContactFilter2D(){};
		m_dropAreaFilter.SetLayerMask(m_dropArea);
		m_results = new Collider2D[3];
	}

	public override bool OnDrop() {
		// Check for resource nodes or turrets
		CheckForDropAreas();
		return base.OnDrop();
	}

	public override bool OnDrag() {
		StopTask();
		return base.OnDrag();
	}

	private void CheckForDropAreas() {
		// Check for collisions with DropAreas
		int numHits = m_collider.OverlapCollider(m_dropAreaFilter, m_results);
		if (numHits == 1) {
			UseDropArea(m_results[0].GetComponent<DropArea>());
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
		}
	}

	private void UseDropArea(DropArea dropArea) {
		if (dropArea is ResourceNode) {
			StartCoroutine(GatherNode(dropArea.GetComponent<ResourceNode>()));
		} /*else if (dropArea is ) {
			// TODO dropping on turrets
		}*/
	}

	private void StopTask() {
        // m_anim.SetTrigger("Idle");
		m_gathering = false;
        m_progressBar.gameObject.SetActive(false);
		// TODO: remove from turret
	}

	private IEnumerator GatherNode(ResourceNode node) {
		Debug.Log("Now gathering " + node.resourceType);
        // m_anim.SetTrigger("Gather");
		m_gathering = true;
        m_progressBar.gameObject.SetActive(true);
        m_progressBar.SetResourceIcon(node.resourceType);
        m_progressBar.SetFillAmount(0);
		float gatherProgress = 0;
		GatheringOccupation occupation = (GatheringOccupation) m_unitStats.GetOccupation(GatheringOccupation.GetOccupationFromResource(node.resourceType));
		while (m_gathering) {
			gatherProgress += occupation.GetGatherSpeed() * Time.deltaTime;
			int amount = (int) gatherProgress;
            if (amount >= 1) {
                if (!m_unitInventory.inventoryFull()) {
                    if (m_unitInventory.tryAddResource(node.resourceType, amount)) {
                        occupation.GiveGatherExp(amount);
                    }
                    gatherProgress -= amount;
                    Debug.Log(node.resourceType + " gathered. unit now has " + m_unitInventory.getResource(node.resourceType) + "\ngather speed is " + occupation.GetGatherSpeed() + ". exp is " + occupation.getCurrentExp());
                } else {
                    // If inventory is full, cap gathering progress
                    gatherProgress = 1;
                }
            }
            m_progressBar.SetFillAmount(gatherProgress);
			yield return null;
		}
	}
}
