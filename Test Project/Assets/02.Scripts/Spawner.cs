using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using System.Xml.Serialization;

public class Spawner : MonoBehaviour //���̺꺰 ���� ����
{
    public Transform[] spawnPoint;
    public List<SpawnData> spawnData = new List<SpawnData>();
    string xmlFileName = "MobData";

    public int currentWave;
    public int maxWave = 20;

    public int[,] waveInfo = new int[,]
    {
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
        {6, 0},
    };

    void Start()
    {
        LoadXML(xmlFileName);
        InvokeRepeating("IncreaseWaveAndWaveStart", 0f, GameManager.Inst.waveChangeTime);
    }

    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(_fileName);
        if (txtAsset == null)
        {
            Debug.LogError("Failed to load XML file: " + _fileName);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        // ��ü ������ �������� ����.
        XmlNodeList all_nodes = xmlDoc.SelectNodes("root/Sheet1");
        foreach (XmlNode node in all_nodes)
        {
            SpawnData newData = new SpawnData();

            newData.spriteType = int.Parse(node.SelectSingleNode("spriteType").InnerText);
            newData.health = float.Parse(node.SelectSingleNode("health").InnerText);
            newData.damage = float.Parse(node.SelectSingleNode("damage").InnerText);
            newData.atkSpeed = float.Parse(node.SelectSingleNode("atkSpeed").InnerText);
            newData.speed = float.Parse(node.SelectSingleNode("speed").InnerText);

            spawnData.Add(newData);
        }
    }

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
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
        //Debug.Log("Wave " + currentWave + " ����");
        StartCoroutine(SpawnWaveEnemies(currentWave));
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Inst.pool.Get(0);    
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[UnityEngine.Random.Range(0, spawnData.Count)]);
    }
    
    IEnumerator SpawnWaveEnemies(int wave)
    {
        int mobsThisWave = waveInfo[wave-1, 0]; //���� ���̺꿡 ���;� �� �� ��
        //Debug.Log(mobsThisWave);
        for(int i=0; i<mobsThisWave; i++)
        {
            Spawn();
            yield return new WaitForSeconds(0.5f);
        }
    } //����� ���߿� ��������-é�ͱ��� �� ������ �ȴٸ� xml�� �޾ƿ��� ����
}

[System.Serializable]
public class SpawnData //���� �ɷ�ġ ������
{
    public int spriteType;
    public float health;
    public float damage;
    public float atkSpeed;
    public float speed;
}
