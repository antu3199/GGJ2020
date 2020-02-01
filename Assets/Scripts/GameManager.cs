using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[System.Serializable]
	public class EnemyCost {
		public GameObject m_enemy;
		public int m_cost;

		public GameObject getPrefab() {
			return m_enemy;
		}
		public int getCost() {
			return m_cost;
		}
	}

	[Tooltip("Target for monsters to destroy")]
	public GameObject target;
	[Tooltip("Spawn Points set to spawn enemies")]
	public List<EnemySpawner> m_enemySpawners = new List<EnemySpawner>();
	[Tooltip("Enemy prefab with base cost - COST MUST BE ORDERED")]
	public List<EnemyCost> m_enemyCost = new List<EnemyCost>();

	[Tooltip("Seconds to finish a round")]
	public float m_roundSeconds;
	[Tooltip("Time between rounds")]
	public float m_intermissionSeconds;
	[Tooltip("Difficulty Scaling [1 - 5]")]
	public int m_budgetMultiplier;

	private int m_round;
	private int m_countdown;

	void Start() {
		StartCoroutine(intermissionPhase());
	}

	public int getTimer() {
		return m_countdown;
	}

	public int getRound() {
		return m_round;
	}

	public int getBudget() {
		return m_round * m_round * 1000 * m_budgetMultiplier;
	}

	public void consumeBudget() {
		int budget = getBudget();
		while(budget > 0) {
			EnemySpawner spawner = m_enemySpawners[UnityEngine.Random.Range(0, m_enemySpawners.Count)];
			int upperRange = m_enemyCost.Count;
			while(true) {
				upperRange = UnityEngine.Random.Range(0, upperRange);
				EnemyCost enemy = m_enemyCost[upperRange];
				if(enemy.getCost() <= budget) {
					spawner.loadEnemy(enemy.getPrefab());
					budget -= enemy.getCost();
					break;
				}
			}
		}
	}

	IEnumerator intermissionPhase() {
		m_countdown = (int)m_intermissionSeconds;
		while(m_countdown > 0) {
			Debug.Log(m_countdown);
			yield return new WaitForSeconds(1);
			m_countdown--;
		}

		m_round++;
		consumeBudget();
		StartCoroutine(mainPhase());
	}

	IEnumerator mainPhase() {
		// Enable Spawners
		foreach(EnemySpawner spawner in m_enemySpawners) {
			spawner.spawn(target);
		}

		m_countdown = (int)m_roundSeconds;
		while(m_countdown > 0) {
			Debug.Log(m_countdown);
			yield return new WaitForSeconds(1);
			m_countdown--;
		}
		StartCoroutine(intermissionPhase());
	}
}
