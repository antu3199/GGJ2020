using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
	TowerAggro aggro;
	public int attack = 1;
	public float cd = 2.0f;
	public float attackEffectTime = 0.1f;
	public bool manned = false;
	SpriteRenderer effect;
	TowerGarrison garrison;
    // Start is called before the first frame update
    void Start()
    {
        if ((aggro = GetComponent<TowerAggro>()) == null)
			Debug.LogError("No TowerAggro script on object " + gameObject.name + "!");
		if ((garrison = GetComponent<TowerGarrison>()) == null){
			Debug.LogError("No TowerGarrison script on object" + gameObject.name + "!");
		}
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
			while (!manned){
				Debug.Log("No personnel");
				yield return null;
			}
			while (aggro.currentTarget == null){
				Debug.Log("No enemy");
				yield return null;
			}
			EnemyStats estats = aggro.currentTarget.GetComponent<EnemyStats>();
			estats.takeDamage(getAttack());
			StartCoroutine(AttackEffect(attackEffectTime));
			Debug.Log("bop");
			yield return new WaitForSeconds(getCooldown());
		}
	}
	
	IEnumerator AttackEffect(float time){
		effect.enabled = true;
		yield return new WaitForSeconds(time);
		effect.enabled = false;
	}
	
	public int getAttack(){
		return garrison.GetTotalAttack(attack);
	}
	
	public float getCooldown(){
		return garrison.GetTotalCooldown(cd);
	}
}
