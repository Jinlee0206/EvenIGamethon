using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

// PopUpManager���� �˾� �̸�(PopUpNames)�� ������ ����� �� �ִ�
// �̹� �̸��� PopUpManager���� ���� �� �ʱ�ȭ �س���, ���� �Լ��� �߰� �ۼ��ϸ� ��
public class PopUpHandler : MonoBehaviour
{
    [System.Serializable]
    public class LobbyLoadEvent : UnityEvent { }
    public LobbyLoadEvent lobbyLoadEvent = new LobbyLoadEvent();

    public UnityEvent onShopBackground;
    public UnityEvent onDogamBackground;

    private static PopUpHandler instance = null;
    public static PopUpHandler Inst
    {
        get
        {
            if (instance == null)
            {
                instance = new PopUpHandler();
            }

            return instance;
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
        onShopBackground.Invoke();
    }

    public void OnClickPopUpDogam()
    {
        onDogamBackground.Invoke();
    }
    #endregion

    public void OnClickExit()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            Debug.Log("�κ� ��ȯ!");
            lobbyLoadEvent.Invoke();
        }
        PopUpManager.Inst.popUpList.Peek().OnClose();
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
