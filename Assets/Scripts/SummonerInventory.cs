using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SummonerInventory : MonoBehaviour {
	public Dictionary<Resource, int> m_resources = new Dictionary<Resource, int>();

	// Use this for initialization
	void Start () {
		m_resources.Add(Resource.ORE, 0);
		m_resources.Add(Resource.WOOD, 0);
		m_resources.Add(Resource.WHEAT, 0);
		m_resources.Add(Resource.GOLD, 0);
	}
	
	// Update is called once per frame
	void Update () {

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

	public void addResource(Resource resource, int amount) {
		m_resources[resource] += amount;
	}
}
