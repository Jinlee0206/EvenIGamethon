using BackEnd;
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
    public Slider hpSlider;
    /*public GameObject hpBar;
    public Slider bossHPBar;*/

    [Header("#Color")]
    public Color hitColor = new Color(1f, 0.5f, 0.5f, 1f);  // �ǰ� �� ������ ����
    SpriteRenderer spriteRenderer;
    Color originalColor;

    [Header("#State")]
    public bool isParalyzed = false;
    public bool isKnockback = false;
    public float knockBackSpeed = 10f;
    public bool isAegsoniaRunning = false;
    public bool isPinestarRunning = false;
    public bool angry = false; //���� ü�� 50�� ȿ�� ������ �ִ���

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
        //bossHPBar =  hpBar.GetComponentInChildren<Slider>();
        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        isLive = true;
        isWallHit = false;
        dmgCanvas = GameObject.Find("DmgPopUpCanvas");
        isAegsoniaRunning = false;
        isParalyzed = false;
        isKnockback = false;
        isPinestarRunning = false;
        angry = false;
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

        originalColor = spriteRenderer.color;

        if (spriteType % 5 == 4)
        {
            hpSlider.gameObject.SetActive(true);
        }
        else
        {
            hpSlider.gameObject.SetActive(false);
        }
        //bossHPBar.value = 1;
    }

        private void Update()
    {
        if (!isWallHit && !isKnockback)
        {
            // �Ʒ��� �̵�
            rb.velocity = Vector2.down.normalized * speed * Time.deltaTime * 10;
            //AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Grass_Effect);
        }
        if(spriteType % 5 == 4 && health < maxHealth / 2 && !angry) //���� ���Ϸ� ��������
        {
            damage *= 2;
            atkSpeed /= 2;

            angry = true;
        }
        if(spriteType % 5 == 4 && angry)
        {
            spriteRenderer.color = Color.red;
        }
        if (spriteType % 5 == 4)
        {
            hpSlider.value = Mathf.Clamp01(health / maxHealth);

        }
        //hpSlider.value = Mathf.Clamp01(health / maxHealth);
    }

    IEnumerator WallAttack(GameObject wall)
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                isWallAttackInProgress = true; // ������ ���۵��� ǥ��
                anim.SetTrigger("Attack");

                if (0 <= spriteType && spriteType <= 8)
                {
                    int rand = Random.Range(0, 3);
                    if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_01);
                    else if (rand == 1) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_02);
                    else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_03);
                }
                else if(spriteType == 9)
                {
                    AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_04);
                }
                else if(spriteType == 10 || spriteType == 11)
                {
                    int rand = Random.Range(0, 2);
                    if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_05);
                    else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_12);
                }
                else if(12 <= spriteType && spriteType <= 18) 
                {
                    int rand = Random.Range(0, 3);
                    if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_06);
                    else if(rand == 1) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_07);
                    else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_08);
                }
                else
                {
                    int rand = Random.Range(0, 3);
                    if(rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_09);
                    else if(rand == 1) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_10);
                    else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Smash_Castle_11);
                }

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

    private void OnCollisionExit2D(Collision2D collision)
    {
        isWallHit = false;
        rb.velocity = Vector2.down.normalized * speed * Time.deltaTime * 10;
    }

    public void TakeDamage(float damage, float explodeDamage, int skillId, float duration)
    {
        if (!gameObject.activeSelf) return;

        if(skillId == 4) //�����Ͼư��
        {
            //���� ������
            if (!isAegsoniaRunning)
            {
                StartCoroutine(Aegsonia(damage, duration));
            }
            else return;
        }
        else if (skillId == 5) //��ེ���ϰ��
        {
            if (!isKnockback) StartCoroutine(KnockBack());
            else return;
        }
        else if(skillId == 6) //�ǳ׽�Ÿ
        {
            if (!isPinestarRunning) StartCoroutine(Pinesta(damage, explodeDamage, duration));
            else return;
        }
        else
        {
            Debug.Log("TakeDamage ȣ�� " + damage);
            health -= damage;
            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Hit);
            Debug.Log("�ǰ�" + damage);

            //�˾� �����ϴ� �κ�
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
            int intDamage = Mathf.FloorToInt(damage);
            popupText.text = intDamage.ToString();
            GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);

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
                int seed;
                if (spriteType % 5 < 3)
                {
                    killExp = 30;
                    seed = 5;
                }
                else if (spriteType % 5 == 3)
                {
                    killExp = 60;
                    seed = 7;
                }
                else
                {
                    killExp = 80;
                    seed = 10;
                }
                GameManager.Inst.GetExp(killExp);
                GameManager.Inst.GetSeed(seed);
            }
        }
    }

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
        gameObject.GetComponent<Enemy>().speed *= 0.5f;
        yield return new WaitForSeconds(duration);  
        spriteRenderer.color = originalColor;
        gameObject.GetComponent<Enemy>().speed /= 0.5f;
        isParalyzed = false;
    }

    IEnumerator Pinesta(float damage, float explodeDamage, float duration)
    {
        isPinestarRunning = true;
        Debug.Log("TakeDamage ȣ�� " + damage);
        health -= damage;
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Hit);
        Debug.Log("�ǰ�" + damage);

        //�˾� �����ϴ� �κ�
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
        int intDamage = Mathf.FloorToInt(damage);
        popupText.text = intDamage.ToString();
        GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);

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
            int seed;
            if (spriteType % 5 < 3)
            {
                killExp = 30;
                seed = 5;
            }
            else if (spriteType % 5 == 3)
            {
                killExp = 60;
                seed = 7;
            }
            else
            {
                killExp = 80;
                seed = 10;
            }
            GameManager.Inst.GetExp(killExp);
            GameManager.Inst.GetSeed(seed);
        }

        yield return new WaitForSeconds(duration);

        Debug.Log("TakeDamage ȣ�� " + explodeDamage);
        health -= explodeDamage;
        Debug.Log("�ǰ�" + explodeDamage);

        //�˾� �����ϴ� �κ�
        popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);
        int intExplodeDamage = Mathf.FloorToInt(explodeDamage);
        popupText.text = intExplodeDamage.ToString();

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
            int seed;
            if (spriteType % 5 < 3)
            {
                killExp = 30;
                seed = 5;
            }
            else if (spriteType % 5 == 3)
            {
                killExp = 60;
                seed = 7;
            }
            else
            {
                killExp = 80;
                seed = 10;
            }
            GameManager.Inst.GetExp(killExp);
            GameManager.Inst.GetSeed(seed);
        }
        isPinestarRunning = false;
    }

    IEnumerator Aegsonia(float damage, float duration)
    {
        isAegsoniaRunning = true;
        while (duration > 0f)
        {
            yield return new WaitForSeconds(1f);

            // �������� ������ �۾� ����
            Debug.Log("�׼��Ͼ� Ÿ�� " + damage);
            health -= damage;
            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Hit);
            Debug.Log("�ǰ�" + damage);

            //�˾� �����ϴ� �κ�
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
            int intDamage = Mathf.FloorToInt(damage);
            popupText.text = intDamage.ToString();
            GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);

            // ����� �ð� ����
            duration -= 1f;

            if (health > 0)
            {
                // �ǰ� �� ����
                StartCoroutine(HitEffect());
            }
            else
            {
                spriteRenderer.color = originalColor;
                Dead();
                GameManager.Inst.kill++;
                int killExp;
                int seed;
                if (spriteType % 5 < 3)
                {
                    killExp = 30;
                    seed = 5;
                }
                else if (spriteType % 5 == 3)
                {
                    killExp = 60;
                    seed = 7;
                }
                else
                {
                    killExp = 80;
                    seed = 10;
                }
                GameManager.Inst.GetExp(killExp);
                GameManager.Inst.GetSeed(seed);
            }
        }
        isAegsoniaRunning = false;
    }

    IEnumerator KnockBack()
    {
        Debug.Log("TakeDamage ȣ�� " + damage);
        health -= damage;
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Hit);
        Debug.Log("�ǰ�" + damage);

        //�˾� �����ϴ� �κ�
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0);
        int intDamage = Mathf.FloorToInt(damage);
        popupText.text = intDamage.ToString();
        GameObject popupTextObejct = Instantiate(dmgText, pos, Quaternion.identity, dmgCanvas.transform);

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
            int seed;
            if (spriteType % 5 < 3)
            {
                killExp = 30;
                seed = 5;
            }
            else if (spriteType % 5 == 3)
            {
                killExp = 60;
                seed = 7;
            }
            else
            {
                killExp = 80;
                seed = 10;
            }
            GameManager.Inst.GetExp(killExp);
            GameManager.Inst.GetSeed(seed);
        }

        isKnockback = true;
        float knockBackDuration = 0.1f; // �˹� ���� �ð� (�ڷ�ƾ�� ���ư� �ð�)
        float timer = 0f;

        while (timer < knockBackDuration)
        {
            timer += Time.deltaTime;
            rb.velocity = Vector2.up.normalized * Time.unscaledDeltaTime * knockBackSpeed * 50;
            //spriteRenderer.color = new Color(0.5f, 0f, 0.5f, 1f);
            yield return null;
        }
        //spriteRenderer.color = originalColor;
        isKnockback = false;
    }

    void Dead()
    {
        int rand = Random.Range(0, 3);
        switch (spriteType) 
        {
            //é�� 1
            case 0:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_01);
                break;
            case 1:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_01);
                break;
            case 2:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_02);
                break;
            case 3:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_02);
                break;
            case 4:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_03);
                break;
            //é�� 2
            case 5:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_04);
                break;
            case 6:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_04);
                break;
            case 7:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_05);
                break;
            case 8:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_05);
                break;
            case 9:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_06);
                break;
            //é�� 3
            case 10:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_07);
                break;
            case 11:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_07);
                break;
            case 12:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_08);
                break;
            case 13:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_08);
                break;
            case 14:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_08);
                break;
            //é�� 4
            case 15:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_09);
                break;
            case 16:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_10);
                break;
            case 17:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_11);
                break;
            case 18:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_12);
                break;
            case 19:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Monster_Die_13);
                break;
        }


        /*foreach (Transform child in transform)
        {
            //Destroy(child.gameObject);
        }*/

        gameObject.SetActive(false);
    }
}