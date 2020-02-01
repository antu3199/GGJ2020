using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseStatsBehaviour : MonoBehaviour {
	public int m_totalHp;
	public int m_currentHp;

	public void raiseTotalHp(int amount) {
		m_totalHp += amount;
	}

	public int getHp() {
		return m_currentHp;
	}

	public void heal(int amount) {
		m_currentHp = Math.Min(m_currentHp + amount, m_totalHp);
	}

	public void takeDamage(int amount) {
		m_currentHp = Math.Max(m_currentHp - amount, 0);
	}
}
