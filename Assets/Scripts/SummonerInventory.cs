using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SummonerInventory : MonoBehaviour {
	[Serializable]
	public class ResourceStack {
		public Text m_ui;
		public int m_count;
	}

	[Serializable]
	public class Entry {
		public Resource m_resource;
		public ResourceStack m_stack;
	}

	public List<Entry> m_entries = new List<Entry>();
	private Dictionary<Resource, ResourceStack> m_resources = new Dictionary<Resource, ResourceStack>();

	void Start() {
		foreach (Entry entry in m_entries) {
			m_resources.Add(entry.m_resource, entry.m_stack);
			updateResource(entry.m_resource, 0);
		}
	}

	public int getResource(Resource resource) {
		if(m_resources.ContainsKey(resource)) {
			return m_resources[resource].m_count;
		}

		else {
			return 0;
		}
	}

	private void updateResource(Resource resource, int amount) {
		m_resources[resource].m_count += amount;
		m_resources[resource].m_ui.text = m_resources[resource].m_count.ToString();
	}

	public bool tryConsumeResource(Resource resource, int amount) {
		if(!m_resources.ContainsKey(resource) || m_resources[resource].m_count < amount) {
			return false;
		}

		updateResource(resource, -1*amount);
		return true;
	}

	public bool tryConsumeResources(Dictionary<Resource, int> requirements) {
		foreach(KeyValuePair<Resource, int> resource in requirements) {
			if(!m_resources.ContainsKey(resource.Key) || m_resources[resource.Key].m_count < resource.Value) {
				return false;
			}
		}

		foreach(KeyValuePair<Resource, int> resource in requirements) {
			updateResource(resource.Key, -1*resource.Value);
		}

		return true;
	}

	public void addResource(Resource resource, int amount) {
		updateResource(resource, amount);
	}
}
