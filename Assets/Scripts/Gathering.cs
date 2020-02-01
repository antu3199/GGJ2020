using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : MonoBehaviour {
    private Collider2D m_collider;
    public LayerMask m_resourceNodes;
    private ContactFilter2D m_resourceFilter;
    private Collider2D[] m_results;

    private void Start() {
        m_collider = GetComponent<Collider2D>();
        m_resourceFilter = new ContactFilter2D(){};
        m_resourceFilter.SetLayerMask(m_resourceNodes);
        m_results = new Collider2D[3];
    }

    private void Update() {
        CheckForNodes();
    }

    public void CheckForNodes() {
        // Check all nodes touching this gatherer
        int numHits = m_collider.OverlapCollider(m_resourceFilter, m_results);
        if (numHits == 1) {
            StartCoroutine(GatherNode(m_results[0].GetComponent<ResourceNode>()));
        } else if (numHits > 1) {
            // If this gatherer is touching more than one node, gather the closest one
            Collider2D closestNode = m_results[0];
            float closestDistance = Vector2.Distance(closestNode.transform.position, transform.position);
            for (int i = 1; i < numHits; i++) {
                float currDistance = Vector2.Distance(m_results[i].transform.position, transform.position);
                if (currDistance < closestDistance) {
                    closestNode = m_results[i];
                    closestDistance = currDistance;
                }
            }
            StartCoroutine(GatherNode(closestNode.GetComponent<ResourceNode>()));
        }
    }

    private IEnumerator GatherNode(ResourceNode node) {
        Debug.Log(node.resourceType);
        yield return null;
    }
}
