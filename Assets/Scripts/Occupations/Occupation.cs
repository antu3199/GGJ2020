using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum OccupationType {
	// Beginner
	VILLAGER,

	// Basic
	MINER,
	LUMBERJACK,
	FARMER,

	// Advanced
	BUILDER,
	GUARD
}

public abstract class Occupation {
	public static int[] EXP_CURVE = new int[] { 5, 20, 40 };

	public OccupationType m_type;
	public int m_level;
	public int m_curExp;

	public OccupationType getOccupation() {
		return m_type;
	}

	public int getLevel() {
		return m_level;
	}

	public int getMaxLevel() {
		return EXP_CURVE.Length;
	}

	public int getCurrentExp() {
		return m_curExp;
	}

	public void giveExp(int amount) {
		m_curExp += amount;
		tryLevelUp();
	}

	public bool tryLevelUp() {
		if(m_level < getMaxLevel() && m_curExp >= getRequiredExpForLevel(m_level)) {
			m_curExp -= getRequiredExpForLevel(m_level);
			m_level += 1;
			return true;
		}

		return false;
	}

	public virtual int getRequiredExpForLevel(int level) {
		return Occupation.EXP_CURVE[level];
	}
}
