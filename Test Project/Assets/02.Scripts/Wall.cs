using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health;
    public float maxHealth;

    private void Start()
    {
        maxHealth = 3500f;
        health = maxHealth;
    }

    public void getDamage(float damage)
    {
        health -= damage;
        //Debug.Log("�� �ǰ� : " + damage + " ���� ü��: " + health);

        if(health <= 0)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        //���� ���� ��ƾ -> UI�� ���ٴ��� �ϴ� ����
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
