using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAggro : MonoBehaviour
{
    List<GameObject> targets;
    GameObject currentTarget;
    public GameObject aimingPart;
    // Start is called before the first frame update
    void Start()
    {
        targets = new List<GameObject>();
        currentTarget = null;
    }

    void Update()
    {
        if (currentTarget)
        {
            aimingPart.transform.eulerAngles =  new Vector3(0, 0, PointToTarget(currentTarget));
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
        targets.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") || !targets.Contains(other.gameObject))
        {
            return;
        }
        targets.Remove(other.gameObject);
    }

    void SortTargets()
    {
        return;
    }

    List<GameObject> GetTargets()
    {
        return targets;
    }

    float PointToTarget(GameObject target)
    {
        Vector2 diff = target.transform.position - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Acos(diff.x / diff.magnitude);
        return diff.y < 0 ? -angle : angle;
    }
}
