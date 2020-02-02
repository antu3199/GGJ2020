using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretAnim : MonoBehaviour {
	public class Effect {
		public static String DEATH = "death";
	}

	[Tooltip("animator for head")]
	public AnimationManager m_animHead;
	[Tooltip("animator for body")]
	public AnimationManager m_animBody;
	[Tooltip("Stat values")]
	public BuildingStats m_stats;

	private int m_lastSeenHp;

	void Start() {
		m_lastSeenHp = m_stats.getHp();
	}

	void Update() {
		//Effects
		if(m_stats.getHp() == 0) {
			m_lastSeenHp = m_stats.getHp();
			m_animHead.SetEffect(Effect.DEATH);
			m_animBody.SetEffect(Effect.DEATH);
		}
	}
}
