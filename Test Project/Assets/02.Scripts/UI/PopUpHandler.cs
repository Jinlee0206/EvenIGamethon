using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// PopUpManager���� �˾� �̸�(PopUpNames)�� ������ ����� �� �ִ�
// �̹� �̸��� PopUpManager���� ���� �� �ʱ�ȭ �س���, ���� �Լ��� �߰� �ۼ��ϸ� ��
public class PopUpHandler : MonoBehaviour
{
    public GameObject[] lobbyBackground;

    private void Start()
    {
        //�κ�������� ����
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            lobbyBackground[0] = GameObject.Find("Home_Background");
            lobbyBackground[1] = GameObject.Find("Shop_Background");
            lobbyBackground[2] = GameObject.Find("Dogam_Background");
        }
    }

    #region PopUpButton
    public void OnClickPopUpStageSelect()
    {

        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strStageSelectUI);
    }

    public void OnClickPopUpSettings()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strSettingsUI);
    }

    public void OnClickPopUpExplainStamina()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strExplainStaminaUI);
    }

    public void OnClickPopUpExplainCorn()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strExplainCornUI);
    }

    public void OnClickPopUpProfile()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strProfileUI);
    }

    public void OnClickPopUpLevelUp()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strLevelUpUI);
    }

    public void OnClickPopUpStart()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strStageStartUI);
    }

    public void OnClickPopUpShop()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strShopUI);
        lobbyBackground[0].SetActive(false);
        lobbyBackground[1].SetActive(true);
        lobbyBackground[2].SetActive(false);
    }

    public void OnClickPopUpDogam()
    {
        //���� ������ Ȱ��ȭ �ڵ� �ְ�
        lobbyBackground[0].SetActive(false);
        lobbyBackground[1].SetActive(false);
        lobbyBackground[2].SetActive(true);
    }

    #endregion

    public void OnClickExit()
    {
        PopUpManager.Inst.popUpList.Peek().OnClose();
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            lobbyBackground[0].SetActive(true);
            lobbyBackground[1].SetActive(false);
            lobbyBackground[2].SetActive(false);
        }
    }

    public void OnClickLevelUp()
    {
        int level = BackendGameData.Instance.UserGameData.level;
        int bread = BackendGameData.Instance.UserGameData.bread;
        int corn = BackendGameData.Instance.UserGameData.corn;

        //if() ==> �������� ������ ���
        if(bread > BackendGameData.Instance.UserGameData.levelUpData[level - 1] && corn > BackendGameData.Instance.UserGameData.cornCostToLevelUp[level - 1])
        {
            BackendGameData.Instance.UserGameData.bread -= BackendGameData.Instance.UserGameData.levelUpData[level - 1];
            BackendGameData.Instance.UserGameData.level += 1;
            BackendGameData.Instance.GameDataUpdate();
        }
        //else ==> �������� �Ұ����� ���
        else StartCoroutine(NotEnoughCorn());
    }

    IEnumerator NotEnoughCorn()
    {
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        transform.parent.parent.GetChild(1).gameObject.SetActive(false);
    }
}
