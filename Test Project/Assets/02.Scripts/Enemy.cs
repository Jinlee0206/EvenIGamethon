using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("#Info")]
    public int spriteType;
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public float atkSpeed;
    public RuntimeAnimatorController[] animCon;

    [Header("#Damage Pop Up")]
    public GameObject dmgText;
    public Text popupText;
    public GameObject dmgCanvas;

    [Header("#Color")]
    public Color hitColor = new Color(1f, 0.5f, 0.5f, 1f);  // �ǰ� �� ������ ����
    SpriteRenderer spriteRenderer;
    Color originalColor;

    [Header("#State")]
    public bool isParalyzed = false;
    public bool isKnockback = false;
    public float knockBackSpeed = 10f;

    bool isWallHit = false;
    bool isLive = false;
    bool isWallAttackInProgress = false;
    Coroutine coroutineInfo;

    Rigidbody2D rb;
    Animator anim;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        wait = new WaitForFixedUpdate();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        isLive = true;
        isWallHit = false;
        dmgCanvas = GameObject.Find("DmgPopUpCanvas");
    }

    /// <summary>
    /// ���� �ɷ�ġ ����
    /// </summary>
    /// <param name="data"></param>
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        spriteType = data.spriteType;
        health = data.health;
        maxHealth = data.health;
        damage = data.damage;
        atkSpeed = data.atkSpeed;
        speed = data.speed;
    }

    private void Update()
    {
        if (!isWallHit && !isKnockback)
        {
            // �Ʒ��� �̵�
            rb.velocity = Vector2.down.normalized * speed * Time.deltaTime * 50;
        }
    }

    IEnumerator WallAttack(GameObject wall)
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                isWallAttackInProgress = true; // ������ ���۵��� ǥ��
                anim.SetTrigger("Attack");
                wall.GetComponent<Wall>().getDamage(damage);
                isWallAttackInProgress = false; // ������ ������ ǥ��
                yield return new WaitForSeconds(atkSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;  // ���� �����ϸ� �ӵ��� 0���� ����
            isWallHit = true;
            GameObject wall = GameObject.Find("Wall");
            StartCoroutine(WallAttack(wall));
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // "Enemy"�� �浹���� ���� �����ϰ� �׳� ��ġ�� ��
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision) //���� �浹 ����
    {
        if (!gameObject.activeSelf) return;
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;  // ���� �����ϸ� �ӵ��� 0���� ����
            isWallHit = true;
            GameObject wall = GameObject.Find("Wall");
            StartCoroutine(WallAttack(wall));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // "Enemy"�� �浹���� ���� �����ϰ� �׳� ��ġ�� ��
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
        isWallHit = false;
        rb.velocity = Vector2.down.normalized * speed * Time.deltaTime * 50;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        health -= collision.GetComponent<Bullet>().damage;
        Debug.Log("�ǰ�");

        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
        GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);
        popupText.text = collision.GetComponent<Bullet>().damage.ToString();

        if (health > 0)
        {
            // �ǰ� �� ����
            StartCoroutine(HitEffect());
        }
        else
        {
            // �׾��� ��
            Dead();
            GameManager.Inst.kill++;
            GameManager.Inst.GetExp();
        }
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        TakeDamage(collision.GetComponent<Bullet>().damage);
    }*/

    public void TakeDamage(float damage, int skillId, float duration)
    {
        if(skillId == 4) //�����Ͼư��
        {
            //���� ������
            if (coroutineInfo != null)  StopCoroutine(Aegsonia(damage, duration));
            coroutineInfo = StartCoroutine(Aegsonia(damage, duration));
        }
        else if (skillId == 5) //��ེ���ϰ�� -> ������ ���� �˹鸸 ��
        {
            StartCoroutine(KnockBack());
        }
        else
        {
            Debug.Log("TakeDamage ȣ�� " + damage);
            health -= damage;
            Debug.Log("�ǰ�" + damage);

            //�˾� �����ϴ� �κ�
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
            GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);
            popupText.text = damage.ToString();

            if (health > 0)
            {
                if (skillId == 3 && !isParalyzed) //����ϰ��
                {
                    StartCoroutine(Paralyze(duration));
                }
                // �ǰ� �� ����
                StartCoroutine(HitEffect());
            }
            else
            {
                // �׾��� ��
                spriteRenderer.color = originalColor;
                Dead();
                GameManager.Inst.kill++;
                int killExp;
                if (spriteType % 5 < 3)
                {
                    killExp = 30;
                }
                else if (spriteType % 5 == 3)
                {
                    killExp = 60;
                }
                else killExp = 80;
                GameManager.Inst.GetExp(killExp);
            }
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision) //���ǵ� �����Ҷ� �� ģ��
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        health -= collision.GetComponent<Bullet>().explodeDamage;
    }*/

    IEnumerator HitEffect()
    {
        Color nowOrigin = spriteRenderer.color;
        // SpriteRenderer�� ������ �����Ͽ� ��ο����� ȿ�� �ο�
        spriteRenderer.color = hitColor;

        yield return new WaitForSeconds(0.1f);

        // �ٽ� ���� �������� ���ƿ��� ��
        spriteRenderer.color = nowOrigin;
    }

    IEnumerator Paralyze(float duration) //�������
    {
        isParalyzed = true;
        spriteRenderer.color = new Color(1f, 1f, 0f); //�����
        speed *= 0.8f;
        yield return new WaitForSeconds(duration);  
        spriteRenderer.color = originalColor;
        speed /= 0.8f;
        isParalyzed = false;
    }

    IEnumerator Pinesta(float secondDamage, float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    IEnumerator Aegsonia(float damage, float duration)
    {
        while (duration > 0f)
        {
            // �������� ������ �۾� ����
            Debug.Log("�׼��Ͼ� Ÿ�� " + damage);
            health -= damage;
            Debug.Log("�ǰ�" + damage);

            //�˾� �����ϴ� �κ�
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
            GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);
            popupText.text = damage.ToString();

            // 1�� ���
            yield return new WaitForSeconds(1f);

            // ����� �ð� ����
            duration -= 1f;

            if (health > 0)
            {
                // �ǰ� �� ����
                StartCoroutine(HitEffect());
            }
            else
            {
                // �׾��� ��
                spriteRenderer.color = originalColor;
                Dead();
                GameManager.Inst.kill++;
                int killExp;
                if (spriteType % 5 < 3)
                {
                    killExp = 30;
                }
                else if (spriteType % 5 == 3)
                {
                    killExp = 60;
                }
                else killExp = 80;
                GameManager.Inst.GetExp(killExp);
            }
        }
    }

    IEnumerator KnockBack()
    {
        isKnockback = true;
        float knockBackDuration = 0.1f; // �˹� ���� �ð� (�ڷ�ƾ�� ���ư� �ð�)
        float timer = 0f;

        while (timer < knockBackDuration)
        {
            timer += Time.deltaTime;
            rb.velocity = Vector2.up.normalized * speed * Time.deltaTime * knockBackSpeed * 50;
            //spriteRenderer.color = new Color(0.5f, 0f, 0.5f, 1f);
            yield return null;
        }
        //spriteRenderer.color = originalColor;
        isKnockback = false;
    }



    void Dead()
    {
        gameObject.SetActive(false);
    }
}