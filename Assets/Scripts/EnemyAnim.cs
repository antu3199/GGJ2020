using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnim : MonoBehaviour {
	public class Animation {
		public static String IDLE = "idle";
		public static String WALK = "walk";
		public static String ATTACK = "attack";
	}

	[Tooltip("animator")]
	public AnimationManager m_anim;
	[Tooltip("Aggro component required")]
	public EnemyAggro m_aggro;
	[Tooltip("Grounded component optional")]
	public GroundedCheck m_groundCheck;

	void Update() {
		if(m_aggro.isInCombat()) {
			m_anim.SetAnimation(Animation.ATTACK);
		}
		else if(m_groundCheck.isGrounded()) {
			m_anim.SetAnimation(Animation.WALK);
		}
		else {
			m_anim.SetAnimation(Animation.IDLE);
		}
	}
}
