using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStats : BaseStatsBehaviour {
    private Dictionary<OccupationType, Occupation> m_occupations;

    private void Start() {
        m_occupations = new Dictionary<OccupationType, Occupation>();
		m_occupations.Add(OccupationType.VILLAGER, new Villager());
		m_occupations.Add(OccupationType.MINER, new Miner());
		m_occupations.Add(OccupationType.LUMBERJACK, new Lumberjack());
		m_occupations.Add(OccupationType.FARMER, new Farmer());
		m_occupations.Add(OccupationType.BUILDER, new Builder());
		m_occupations.Add(OccupationType.GUARD, new Guard());
    }

    public Occupation GetOccupation(OccupationType occupationType) {
        Occupation ret = m_occupations[occupationType];
        if (ret == null) {
            Debug.LogWarning("Unit missing " + occupationType + " Occupation");
        }
        return m_occupations[occupationType];
    }
}
