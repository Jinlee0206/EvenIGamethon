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
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Battle_Effect);
    }

    public void OnClickPopUpSettings()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strSettingsUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
    }

    public void OnClickPopUpExplainStamina()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strExplainStaminaUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Wheel);

    }

    public void OnClickPopUpExplainCorn()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strExplainCornUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Corn);
    }

    public void OnClickPopUpProfile()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strProfileUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
    }

    public void OnClickPopUpLevelUp() //������ �Ͻðڽ��ϱ�?
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strLevelUpUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
    }

    public void OnClickPopUpStart()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strStageStartUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Battle_Effect);
    }

    public void OnClickPopUpShop()
    {
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strShopUI);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Shop_Effect);
        onShopBackground.Invoke();
    }

    public void OnClickPopUpDogam()
    {
        onDogamBackground.Invoke();
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
    }
    #endregion

    public void OnClickExit()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
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
        if (bread > BackendGameData.Instance.UserGameData.levelUpData[level - 1] && corn > BackendGameData.Instance.UserGameData.cornCostToLevelUp[level - 1] && level < 20)
        {
            BackendGameData.Instance.UserGameData.bread -= BackendGameData.Instance.UserGameData.levelUpData[level - 1];
            BackendGameData.Instance.UserGameData.corn -= BackendGameData.Instance.UserGameData.cornCostToLevelUp[level - 1];
            BackendGameData.Instance.UserGameData.level += 1;

            LevelUpUI.Inst.ChangeCornText();
            BackendGameData.Instance.GameDataUpdate();
            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Lobby_Hamster_Level_Up);
        }
        //else ==> �������� �Ұ����� ���
        else
        {
            //�ӽ�
            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
            if(bread < BackendGameData.Instance.UserGameData.levelUpData[level - 1])
            {
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
                //���� �����մϴ� �޽��� ����
                StartCoroutine(NotEnoughBread());
            }
            else if (corn < BackendGameData.Instance.UserGameData.cornCostToLevelUp[level - 1])
            {
                StartCoroutine(NotEnoughCorn());
            }
        }
    }

    IEnumerator NotEnoughCorn()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        transform.parent.parent.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator NotEnoughBread()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        transform.parent.parent.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        transform.parent.parent.GetChild(2).gameObject.SetActive(false);
    }
}
