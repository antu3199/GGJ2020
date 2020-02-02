using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitInventory : MonoBehaviour {
	public static int BACKPACK_UPGRADE = 5;

	public int m_resourceLimit;
	public Dictionary<Resource, int> m_resources = new Dictionary<Resource, int>();

	// Use this for initialization
	void Start () {
		m_resources.Add(Resource.ORE, 0);
		m_resources.Add(Resource.WOOD, 0);
		m_resources.Add(Resource.WHEAT, 0);
		m_resources.Add(Resource.GOLD, 0);
	}

	public int countInventory() {
		int count = 0;
		foreach(KeyValuePair<Resource, int> resource in m_resources) {
			count += resource.Value;
		}

		return count;
	}

	public bool inventoryFull() {
		return countInventory() >= m_resourceLimit;
	}

	public void depositResources(SummonerInventory summonerInventory) {
		List<Resource> keys = new List<Resource>(m_resources.Keys);
		foreach(Resource key in keys) {
			if(m_resources[key] > 0) {
				summonerInventory.addResource(key, m_resources[key]);
				m_resources[key] = 0;
			}
		}
	}

	public void upgradeBackpack() {
		m_resourceLimit += UnitInventory.BACKPACK_UPGRADE;
	}

	public bool tryAddResource(Resource resource, int amount) {
		if(countInventory() + amount > m_resourceLimit) {
			return false;
		}

		m_resources[resource] += amount;
		return true;
	}

	public int getResource(Resource resource) {
		if(m_resources.ContainsKey(resource)) {
			return m_resources[resource];
		}

		else {
			return 0;
		}
	}

	public bool tryConsumeResource(Resource resource, int amount) {
		if(!m_resources.ContainsKey(resource) || m_resources[resource] < amount) {
			return false;
		}

		m_resources[resource] -= amount;
		return true;
	}

	public bool tryConsumeResources(Dictionary<Resource, int> requirements) {
		foreach(KeyValuePair<Resource, int> resource in requirements) {
			if(!m_resources.ContainsKey(resource.Key) || m_resources[resource.Key] < resource.Value) {
				return false;
			}
		}

		foreach(KeyValuePair<Resource, int> resource in requirements) {
			m_resources[resource.Key] -= resource.Value;
		}

		return true;
	}
}
