using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public float timeSpawn;
    public int countEnemy;
}

public class WaveSpawn : MonoBehaviour
{
    [SerializeField] private EnemyWave[] enemyWaves;

    [SerializeField] private GameObject[] enemyPrefabs;

    void Start()
    {
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeSpawn, enemyWaves[i].countEnemy));//Tạo các wave
        }
    }

    IEnumerator CreateEnemyWave(float delay, int countEnemy)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);//Thời gian chờ tạo wave

        StartCoroutine(SpawnEnemyOnWave(countEnemy));
    }

    private IEnumerator SpawnEnemyOnWave(int countEnemy)
    {
        for(int i = 0; i< countEnemy; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position, Quaternion.identity);//Tạo wave
            enemy.transform.parent = transform;
            yield return new WaitForSeconds(1f);
        }
    }
}
