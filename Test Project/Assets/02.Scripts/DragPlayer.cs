using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPlayer : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // �˾� UI�� Ȱ��ȭ�ϴ� ���� �߰�
        if (eventData.clickCount == 1)
        {
            ShowPopUp();
        }
    }

    private void ShowPopUp()
    {
        // PopUpHandler ������Ʈ ��������
        PopUpHandler popUpHandler = GetComponent<PopUpHandler>();

        if (popUpHandler != null)
        {
            popUpHandler.OnClickPopUpProfile();
        }
        else
        {
            Debug.LogError("PopUpHandler�� ã�� �� �����ϴ�.");
        }
    }
}
