using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header ("#Info")]
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

    bool isWallHit = false;
    bool isLive = false;
    bool isWallAttackInProgress = false;

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


        /*if(speed == 0f && !isWallAttackInProgress) //���� ����
        {
            GameObject wall = GameObject.Find("Wall");
            StartCoroutine(WallAttack(wall));
        }*/
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

    private void OnCollisionEnter2D(Collision2D collision) //���� �浹 ����
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            speed = 0f;  // ���� �����ϸ� �ӵ��� 0���� ����
            isWallHit = true;
            GameObject wall = GameObject.Find("Wall");
            StartCoroutine(WallAttack(wall));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // "Enemy"�� �浹���� ���� �����ϰ� �׳� ��ġ�� ��
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isWallHit = false;
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
        Debug.Log("TakeDamage ȣ�� " + damage);
        health -= damage;
        Debug.Log("�ǰ�" + damage);

        //�˾� �����ϴ� �κ�
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
        GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);
        popupText.text = damage.ToString();

        if (health > 0)
        {
            if(skillId == 3 && !isParalyzed) //����ϰ��
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
            GameManager.Inst.GetExp();
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

    IEnumerator KnockBack()
    {
        yield return wait;
        float knockBackDistance = 0.5f; // Adjust as needed
    }


    void Dead()
    {
        gameObject.SetActive(false);
    }
}