using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PopUpManager���� �˾� �̸�(PopUpNames)�� ������ ����� �� �ִ�
// �̹� �̸��� PopUpManager���� ���� �� �ʱ�ȭ �س���, ���� �Լ��� �߰� �ۼ��ϸ� ��
public class PopUpHandler : MonoBehaviour
{
    public void OnClickExit()
    {
        PopUpManager.Inst.popUpList.Peek().OnClose();
    }

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

}
