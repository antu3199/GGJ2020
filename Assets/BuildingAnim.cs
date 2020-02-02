using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingAnim : MonoBehaviour {
	public class Effect {
		public static String DEATH = "death";
	}

	[Tooltip("animator")]
	public AnimationManager m_anim;
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
			m_anim.SetEffect(Effect.DEATH);
		}
	}
}
