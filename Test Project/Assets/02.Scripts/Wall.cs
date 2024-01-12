using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health;

    public void getDamage(float damage)
    {
        health -= damage;
        Debug.Log("�� �ǰ� : " + damage);

        if(health <= 0)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        //���� ���� ��ƾ -> UI�� ���ٴ��� �ϴ� ����
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(health > 0)
        {
            health -= Time.deltaTime * 10;
        }
    }
}
