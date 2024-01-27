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
    public Transform target;
    List<GameObject> targetsInRange = new List<GameObject>();

    TowerData thisData;
    Animator anim;
    float time = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        time = 1000;
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
    }

    private void FixedUpdate()
    {
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
            target = targetsInRange[Random.Range(0, targetsInRange.Count)].transform;
            Debug.Log("��Ÿ��� Ÿ�� ����");
        }

        if(time > atkSpeed && target != null)
        {
            time = 0;
            //Ÿ�Ժ� �ٸ����� �Լ��� �ʿ��ϴٸ� ���⿡
            BulletSpawn(thisData, target);
        }
        if (targetsInRange.Count > 0) targetsInRange.Clear();
        if (target != null) target = null;
    }

    void BulletSpawn(TowerData data, Transform target)
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