using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour //���̺꺰 ���� ����
{
    public Transform[] spawnPoint;
    public List<SpawnData> spawnData = new List<SpawnData>();
    string xmlFileName = "MobData";
    string stageXmlFileName;

    [Header("#StageInfo")]
    public int chapter;
    public int stage;
    public List<StageWaveData> stageWaveData = new List<StageWaveData>();
    public List<MobMagnificationData> mobMagnificationData = new List<MobMagnificationData>();
    string magXmlFileName = "MobStatMagnification";

    [Header("Boss Effect")]
    public Image redLightImage;
    public Image warningImage;

    public event UnityAction<int> OnWaveChanged; // ���̺� �ٲ� �� �˷��ִ� UnityAction

    [Header("Wave Info")]
    public int currentWave;
    public int maxWave = 20;
    public int stageMobCount = 0; //���� ������������ ������ �� ���� �� -> �¸� ������ ���
    public GameObject[] tilemaps;

    void Start()
    {
        LoadXML(xmlFileName);
        InvokeRepeating("IncreaseWaveAndWaveStart", 0f, GameManager.Inst.waveChangeTime);

        //���̺� ���� ���� �Լ�
        if (GameObject.Find("StageManager") == null)
        {
            chapter = 1;
            stage = 1;
        }
        else
        {
            chapter = StageSelect.instance.chapter;
            stage = StageSelect.instance.stage;
        }
        stageXmlFileName = "Chapter" + chapter;
        LoadStageXml(stageXmlFileName);

        //���������� �� �ɷ�ġ ����
        LoadMagXml(magXmlFileName);

        /*
        LoadXML(xmlFileName);
        InvokeRepeating("IncreaseWaveAndWaveStart", 0f, GameManager.Inst.waveChangeTime);

        //���̺� ���� ���� �Լ�
        chapter = StageSelect.instance.chapter;
        stage = StageSelect.instance.stage;
        stageXmlFileName = "Chapter" + chapter;
        LoadStageXml(stageXmlFileName);

        //���������� �� �ɷ�ġ ����
        LoadMagXml(magXmlFileName);
        */

        for(int i=0; i<20; i++)
        {
            stageMobCount += stageWaveData[i].mob1;
            stageMobCount += stageWaveData[i].mob2;
            stageMobCount += stageWaveData[i].mob3;
            stageMobCount += stageWaveData[i].semiBoss;
            stageMobCount += stageWaveData[i].boss;
        }

        /*for(int i=0; i<4; i++)
        {
            tilemaps[i].SetActive(false);
        }
        tilemaps[chapter].SetActive(true);*/
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

    private void LoadStageXml(string _fileName)
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
            StageWaveData newData = new StageWaveData();

            newData.wave = int.Parse(node.SelectSingleNode("wave").InnerText);
            newData.mob1 = int.Parse(node.SelectSingleNode("mob1").InnerText);
            newData.mob2 = int.Parse(node.SelectSingleNode("mob2").InnerText);
            newData.mob3 = int.Parse(node.SelectSingleNode("mob3").InnerText);
            newData.semiBoss = int.Parse(node.SelectSingleNode("semiBoss").InnerText);
            newData.boss = int.Parse(node.SelectSingleNode("boss").InnerText);

            stageWaveData.Add(newData);
        }
    }

    private void LoadMagXml(string _fileName)
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
            MobMagnificationData newData = new MobMagnificationData();

            newData.chpater = int.Parse(node.SelectSingleNode("chapter").InnerText);
            newData.stage = int.Parse(node.SelectSingleNode("stage").InnerText);
            newData.mobHealth = float.Parse(node.SelectSingleNode("mobHealth").InnerText);
            newData.mobDamage = float.Parse(node.SelectSingleNode("mobDamage").InnerText);

            mobMagnificationData.Add(newData);
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

        if(GameManager.Inst.kill >= stageMobCount)
        {
            Victory();
        }
    }

    void Victory()
    {
        //�¸� ����
        UIManager.Inst.victoryUI.SetActive(true);          // VictoryUI�� �ѱ⸸ �Ѵ�
    }

    private void IncreaseWaveAndWaveStart()
    {
        currentWave++;
        //Debug.Log("Wave " + currentWave + " ����");
        // �߰� �ۼ� �κ�
        if (OnWaveChanged != null)
            OnWaveChanged.Invoke(currentWave);          // ���̺� �ٲ� ��, 

        if (stage == 5 && currentWave == 10) //�� é�� �������� 5, 10���̺꿡��
        {
            //������� �� ������ ���̺갡 ����ǵ���, ������ ���� ���� �Լ� ����Ұ�
            StartCoroutine(BossStageEffect());
        }
        else StartCoroutine(SpawnWaveEnemies(currentWave));
    }

    IEnumerator BossStageEffect()
    {
        redLightImage.gameObject.SetActive(true);
        warningImage.gameObject.SetActive(true);
        // 2. ȭ�� ��ü�� �������� ���������� 3ȸ ������
        for (int i = 0; i < 3; i++)
        {
            SetRedLightImage(new Color(1f, 0f, 0f, 0.5f)); // ������ �̹��� ���� ����
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
            SetRedLightImage(Color.clear); // ������ �̹��� ������ ���� ������ ����
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
        }
        redLightImage.gameObject.SetActive(false);
        warningImage.gameObject.SetActive(false);

        //����ٰ� ���� ���� �ֱ�
        Spawn((chapter * 5 - 1));
    }

    // ������ �̹����� ������ �����ϴ� �Լ�
    void SetRedLightImage(Color color)
    {
        redLightImage.color = color;
    }

    void Spawn(int index)
    {
        GameObject enemy = GameManager.Inst.pool.Get(0);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
        //enemy.GetComponent<Enemy>().Init(spawnData[UnityEngine.Random.Range(0, spawnData.Count)]);
        enemy.GetComponent<Enemy>().Init(spawnData[index]);
        enemy.GetComponent<Enemy>().health *= mobMagnificationData[(chapter - 1) * 5 + (stage - 1)].mobHealth;
        enemy.GetComponent<Enemy>().maxHealth *= mobMagnificationData[(chapter - 1) * 5 + (stage - 1)].mobHealth;
        enemy.GetComponent<Enemy>().damage *= mobMagnificationData[(chapter - 1) * 5 + (stage - 1)].mobDamage;

        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(4, 4, 1); //scale �ʱ�ȭ��

        if(index % 5 == 3)
        {
            if (spriteRenderer != null)
            {
                // ���� scale ���� ������
                Vector3 currentScale = spriteRenderer.transform.localScale;

                // scale ���� ���� ���� 2�� ����
                spriteRenderer.transform.localScale = new Vector3(currentScale.x * 1.5f, currentScale.y * 1.5f, currentScale.z);
            }
        }
        else if (index % 5 == 4) // ���� ���� ���
        {
            if (spriteRenderer != null)
            {
                // ���� scale ���� ������
                Vector3 currentScale = spriteRenderer.transform.localScale;

                // scale ���� ���� ���� 2�� ����
                spriteRenderer.transform.localScale = new Vector3(currentScale.x * 2, currentScale.y * 2, currentScale.z);
            }
        }
    }

    IEnumerator SpawnWaveEnemies(int wave)
    {
        int[] eachMobThisWave = new int[5]; //������ = �̹� ���̺꿡�� �� ������ ��ȯ ��
        eachMobThisWave[0] = stageWaveData[wave - 1].mob1;
        eachMobThisWave[1] = stageWaveData[wave - 1].mob2;
        eachMobThisWave[2] = stageWaveData[wave - 1].mob3;
        eachMobThisWave[3] = stageWaveData[wave - 1].semiBoss;
        eachMobThisWave[4] = stageWaveData[wave - 1].boss;

        //�� é�ͺ��� mob1, 2, 3, semi, boss�� �����տ��� ���°���� �ҷ������� �˾ƾ� ��
        //�� �����͸� �޾Ƽ� ����Ʈ�� ���ں��� ������ ����, ��� ��ȯ
        //�� ������ spawnData���� ���° �������� �˾ƺ���
        int[] eachIndex = new int[5]; //mob1, mob2, mob3, semiBoss, boss
        switch(chapter)
        {
            case 1:
                for(int i=0; i<5; i++)
                {
                    eachIndex[i] = i;
                }
                break;
            case 2: 
                for(int i=0; i<5; i++)
                {
                    eachIndex[i] = i + 5;
                }
                break;
            case 3:
                for(int i=0; i<5; i++)
                {
                    eachIndex[i] = i + 10;
                }
                break;
            case 4: 
                for(int i=0; i<5; i++)
                {
                    eachIndex[i] = i + 15;
                }
                break;
            default:
                for (int i = 0; i < 5; i++)
                {
                    eachIndex[i] = i;
                }
                break;
        }

        List<int> spawnList = new List<int>();
        for(int i=0; i<4; i++)
        {
            for(int j=0; j < eachMobThisWave[i]; j++)
            {
                spawnList.Add(eachIndex[i]);
            }
        }
        string output = string.Join(" ", spawnList);
        //Debug.Log("Origin Spawn: " + output);

        ShuffleList(spawnList);

        foreach(var index in spawnList)
        {
            Spawn(index);
            yield return new WaitForSeconds(0.5f);
        }

        /*//���⿡ for���� ���̺꺰 ���� �� ������ ������ �ȴ�
        for (int i = 0; i < mobThisWave; i++)
        {
            Spawn(i);
            yield return new WaitForSeconds(0.5f);
        }*/
    }

    //���� �������� ���� - FisherYate �˰���
    void ShuffleList<T>(List<T> list)
    {
        System.Random random = new System.Random();

        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);

            // Swap list[i] and list[j]
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        string output = string.Join(" ", list);
        //Debug.Log("Shuffled Spawn: " + output);
    }
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

[System.Serializable]
public class StageWaveData
{ //��������, ���̺꺰 �� ������ ��ȯ ��
    public int wave;
    public int mob1;
    public int mob2;
    public int mob3;
    public int semiBoss;
    public int boss;
}

[System.Serializable]
public class MobMagnificationData
{
    public int chpater;
    public int stage;
    public float mobHealth;
    public float mobDamage;
}