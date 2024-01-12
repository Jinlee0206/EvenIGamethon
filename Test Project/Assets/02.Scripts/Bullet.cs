using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int skillId; //��ų ��������Ʈ ���� ���̵�
    public float damage;
    public int penetrate; //���� Ƚ�� -> 0�̶�� �ѹ� �ε����� ��, -1�̸� ����
    public float bulletSpeed; //����ü �ӵ�
    public float atkRange;
    public Transform target;

    Rigidbody2D rb;
    private Vector3 initialDirection;
    private Transform initialLocation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(PlayerData playerData)
    {
        skillId = playerData.skillId;
        damage = playerData.damage;
        penetrate = playerData.penetrate;
        bulletSpeed = playerData.bulletSpeed;
        atkRange = playerData.atkRange;
    }

    private void OnEnable() //Start���ϸ� ��Ȱ��ɶ� target������ ������Ʈ���� ����
    {
        initialLocation = GameManager.Inst.player.fireArea;
        target = GameManager.Inst.player.target;
        initialDirection = target.position - initialLocation.position;
    }

    private void Update()
    {
        /*if(target == null || !target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }*/

        if (gameObject.activeSelf)
        {
            rb.velocity = initialDirection.normalized * bulletSpeed;
            gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, initialDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || penetrate == -1) return; 
        //������� �⺻ -1�̶�� ���Ѱ���

        penetrate--;
        if (penetrate == -1)
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}