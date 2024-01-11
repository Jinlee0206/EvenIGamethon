using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) // �׽�Ʈ��
        {
            cardMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// ī�� �������� ��, ī�� �޴�â ���� ���� �簳
    /// ī�� ���� �� ���� ������Ʈ �ϴ� ���� ���� �ʿ�
    /// </summary>
    public void ChooseCard()
    {
        cardMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
