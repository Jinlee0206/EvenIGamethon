using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    public Button bottonCorn01;
    public Button bottonCorn05;
    public Button bottonCorn10;

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (bottonCorn01 != null)
        {
            bottonCorn01.onClick.AddListener(() =>
            {
                // ������ �Ѱ� ��� �Լ� �߰�
            });
        }

        if (bottonCorn05 != null)
        {
            bottonCorn05.onClick.AddListener(() =>
            {
                // ������ �ټ��� ��� �Լ� �߰�
            });
        }

        if (bottonCorn10 != null)
        {
            bottonCorn05.onClick.AddListener(() =>
            {
                // ������ ���� ��� �Լ� �߰�
            });
        }
    }


}
