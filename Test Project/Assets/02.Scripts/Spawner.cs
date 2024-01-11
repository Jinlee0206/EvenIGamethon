using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    public int currentWave;
    public int maxWave = 20;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        InvokeRepeating("IncreaseWaveAndWaveStart", 0f, GameManager.Inst.waveChangeTime);
    }

    private void Update()
    {
        float gameTime = GameManager.Inst.gameTime;
        if (currentWave >= maxWave)
        {
            CancelInvoke("IncreaseWave");
            Debug.Log("Wave�� �ִ뿡 �����Ͽ� InvokeRepeating�� ����Ǿ����ϴ�.");
        }
    }

    private void IncreaseWaveAndWaveStart()
    {
        currentWave++;
        Debug.Log("Wave " + currentWave + " ����");
        StartCoroutine(SpawnWaveEnemies(currentWave));
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Inst.pool.Get(0);    
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[Random.Range(0, spawnData.Length)]);
    }
    
    IEnumerator SpawnWaveEnemies(int wave)
    {
        for(int i=0; i<wave; i++)
        {
            Spawn();
            yield return new WaitForSeconds(0.5f);
        }
    }
}

[System.Serializable]
public class SpawnData //���� �ɷ�ġ ������
{
    public int spriteType;
    public int health;
    public float speed;
    public int exp;
}
