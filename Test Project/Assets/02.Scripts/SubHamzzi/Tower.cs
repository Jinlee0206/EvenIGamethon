using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

//public enum TowerType { Arrow, Bomb, Black, Tank, Heal }

public class Tower : MonoBehaviour //������ ���� ���۵� -> ���⼭ �� �ҷ��� �����ؾ���
{
    [Header("#Info")]
    public int towerType;
    public float atkSpeed;
    public float damage;
    public float splashRange;
    public float duration;
    public float barrier;
    public float heal;
    public float atkRange;
    public RuntimeAnimatorController[] animCon;

    [Header("#State")]
    public GameObject target;
    List<GameObject> targetsInRange = new List<GameObject>();

    TowerData thisData;
    Animator anim;
    float time = 1000;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        thisData = new TowerData();
    }

    public void Init(TowerData data)
    {
        thisData = data;
        anim.runtimeAnimatorController = animCon[data.towerType];
        towerType = data.towerType;
        atkSpeed = data.atkSpeed;
        damage = data.damage;
        splashRange = data.splashRange;
        duration = data.duration;
        barrier = data.barrier;
        heal = data.heal;
        atkRange = data.atkRange;

        gameObject.transform.localScale = new Vector3(3, 3, 3);
        time = 1000;
    }

    private void FixedUpdate()
    {
        thisData.damage = damage;
        thisData.atkSpeed = atkSpeed;
        thisData.duration = duration;
        thisData.barrier = barrier;
        thisData.heal = heal;
        thisData.atkRange = atkRange;

        time += Time.deltaTime;
        List<GameObject> targets = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        foreach(GameObject target in targets)
        {
            if(Vector2.Distance(target.transform.position, transform.position) < atkRange)
            {
                targetsInRange.Add(target);
            }
        }
        if (targetsInRange.Count > 0)
        {
            target = targetsInRange[Random.Range(0, targetsInRange.Count)];
            //Debug.Log("��Ÿ��� Ÿ�� ����");
        }

        if(time > atkSpeed && target != null)
        {
            time = 0;
            //Ÿ�Ժ� �ٸ����� �Լ��� �ʿ��ϴٸ� ���⿡
            BulletSpawn(thisData, target);
            switch(towerType)
            {
                case 0:
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Arrow_Attack);
                    break;
                case 1:
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Cannon_Attack);
                    break;
                case 2:
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Black_Magic_Spell);
                    break;
                case 3:
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Sheild_Spell);
                    break;
                case 4:
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Heal_Spell);
                    break;
            }
        }
        if (targetsInRange.Count > 0) targetsInRange.Clear();
        if (target != null) target = null;
    }

    void BulletSpawn(TowerData data, GameObject target)
    {
        GameObject towerBullet = GameManager.Inst.pool.Get(3);
        towerBullet.GetComponent<TowerBullet>().Init(data, target);
        if(data.towerType == 2)
        {
            towerBullet.transform.position = target.transform.position;
        }
        else if(data.towerType == 3 || data.towerType == 4)
        {
            towerBullet.transform.position = transform.position;
        }
        else towerBullet.transform.position = transform.position;
    }
}