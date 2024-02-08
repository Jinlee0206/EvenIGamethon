using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    public Button bottonCorn01;
    public Button bottonCorn05;
    public Button bottonCorn10;

    public GameObject lobbyGrid; // �κ� �׸���
    public GameObject shopGrid; // ���� �׸���

    void Start()
    {
        Init();
    }

    void Init()
    {
        //if (bottonCorn01 != null)
        //{
        //    bottonCorn01.onClick.AddListener(() =>
        //    {
        //        //������ �Ѱ� ��� �Լ� �߰�
        //        BackendGameData.Instance.UserGameData.corn += 1;
        //        BackendGameData.Instance.GameDataUpdate();
        //        LobbyScene.Instance.UpdateCurrencyData();
        //    });
        //}

        //if (bottonCorn05 != null)
        //{
        //    bottonCorn05.onClick.AddListener(() =>
        //    {
        //        //������ �ټ��� ��� �Լ� �߰�
        //        BackendGameData.Instance.UserGameData.corn += 5;
        //        BackendGameData.Instance.GameDataUpdate();
        //        LobbyScene.Instance.UpdateCurrencyData();
        //    });
        //}

        //if (bottonCorn10 != null)
        //{
        //    bottonCorn05.onClick.AddListener(() =>
        //    {
        //        //������ ���� ��� �Լ� �߰�
        //        BackendGameData.Instance.UserGameData.corn += 10;
        //        BackendGameData.Instance.GameDataUpdate();
        //        LobbyScene.Instance.UpdateCurrencyData();
        //    });
        //}

        lobbyGrid = FindObjectOfType<Grid>().gameObject.transform.GetChild(0).gameObject;
        shopGrid = FindObjectOfType<Grid>().gameObject.transform.GetChild(1).gameObject;

        OnPopupOpened();
    }

    // �˾� â�� ���� �� ȣ��Ǵ� �Լ�
    public void OnPopupOpened()
    {
        // �κ���� �ش��ϴ� �׸��带 ��Ȱ��ȭ�ϰ�, ���� �ʿ� �ش��ϴ� �׸��带 Ȱ��ȭ
        lobbyGrid.SetActive(false);
        shopGrid.SetActive(true);
    }

    // �˾� â�� ���� �� ȣ��Ǵ� �Լ�
    public void OnPopupClosed()
    {
        // �κ���� �ش��ϴ� �׸��带 Ȱ��ȭ�ϰ�, ���� �ʿ� �ش��ϴ� �׸��带 ��Ȱ��ȭ
        lobbyGrid.SetActive(true);
        shopGrid.SetActive(false);
    }


    public void OnClickConr60()
    {
        BackendGameData.Instance.UserGameData.corn += 60;
        BackendGameData.Instance.GameDataUpdate();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
    }

    public void OnClickCorn300()
    {
        BackendGameData.Instance.UserGameData.corn += 300;
        BackendGameData.Instance.GameDataUpdate();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
    }

    public void OnClickCorn600()
    {
        BackendGameData.Instance.UserGameData.corn += 600;
        BackendGameData.Instance.GameDataUpdate();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
    }

}
