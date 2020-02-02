using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapelBlastAnimator : MonoBehaviour
{
    public Transform circle;

    public float baseScale = 0.5f;
    public  float amp = 0.5f;

    public float speed = 1f;

    public float timeBeforeExplosion = 5f;

    float curTime = 0;

    bool isCharging = true;
    float explosionTimer = 0;
    public float explosionSize = 3f;
    public float explosionSpeed = 20f;
    
    public Color chargingColor;
    public Color explosionColor;
    public SpriteRenderer explosionRend;
    public CircleCollider2D explCollider;

    // Start is called before the first frame update
    void Start()
    {
        explosionRend.color = chargingColor;
    }

    // Update is called once per frame
    void Update()
    {
        explCollider.enabled = !isCharging;


       if (isCharging) {
          explosionTimer += Time.deltaTime;
          curTime += Time.deltaTime * speed;
          float circleSize = baseScale + Mathf.Sin(curTime) * amp;
          circle.localScale = new Vector3(circleSize, circleSize, circle.localScale.z);
          if (explosionTimer >= timeBeforeExplosion) {
            isCharging = false;
            explosionTimer = 0;
            curTime = 0;
            explosionRend.color = explosionColor;
          }
       } else {
         explosionTimer += Time.deltaTime;
         float circleSize = explosionTimer * explosionSpeed;
         circle.localScale = new Vector3(circleSize, circleSize, circle.localScale.z);
         if (circleSize >= explosionSize) {
           isCharging = true;
           explosionTimer = 0;
           curTime = 0;
           explosionRend.color = chargingColor;
           float circleSize2 = baseScale + Mathf.Sin(curTime) * amp;
           circle.localScale = new Vector3(circleSize2, circleSize2, circle.localScale.z);
         }
       }
    }

    void OnTriggerStay2D(Collider2D other) {
      Debug.Log(other.gameObject.name);
      if ( other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
        other.GetComponent<EnemyStats>().takeDamage(1);
      }
    }
}
