using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour {
	[Tooltip("Aggro component required")]
	public EnemyAggro m_aggro;
	[Tooltip("1 - Can move along axis, 0 - Cannot move along axis")]
	public Vector2 m_availableDir;

	public float m_avgSpeed;
	public float m_jumpForce;
	[Tooltip("Chance to trigger a jump: [0 - 1]")]
	public float m_jumpChance;
	[Tooltip("Variance of action intensity (x - lower, y - upper)")]
	public Vector2 m_variance;

	private Rigidbody2D m_rb;

	void Start() {
		m_rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		// Attacking
		if(m_aggro.isInCombat() != null) {
			m_rb.velocity = Vector2.zero;
		}
		// Moving
		else {
			Vector2 dirToMove = m_aggro.getMovingDirection();
			m_rb.velocity = new Vector2(dirToMove.x * m_availableDir.x, dirToMove.y * m_availableDir.y).normalized * m_avgSpeed * UnityEngine.Random.Range(m_variance.x, m_variance.y);

			if(m_jumpForce > 0 && UnityEngine.Random.Range(0f, 1f) <= m_jumpChance) {
				m_rb.AddForce(new Vector2(0, m_jumpForce));
			}
		}
	}
}
