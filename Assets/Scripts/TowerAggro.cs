using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAggro : MonoBehaviour
{
    List<EnemyStats> targets;
    public EnemyStats currentTarget;
    public GameObject aimingPart;
    // Start is called before the first frame update
    void Start()
    {
        targets = new List<EnemyStats>();
        currentTarget = null;
		if (aimingPart == null){
			if ((aimingPart = transform.Find("Aiming Part").gameObject) == null)
				Debug.Log("No aiming part found");
		}
    }

    void Update()
    {
        if (currentTarget)
        {
            aimingPart.transform.eulerAngles =  new Vector3(0, 0, PointToTarget(currentTarget.transform));
        }
        else
        {
            targets.Remove(currentTarget);
            currentTarget = null;
        }
    }

    void FixedUpdate()
    {
        if(targets.Count == 0)
        {
            currentTarget = null;
        }
        else
        {
            currentTarget = targets[0];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter thing");
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            return;
        }
		EnemyStats toAdd = other.GetComponent<EnemyStats>();
		if (toAdd != null)
			targets.Add(toAdd);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            return;
        }
		EnemyStats toRemove = other.GetComponent<EnemyStats>();
		if (toRemove)
			targets.Remove(toRemove);
		
    }

    void SortTargets()
    {
        return;
    }

    List<EnemyStats> GetTargets()
    {
        return targets;
    }

    float PointToTarget(Transform target)
    {
        Vector2 diff = target.position - aimingPart.transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Acos(diff.x / diff.magnitude);
        return diff.y < 0 ? -angle : angle;
    }
}
