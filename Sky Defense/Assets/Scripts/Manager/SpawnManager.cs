using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float timeWaitStart;
    [SerializeField] private Vector2 timeSpawn;
    [SerializeField] private float timeRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeStart());
    }

    public void Spawner()
    {
        GameObject enemy =  Instantiate(enemies[Random.Range(0, enemies.Length)]);//Sinh ra quái ngẫu nhiên
        enemy.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + Random.Range(-2f, 2f), 0);
        if (enemy.GetComponent<Transform>().position.x > 0)
        {
            enemy.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeWaitStart);
        InvokeRepeating("Spawner", timeRate, Random.Range(timeSpawn.x, timeSpawn.y));//Gọi Hàm Spawer sau 1 khoảng thời gian
    }
}
