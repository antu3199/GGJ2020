using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitAnim : MonoBehaviour {
	public class Animation {
		public static String IDLE = "idle";
		public static String GATHER = "gather";
	}

	public class Effect {
		public static String DAMAGE = "damage";
		public static String DEATH = "death";
	}

	[Tooltip("animator")]
	public AnimationManager m_anim;
	[Tooltip("dragging system")]
	public DraggableUnit m_dragSystem;
	[Tooltip("Grounded component optional")]
	public GroundedCheck m_groundCheck;
	[Tooltip("Stat values")]
	public UnitStats m_stats;

	private int m_lastSeenHp;

	void Start() {
		m_lastSeenHp = m_stats.getHp();
	}

	void Update() {
		// Animations
		if(m_dragSystem.isGathering()) {
			m_anim.SetAnimation(Animation.GATHER);
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
