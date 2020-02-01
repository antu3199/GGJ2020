using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Queue<GameObject> m_enemies = new Queue<GameObject>();
	public float m_spawnInterval;

	public void loadEnemy(GameObject enemy) {
		m_enemies.Enqueue(enemy);
	}

	public void setSpawnInterval(float interval) {
		m_spawnInterval = interval;
	}

	public void spawn(GameObject target) {
		StartCoroutine(SpawnWave(target));
	}

	IEnumerator SpawnWave(GameObject target) {
		while(m_enemies.Count > 0) {
			GameObject obj = Instantiate(m_enemies.Dequeue(), transform.position, Quaternion.identity);
			obj.GetComponent<EnemyAggro>().setTarget(target.transform);
			yield return new WaitForSeconds(m_spawnInterval);
		}
	}
}
