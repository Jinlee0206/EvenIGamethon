using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Linq;

public class Player : MonoBehaviour
{
    public List<PlayerData> playerData = new List<PlayerData>();
    public Transform fireArea;
    public Transform target;

    public Transform[] momenstoPoint;
    public Transform nextMomenstoLocation;

    string xmlFileName = "PlayerData";
    int[] damageUpgradeAmount = { 0, 3, 6, 9, 12, 19, 22, 25, 28, 31, 38, 41, 44, 47, 50, 58, 61, 64, 67, 70, 78 };

    void Start()
    {
        LoadXML(xmlFileName);

        int level = BackendGameData.Instance.UserGameData.level;
        PlayerUpgrade(level);
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
            PlayerData newData = new PlayerData();

            newData.skillId = int.Parse(node.SelectSingleNode("skillId").InnerText);
            newData.penetrate = int.Parse(node.SelectSingleNode("penetrate").InnerText);
            newData.damage = float.Parse(node.SelectSingleNode("damage").InnerText);
            newData.atkSpeed = float.Parse(node.SelectSingleNode("atkSpeed").InnerText);
            newData.secondDelay = float.Parse(node.SelectSingleNode("secondDelay").InnerText);
            newData.duration = float.Parse(node.SelectSingleNode("duration").InnerText);
            newData.bulletSpeed = float.Parse(node.SelectSingleNode("bulletSpeed").InnerText);
            newData.atkRange = float.Parse(node.SelectSingleNode("atkRange").InnerText);
            newData.explodeDamage = float.Parse(node.SelectSingleNode("explodeDamage").InnerText);
            newData.isExplode = bool.Parse(node.SelectSingleNode("isExplode").InnerText);
            newData.isUnlocked = bool.Parse(node.SelectSingleNode("isUnlocked").InnerText);
            newData.isPenetrate = bool.Parse(node.SelectSingleNode("isPenetrate").InnerText);
            newData.splashRange = float.Parse(node.SelectSingleNode("splashRange").InnerText);

            playerData.Add(newData);
        }
    }

    private void PlayerUpgrade(int level) //�������� ���� �޾ƿ���
    {
        foreach(PlayerData data in playerData)
        {
            data.damage += damageUpgradeAmount[level - 1];
            data.explodeDamage += damageUpgradeAmount[level - 1];
        }
        Debug.Log("���� ���׷��̵� ���� " + level);
    }

    private void Update()
    {
        target = gameObject.GetComponent<Scanner>().nearestTarget;

        if (target == null) return;

        Vector3 offset = target.position - transform.position;
        float distance = offset.magnitude;
        /*if(Input.GetKeyUp(KeyCode.Space))
        {
            Attack(target);
        }*/

        foreach(PlayerData data in playerData)
        {
            if (data.isUnlocked)
            {
                data.UpdateCooldown();
                if (data.skillId == 5 && data.CanUseSkill()) //��ེ���ϰ�� �ٸ� ���� ���
                {
                    nextMomenstoLocation = momenstoPoint[Random.Range(0, momenstoPoint.Length)];
                    BulletSpawn(data);
                    data.StartCoolDown();
                }
                else if (data.CanUseSkill() && distance < data.atkRange)
                {
                    BulletSpawn(data);
                    data.StartCoolDown();
                    switch(data.skillId)
                    {
                        case 0:
                            int rand = 0;
                            rand = Random.Range(0, 3);
                            if(rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Attack1);
                            else if(rand == 1) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Attack2);
                            else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Attack3);
                            break;
                        case 1:
                            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Fire_Attack);
                            break;
                        case 2:
                            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Ice_Attack);
                            break;
                        case 3:
                            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Electric_Attack);
                            break;
                        case 4:
                            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Lightning_Attack);
                            break;
                        case 5:
                            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Dark_Attack);
                            break;
                        case 6:
                            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Missile_Attack);
                            break;

                    }
                }
            }
        }
    }

    void BulletSpawn(PlayerData data)
    {
        GameObject bullet = GameManager.Inst.pool.Get(1);
        if (data.skillId == 5) //��ེ�丸 �ٸ� ����
        {
            bullet.transform.position = nextMomenstoLocation.position;
        }
        else
        {
            bullet.transform.position = fireArea.position;
        }
        bullet.GetComponent<Bullet>().Init(data);
        //bullet.GetComponent<Bullet>().target = target;
    }
}

[System.Serializable]
public class PlayerData //����ĳ���� �ɷ�ġ(��ų) ������
{
    public int skillId; //���� ��������
    public int penetrate; //���� Ƚ��
    public float damage; //���ݷ�
    public float atkSpeed; //���� �ӵ�
    public float secondDelay; //�ι�° ������ �ִ� ��ų�� �ι�° ���� ������
    public float duration; //���ӵ����� ��ų�� ���ӽð�
    public float bulletSpeed; //����ü �ӵ�
    public float atkRange; //�����Ÿ�
    public float explodeDamage; //���� ������
    public bool isExplode; //���������� ����
    public bool isUnlocked; //�رݵƴ��� ����
    public bool isPenetrate; //���������� ����
    public float splashRange; //������ų ����
    

    private float cooldownTimer = 0f;

    public bool CanUseSkill()
    {
        return cooldownTimer <= 0f;
    }

    public void StartCoolDown()
    {
        cooldownTimer = atkSpeed;
    }

    public void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            //Debug.Log(skillId + "�� ��ų ��Ÿ�� : " + cooldownTimer);
        }
    }
}
