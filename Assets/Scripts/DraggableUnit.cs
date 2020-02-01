using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableUnit : Draggable
{
    private Gatherer m_gatherer;

    protected override void Start() {
        base.Start();
        m_gatherer = GetComponent<Gatherer>(); 
    }

    public override bool OnDrop() {
        m_gatherer.CheckForNodes();
		return base.OnDrop();
    }

    public override bool OnDrag() {
        m_gatherer.StopGathering();
		return base.OnDrag();
    }
}
