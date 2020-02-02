using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : DropArea {
    public Resource resourceType;
    public int m_startQuantity;
    private int m_currQuantity;
    private List<DraggableUnit> m_gatherers;
    
    private void Start() {
        m_currQuantity = m_startQuantity;
        m_gatherers = new List<DraggableUnit>();
    }

    public void AddGatherer(DraggableUnit unit) {
        m_gatherers.Add(unit);
    }
    public void RemoveGatherer(DraggableUnit unit) {
        m_gatherers.Remove(unit);
    }

    public int Harvest(int amount) {
        if (amount < m_currQuantity) {
            m_currQuantity -= amount;
            return amount;
        } else {
            int remaining = m_currQuantity;
            m_currQuantity = 0;
            DepleteNode();
            return remaining;
        }
    }

    private void DepleteNode() {
        for (int i = m_gatherers.Count - 1; i >= 0; i--) {
            m_gatherers[i].StopTask();
        }
        Destroy(gameObject);
    }
}
