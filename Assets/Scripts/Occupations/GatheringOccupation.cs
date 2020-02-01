using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringOccupation : Occupation {

    // Exp gained per 1 resource gathered
    public int expPerGather = 1;
    public static AnimationCurve gatherSpeedScaling = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 2.0f);

	public static OccupationType GetOccupationFromResource(Resource resource) {
		switch(resource) {
			case (Resource.ORE):
				return OccupationType.MINER;
			case (Resource.WOOD):
				return OccupationType.LUMBERJACK;
			case (Resource.WHEAT):
				return OccupationType.FARMER;
		}
        Debug.Log(".");
		return OccupationType.VILLAGER;
	}

    public float GetGatherSpeed() {
        return gatherSpeedScaling.Evaluate((float) m_level / getMaxLevel());
    }

    public void GiveGatherExp(int numResources) {
        giveExp(numResources * expPerGather);
    }
}
