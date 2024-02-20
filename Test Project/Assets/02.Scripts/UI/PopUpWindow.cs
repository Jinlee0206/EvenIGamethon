using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        PopUpBGMPlay();
    }

    // �˾� â ���� �Լ�
    public void OnClose()
    {
        LobbyBGMPlay();                             // �κ� BGM �ٽ� ����
        PopUpManager.Inst.ClosePopUp(this);
        Destroy(gameObject);
    }

    void PopUpBGMPlay()
    {
        if (gameObject.name == PopUpManager.Inst.PopUpNames.strShopUI)
        {
            Debug.Log(gameObject.name); // �����
            AudioManager.Inst.StopBgm();
            AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Shop);
        }
        // �ٸ� �˾� (����)
    }

    void LobbyBGMPlay()
    {
        if (gameObject.name == PopUpManager.Inst.PopUpNames.strShopUI)
        {
            AudioManager.Inst.StopBgm();
            AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Lobby);
        }
    }
}
