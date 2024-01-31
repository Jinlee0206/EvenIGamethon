using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public SpriteRenderer wallImage;
    public Sprite[] wallImages;

    private void Awake()
    {
        wallImage = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        maxHealth = 3500f;
        health = maxHealth;
    }

    private void Update()
    {
        int rand = Random.Range(0, 2);
        /*if (health > maxHealth * 0.8f)
        {
            wallImage.sprite = wallImages[0];
            if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_01);
            else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_02);
        }
        else if (health < maxHealth * 0.8f && health > maxHealth * 0.6f)
        {
            wallImage.sprite = wallImages[1];
            if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_01);
            else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_02);
        }
        else if (health < maxHealth * 0.6f && health > maxHealth * 0.3f)
        {
            wallImage.sprite = wallImages[2];
            if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_01);
            else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_02);
        }
        else if (health < maxHealth * 0.3f && health > 0)
        {
            wallImage.sprite = wallImages[3];
            if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_01);
            else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_02);
        }
        else
        {
            wallImage.sprite = wallImages[4];
            if (rand == 0) AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_01);
            else AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Castle_Brake_02);
        }*/
    }



    public void getDamage(float damage)
    {
        health -= damage;
        //Debug.Log("�� �ǰ� : " + damage + " ���� ü��: " + health);

        if(health <= 0)
        {
            wallImage.sprite = Resources.Load<Sprite>("04.Images/Wall/Castle_0percent.png");
            gameOver();
        }
    }

    public void gameOver()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Stage_Fail);
        //���� ���� ��ƾ -> UI�� ���ٴ��� �ϴ� ����
        GameManager.Inst.Stop();                        // ���� �ð� ����
        UIManager.Inst.gameOverUI.SetActive(true);      // ���� ���� UI ������
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(health > 0)
        {
            health -= Time.deltaTime * 10;
        }
    }
}
