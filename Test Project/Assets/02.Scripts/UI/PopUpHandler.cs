using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// PopUpManager���� �˾� �̸�(PopUpNames)�� ������ ����� �� �ִ�
// �̹� �̸��� PopUpManager���� ���� �� �ʱ�ȭ �س���, ���� �Լ��� �߰� �ۼ��ϸ� ��
public class PopUpHandler : MonoBehaviour
{
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
    }

    #endregion

    public void OnClickExit()
    {
        PopUpManager.Inst.popUpList.Peek().OnClose();
    }

    public void OnClickLevelUp()
    {
        //if() ==> �������� ������ ���
        //else ==> �������� �Ұ����� ���
        StartCoroutine(NotEnoughCorn());
    }

    IEnumerator NotEnoughCorn()
    {
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        transform.parent.parent.GetChild(1).gameObject.SetActive(false);
    }
}
