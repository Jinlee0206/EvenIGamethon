using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPlayer : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //// �˾� UI�� Ȱ��ȭ�ϴ� ���� �߰�
        //if (eventData.clickCount == 1)
        //{
        //    ShowPopUp();
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void ShowPopUp()
    {
        //// PopUpHandler ������Ʈ ��������
        //PopUpHandler popUpHandler = GetComponent<PopUpHandler>();

        //if (popUpHandler != null)
        //{
        //    popUpHandler.OnClickPopUpProfile();
        //    Debug.LogError("������UI �˾� ã�� �� �����ϴ�.");

        //}
        //else
        //{
        //    Debug.LogError("PopUpHandler�� ã�� �� �����ϴ�.");
        //}
    }
}