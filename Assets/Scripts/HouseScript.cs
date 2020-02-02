using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public List<DraggableUnit> spawnList;
    // Start is called before the first frame update
    void Start()
    {
        DraggableUnit a = Instantiate(spawnList[(int)(Random.value * spawnList.Count)]);
        a.transform.position = new Vector3(transform.position.x, transform.position.y, a.transform.position.z);
    }
}
