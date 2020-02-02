using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingStats : BaseStatsBehaviour {
	public ProgressBar m_progressBar;

	protected override void Update() {
		base.Update();

		if(m_currentHp != m_totalHp) {
			m_progressBar.gameObject.SetActive(true);
			m_progressBar.SetFillAmount(((float)m_currentHp)/m_totalHp);
		}
	}
}
