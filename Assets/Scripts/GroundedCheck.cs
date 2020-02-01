using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundedCheck : MonoBehaviour {
	[Tooltip("Pivot to ground distance")]
	public float m_distToGround;
	[Tooltip("Margin of error allowed")]
	public float m_marginalError;
	[Tooltip("Width from Pivot")]
	public float m_width;

	private bool m_currentlyGrounded;

	void Update() {
		Vector3 left_pos = new Vector3(transform.position.x - m_width, transform.position.y, transform.position.z);
		Vector3 right_pos = new Vector3(transform.position.x + m_width, transform.position.y, transform.position.z);
		m_currentlyGrounded = (Physics2D.Raycast(left_pos, -Vector2.up, m_distToGround + m_marginalError)
			| Physics2D.Raycast(right_pos, -Vector2.up, m_distToGround + m_marginalError));
	}

	public bool isGrounded() {
		return m_currentlyGrounded;
	}
}
