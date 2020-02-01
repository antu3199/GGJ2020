using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
	TowerAggro aggro;
	public int attack = 1;
	public float cd = 1.0f;
	public float attackEffectTime = 0.1f;
	SpriteRenderer effect;
    // Start is called before the first frame update
    void Start()
    {
        if ((aggro = GetComponent<TowerAggro>()) == null)
			Debug.LogError("No TowerAggro script on object " + gameObject.name + "!");
		effect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
		effect.enabled = false;
		StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	IEnumerator Attack(){
		while (true){
			while (aggro.currentTarget == null){
				Debug.Log("No enemy");
				yield return null;
			}
			EnemyStats estats = aggro.currentTarget.GetComponent<EnemyStats>();
			estats.takeDamage(attack);
			StartCoroutine(AttackEffect(attackEffectTime));
			Debug.Log("bop");
			yield return new WaitForSeconds(cd);
		}
	}
	
	IEnumerator AttackEffect(float time){
		effect.enabled = true;
		yield return new WaitForSeconds(time);
		effect.enabled = false;
	}
}
