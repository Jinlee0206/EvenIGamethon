using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonHandler : MonoBehaviour
{
    public GameObject lobbyGrid; // �κ� �׸���
    public GameObject shopGrid; // ���� �׸���
    public GameObject warningPanel;

    public Button arrowBtn;
    public Button bombBtn;
    public Button blackBtn;
    public Button tankBtn;
    public Button healBtn;

    public Button yesBtn;
    public Button noBtn;
    public TextMeshProUGUI warningMsg;
    private int cornProductId;

    void Start()
    {
        Init();
        warningMsg.text = "";
        cornProductId = 0;

        if(BackendGameData.Instance.TowerDB.t0) arrowBtn.interactable = false;
        if(BackendGameData.Instance.TowerDB.t1) bombBtn.interactable = false;
        if(BackendGameData.Instance.TowerDB.t2) blackBtn.interactable = false;
        if(BackendGameData.Instance.TowerDB.t3) tankBtn.interactable = false;
        if(BackendGameData.Instance.TowerDB.t4) healBtn.interactable = false;
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
        BackendGameData.Instance.GameDataLoad();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
    }

    public void OnClickCorn300()
    {
        BackendGameData.Instance.UserGameData.corn += 300;
        BackendGameData.Instance.GameDataUpdate();
        BackendGameData.Instance.GameDataLoad();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
    }

    public void OnClickCorn600()
    {
        BackendGameData.Instance.UserGameData.corn += 600;
        BackendGameData.Instance.GameDataUpdate();
        BackendGameData.Instance.GameDataLoad();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
    }

    public void OnClickBread05()
    {
        if(BackendGameData.Instance.UserGameData.corn < 20)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 20����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 0;
    }

    public void OnClickThreadmill01()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        if(BackendGameData.Instance.UserGameData.threadmill == 10)
        {
            StartCoroutine(FullThreadmill());
            return;
        }
        if (BackendGameData.Instance.UserGameData.corn < 10)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 10����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 1;
    }

    public void OnClickRemoveAds()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        //�׳� IAP �Լ� ����
        warningPanel.SetActive(true);
        warningMsg.text = "�غ� �� �Դϴ�...";
        cornProductId = 2;
    }

    public void OnClickArrowBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        if (BackendGameData.Instance.UserGameData.corn < 250)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 250����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 3;
    }

    public void OnClickBombBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        if (BackendGameData.Instance.UserGameData.corn < 480)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 480����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 4;
    }

    public void OnClickBlackBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        if (BackendGameData.Instance.UserGameData.corn < 800)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 800����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 5;
    }

    public void OnClickTankBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        if (BackendGameData.Instance.UserGameData.corn < 1500)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 1500����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 6;
    }

    public void OnClickHealBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        if (BackendGameData.Instance.UserGameData.corn < 1500)
        {
            StartCoroutine(NotEnoughCorn());
            return;
        }
        warningPanel.SetActive(true);
        warningMsg.text = "������ 1500����\n���� �����Ͻðڽ��ϱ�?";
        cornProductId = 7;
    }

    public void YesBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Purchase_Effect);
        if (cornProductId == 0)//�Ļ� ������ ���
        {
            BackendGameData.Instance.UserGameData.corn -= 20;
            BackendGameData.Instance.UserGameData.bread += 5;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
        }
        else if(cornProductId == 1) //�¹��� ������ ���
        {
            BackendGameData.Instance.UserGameData.corn -= 10;
            BackendGameData.Instance.UserGameData.threadmill += 1;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
        }
        else if(cornProductId == 2)
        {
            warningPanel.SetActive(false);
            warningMsg.text = string.Empty;
        }
        else if(cornProductId == 3)
        {
            BackendGameData.Instance.TowerDB.t0 = true;
            BackendGameData.Instance.UserGameData.corn -= 250;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
            BackendGameData.Instance.TowerDataUpdate();
            BackendGameData.Instance.TowerDataLoad();
        }
        else if (cornProductId == 4)
        {
            BackendGameData.Instance.TowerDB.t1 = true;
            BackendGameData.Instance.UserGameData.corn -= 480;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
            BackendGameData.Instance.TowerDataUpdate();
            BackendGameData.Instance.TowerDataLoad();
        }
        else if (cornProductId == 5)
        {
            BackendGameData.Instance.TowerDB.t2 = true;
            BackendGameData.Instance.UserGameData.corn -= 800;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
            BackendGameData.Instance.TowerDataUpdate();
            BackendGameData.Instance.TowerDataLoad();
        }
        else if (cornProductId == 6)
        {
            BackendGameData.Instance.TowerDB.t3 = true;
            BackendGameData.Instance.UserGameData.corn -= 1500;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
            BackendGameData.Instance.TowerDataUpdate();
            BackendGameData.Instance.TowerDataLoad();
        }
        else if (cornProductId == 7)
        {
            BackendGameData.Instance.TowerDB.t4 = true;
            BackendGameData.Instance.UserGameData.corn -= 1500;
            BackendGameData.Instance.GameDataUpdate();
            BackendGameData.Instance.GameDataLoad();
            BackendGameData.Instance.TowerDataUpdate();
            BackendGameData.Instance.TowerDataLoad();
        }
        warningPanel.SetActive(false);
        warningMsg.text = string.Empty;
    }

    public void NoBtn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        warningPanel.SetActive(false);
        warningMsg.text = string.Empty;
    }

    IEnumerator NotEnoughCorn()
    {
        warningPanel.SetActive(true);
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        warningMsg.text = "��������\n�����մϴ�.";
        yield return new WaitForSeconds(1.5f);

        warningMsg.text = string.Empty;
        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
        warningPanel.SetActive(false);
    }

    IEnumerator FullThreadmill()
    {
        warningPanel.SetActive(true);
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        warningMsg.text = "�¹�����\n�ִ�ġ �Դϴ�.";
        yield return new WaitForSeconds(1.5f);

        warningMsg.text = string.Empty;
        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
        warningPanel.SetActive(false);
    }
}
