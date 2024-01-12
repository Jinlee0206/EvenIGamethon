using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public float atkSpeed;
    public RuntimeAnimatorController[] animCon;

    bool isWallHit = false;
    bool isLive = false;
    bool isWallAttackInProgress = false;

    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    private void OnEnable()
    {
        isLive = true;
        isWallHit = false;
        health = maxHealth;
    }

    /// <summary>
    /// ���� �ɷ�ġ ����
    /// </summary>
    /// <param name="data"></param>
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        health = data.health;
        maxHealth = data.health;
        damage = data.damage;
        atkSpeed = data.atkSpeed;
        speed = data.speed;
    }

    private void Update()
    {
        if (!isWallHit)
        {
            // �Ʒ��� �̵�
            Vector2 movement = Vector2.down * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }

        if(speed == 0f && !isWallAttackInProgress) //���� ����
        {
            GameObject wall = GameObject.Find("Wall");
            StartCoroutine(WallAttack(wall));
        }
    }

    IEnumerator WallAttack(GameObject wall)
    {
        isWallAttackInProgress = true; // ������ ���۵��� ǥ��

        yield return new WaitForSeconds(atkSpeed);
        wall.GetComponent<Wall>().getDamage(damage);

        isWallAttackInProgress = false; // ������ ������ ǥ��
    }

    private void OnCollisionEnter2D(Collision2D collision) //���� �浹 ����
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            speed = 0f;  // ���� �����ϸ� �ӵ��� 0���� ����
            isWallHit = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // "Enemy"�� �浹���� ���� �����ϰ� �׳� ��ġ�� ��
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage;
        Debug.Log("�ǰ�");

        if (health > 0) //�ǰ� �� ����
        {

        }
        else //�׾��� ��
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}