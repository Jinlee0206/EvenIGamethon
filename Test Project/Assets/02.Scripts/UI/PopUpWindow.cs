using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    // �˾� â ���� �Լ�
    public void OnClose()
    {
        PopUpManager.Inst.ClosePopUp(this);
        Destroy(gameObject);
    }
}
