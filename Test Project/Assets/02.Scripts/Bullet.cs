using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int skillId; //��ų ��������Ʈ ���� ���̵�
    public int penetrate; //���� Ƚ�� -> 0�̶�� �ѹ� �ε����� ��, -1�̸� ����
    public float damage;
    public float atkSpeed;
    public float secondDelay;
    public float duration;
    public float bulletSpeed; //����ü �ӵ�
    public float atkRange;
    public float explodeDamage;
    public bool isExplode;
    public bool isUnlocked;
    public Transform target;

    [Header("#Skill Effect")]
    public RuntimeAnimatorController[] animCon;
    Animator anim;


    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    private Vector3 initialDirection;
    private Transform initialLocation;

    Vector2 currentSize;
    Vector2 newSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Init(PlayerData playerData)
    {
        anim.runtimeAnimatorController = animCon[playerData.skillId];
        skillId = playerData.skillId;
        penetrate = playerData.penetrate;
        damage = playerData.damage;
        atkSpeed = playerData.atkSpeed;
        secondDelay = playerData.secondDelay;
        duration = playerData.duration;
        bulletSpeed = playerData.bulletSpeed;
        atkRange = playerData.atkRange;
        explodeDamage = playerData.explodeDamage;
        isExplode = playerData.isExplode;
        isUnlocked = playerData.isUnlocked;
    }

    private void OnEnable() //Start���ϸ� ��Ȱ��ɶ� target������ ������Ʈ���� ����
    {
        initialLocation = GameManager.Inst.player.fireArea;
        target = GameManager.Inst.player.target;
        initialDirection = target.position - initialLocation.position;

        currentSize = capsuleCollider.size;
        newSize = new Vector2(currentSize.x * 2f, currentSize.y * 2f);
    }

    private void Update()
    {
        /*if(target == null || !target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }*/
        /*AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // "MainEffect" �ִϸ��̼��� ��� ������ Ȯ��
        if (stateInfo.IsName("MainEffect"))
        {
            rb.velocity = Vector2.zero;
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

        switch (gameObject.GetComponent<Bullet>().skillId)
        {
            case 0:
                penetrate--;
                if (penetrate == -1)
                {
                    bulletSpeed = 0;
                    gameObject.SetActive(false);
                }
                break;
            case 1:
                penetrate--;
                if (penetrate == -1)
                {
                    bulletSpeed = 0;
                    anim.SetTrigger("MainEffect"); //���� ����Ʈ�� ����

                    //���� ����Ʈ �ʹ� ���� -> 2���
                    anim.speed = 3.0f;

                    //�ݶ��̴� ũ�� Ű���, ���� ������ �߰�
                    capsuleCollider.size = newSize;
                    StartCoroutine(BombardaExplode(collision.gameObject, explodeDamage));
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                penetrate--;
                if (penetrate == -1)
                {
                    bulletSpeed = 0;
                    gameObject.SetActive(false);
                }
                break;
        }

        //�⺻ �׳� ������ �������
        /*penetrate--;
        if (penetrate == -1)
        {
            bulletSpeed = 0;
            penetrate = maxPenetrate;
            gameObject.SetActive(false);
        }*/
    }

    IEnumerator BombardaExplode(GameObject enemyObject, float damage)
    {
        if (enemyObject == null)
        {
            Debug.LogError("Enemy GameObject is null in BombardaExplode coroutine.");
            yield break; // ������ ó���� �� �� �ڷ�ƾ�� �����մϴ�.
        }

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Enemy component not found on the Enemy GameObject in BombardaExplode coroutine.");
            yield break; // ������ ó���� �� �� �ڷ�ƾ�� �����մϴ�.
        }
        else
        {
            Debug.Log("���ߵ����� �õ�");
            //yield return new WaitForSeconds(0.7f); 
            enemy.TakeDamage(damage); //������ ���ִϱ� ��.
            Debug.Log("���ߵ����� ��");
            yield break;
        }
    }

    public void OnAnimationEnd() //�ִϸ��̼� �̺�Ʈ
    {
        // �ִϸ��̼� ������ ����Ǹ� ȣ��Ǵ� �Լ�
        gameObject.SetActive(false);
        capsuleCollider.size = currentSize;
    }
}