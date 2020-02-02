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

	public class Effect {
		public static String DAMAGE = "damage";
		public static String DEATH = "death";
	}

	[Tooltip("animator")]
	public AnimationManager m_anim;
	[Tooltip("Aggro component required")]
	public EnemyAggro m_aggro;
	[Tooltip("Grounded component optional")]
	public GroundedCheck m_groundCheck;
	[Tooltip("Stat values")]
	public EnemyStats m_stats;

	private int m_lastSeenHp;

	void Start() {
		m_lastSeenHp = m_stats.getHp();
	}

	void Update() {
		// Animations
		if(m_aggro.isInCombat()) {
			m_anim.SetAnimation(Animation.ATTACK);
		}
		else if(m_groundCheck.isGrounded()) {
			m_anim.SetAnimation(Animation.WALK);
		}
		else {
			m_anim.SetAnimation(Animation.IDLE);
		}

		//Effects
		if(m_stats.getHp() == 0) {
			m_lastSeenHp = m_stats.getHp();
			m_anim.SetEffect(Effect.DEATH);
			m_anim.PauseAnimation();
		}
		else if(m_lastSeenHp > m_stats.getHp()) {
			m_lastSeenHp = m_stats.getHp();
			m_anim.SetEffect(Effect.DAMAGE);
		}
	}
}
