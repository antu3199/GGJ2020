using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    public GameObject realObjectPrefab;
    public SpriteRenderer spriteRend;
    public MouseFollowerObject followScript;
    public Color goodPlacementColor;
    public Color badPlacementColor;

    bool canPlace = false;
    // Start is called before the first frame update
    void Start()
    {
        followScript.onClickObject = OnClickObject;

        canPlace = true; // TODO: IMPLEMENT THIS.
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlace) {
          spriteRend.color = goodPlacementColor;
        } else {
          spriteRend.color = badPlacementColor;
        }
    } 

    public void OnClickObject() {
      //TODO: Check if can build object at this location...
      // TODO: Spend resources

      GameObject newObject = Instantiate(realObjectPrefab) as GameObject;
      newObject.transform.position = followScript.transform.position;
      HUDManager.Instance.ResetHUDState();
      Destroy(this.gameObject);
    }
}
