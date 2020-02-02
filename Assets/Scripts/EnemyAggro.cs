using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAggro : MonoBehaviour {
	// Aggroed target
	public Transform m_target;
	public int m_attackDamage;
	public float m_attackDelay;

	// Combat Detection
	public GameObject m_attackable;

	public GameObject isInCombat() {
		return m_attackable;
	}

	public void setTarget(Transform target) {
		m_target = target;
	}

	public Vector2 getMovingDirection() {
		return (m_target.position - transform.position).normalized;
	}

	// Collider Checks
	protected virtual void OnCollisionEnter2D(Collision2D col) {
		BaseStatsBehaviour target = col.gameObject.GetComponent<BaseStatsBehaviour>();
		if(target != null &&
			target.GetType() != typeof(EnemyStats) &&
			target.getHp() > 0) {
			m_attackable = col.gameObject;
			StartCoroutine(attack());
		}
	}

	protected virtual void OnCollisionExit2D(Collision2D col) {
		m_attackable = null;
	}

	//Coroutines
	protected virtual IEnumerator attack() {
		while(m_attackable != null && m_attackable.GetComponent<BaseStatsBehaviour>() != null) {
			m_attackable.GetComponent<BaseStatsBehaviour>().takeDamage(m_attackDamage);
			yield return new WaitForSeconds(m_attackDelay);
		}
	}
}
