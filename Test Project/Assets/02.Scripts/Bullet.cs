using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float splashRange;
    public Vector2 targetPosition;
    public GameObject target;

    [Header("#Skill Effect")]
    public RuntimeAnimatorController[] animCon;
    Animator anim;


    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    private Vector2 initialDirection;
    private Vector2 initialLocation;
    private bool isTargetLocked = false;

    Vector2 currentSize;
    Vector2 newSize;

    bool bombardaExplodeCoroutineStarted = false;

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
        splashRange = playerData.splashRange;

        if (gameObject.GetComponent<Bullet>().skillId == 1) //���ٸ����ϰ�� �ݶ��̴� ��� ����
        {
            capsuleCollider.enabled = false;
        }
    }

    private void OnEnable() //Start���ϸ� ��Ȱ��ɶ� target������ ������Ʈ���� ����
    {
        initialLocation = GameManager.Inst.player.fireArea.position;
        targetPosition = GameManager.Inst.player.target.position;
        initialDirection = targetPosition - initialLocation;
        target = GameManager.Inst.player.target.gameObject;

        currentSize = capsuleCollider.size;
        newSize = new Vector2(currentSize.x * 8f, currentSize.y * 4f);
        capsuleCollider.isTrigger = true;
        capsuleCollider.size = new Vector2(1f, 1f);
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
            if (skillId == 1) //���ٸ���
            {
                rb.velocity = initialDirection.normalized * bulletSpeed;
                gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, initialDirection);

                // ���� ���� ���� ���� ��ġ�� ��ġ�ϸ�
                float positionError = 0.1f; // ������ ���� ������ �����ϼ���

                if (!capsuleCollider.enabled && Vector2.Distance(transform.position, targetPosition) < positionError)
                {
                    transform.localScale = Vector3.one;
                    anim.speed = 2;
                    anim.SetTrigger("MainEffect");
                    //capsuleCollider.size = newSize;
                    capsuleCollider.enabled = true; //�� �������� �� ������ �ؾ��� �ݶ��̴��� �Ѽ��� �ȵȴ�.
                    bulletSpeed = 0f;
                }
            }
            else if (skillId == 3) //���
            {
                transform.localScale = new Vector3(0.7f, 2f, 0.7f);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy").Where(obj => obj.transform.position.y <= 2.5f).ToArray();
                if (enemies.Length > 0 && !isTargetLocked)
                {
                    isTargetLocked = true;
                    target = enemies[Random.Range(0, enemies.Length)];
                    targetPosition = target.transform.position;
                }
                transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 0.2f);
                anim.speed = 3;
            }
            else
            {
                rb.velocity = initialDirection.normalized * bulletSpeed;
                gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, initialDirection);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || penetrate == -1) return; 
        //������� �⺻ -1�̶�� ���Ѱ���

        switch (gameObject.GetComponent<Bullet>().skillId)
        {
            case 0: //������
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, skillId, duration);
                penetrate--;
                if (penetrate == -1)
                {
                    bulletSpeed = 0;
                    gameObject.SetActive(false);
                }
                break;
            case 1: //���ٸ���
                //collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                penetrate--;
                if (penetrate == -1)
                {
                    var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
                    foreach(var hitCollider in hitColliders)
                    {
                        var enemy = hitCollider.GetComponent<Enemy>();
                        if(enemy)
                        {
                            var closestPoint = hitCollider.ClosestPoint(transform.position);
                            var distance = Vector3.Distance(closestPoint, transform.position);

                            enemy.TakeDamage(explodeDamage, skillId, duration);
                        }
                    }
                }
                break;
            case 2: //�Ʊ��Ƹ�Ƽ
                break;
            case 3: //���
                //������ ����
                break;
            case 4: //�׼ҴϾ�
                break;
            case 5: //��ེ��
                break;
            case 6: //�ǳ׽�Ÿ
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, skillId, duration);
                collision.gameObject.GetComponent<Enemy>().TakeDamage(explodeDamage, skillId, duration);
                penetrate--;
                if (penetrate == -1)
                {
                    bulletSpeed = 0;
                    gameObject.SetActive(false);
                }
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
    }

    public void OnAnimationEnd() //�ִϸ��̼� �̺�Ʈ
    {
        // �ִϸ��̼� ������ ����Ǹ� ȣ��Ǵ� �Լ�
        capsuleCollider.size = currentSize;
        anim.speed = 1;
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        isTargetLocked = false;
        gameObject.SetActive(false);
    }

    public void OnAnimationLumos()
    {
        target.GetComponent<Enemy>().TakeDamage(damage, skillId, duration);

        capsuleCollider.size = currentSize;
        anim.speed = 1;
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        isTargetLocked = false;
        gameObject.SetActive(false);
    }
}