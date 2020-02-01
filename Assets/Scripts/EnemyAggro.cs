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
	private GameObject m_attackable;

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
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<BaseStatsBehaviour>() != null) {
			m_attackable = col.gameObject;
			StartCoroutine(attack());
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		m_attackable = null;
	}

	//Coroutines
	IEnumerator attack() {
		while(m_attackable != null && m_attackable.GetComponent<BaseStatsBehaviour>() != null) {
			Debug.Log("Attacking!");
			yield return new WaitForSeconds(m_attackDelay);
		}
	}
}
