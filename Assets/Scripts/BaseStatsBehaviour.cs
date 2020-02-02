using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseStatsBehaviour : MonoBehaviour {
	public int m_totalHp;
	public int m_currentHp;
	[Tooltip("Set delay to be destroyed")]
	public int m_delayDestroy;

  public string takeDamageSound = "";
  public int takeDamageSoundVolume = -1;

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
    MusicManager.Instance.PlaySound(takeDamageSound, takeDamageSoundVolume);
	}

	protected virtual void Update() {
		if(m_currentHp <= 0) {
			Destroy(gameObject, m_delayDestroy);
		}
	}
}
