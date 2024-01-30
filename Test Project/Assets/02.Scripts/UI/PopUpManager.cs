using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// �˾� ��ü�� �����ϴ� �Ŵ���
public class PopUpManager : MonoBehaviour
{
    public GameObject myNoTouch;                                        // �˾� â�� ������ ��ٸ� ���� â�� ���� ���ϰ� �ϴ� �� Image UI ������Ʈ
    public Stack<PopUpWindow> popUpList = new Stack<PopUpWindow>();     // ���� ���� �˾� â�� ���� ��� Stack ���·� ���� ���� â���� close �ϰ� �� ����
    public UnityAction allClose = null;                               // ��ü �˾� â�� �� �ݴ� ���(����� ���� X)

    // PopUpNames ��ü�� ������ ���
    public PopUpNames PopUpNames { get; private set; } = new PopUpNames("StageSelectUI", "SettingsUI", "ExplainStaminaUI", "ExplainCornUI", "ProfileUI", "TowerUI", "LevelUpUI", "StageStartUI", "TowerUpgradeSellUI", "ShopUI");

    public static PopUpManager Inst { get; private set; }               // ����ƽ ������Ƽ�� ���

    private void Awake()
    {
        Inst = this;
    }

    // �˾� ����
    public void CreatePopup(string popUp)
    {
        myNoTouch.SetActive(true);
        myNoTouch.transform.SetAsLastSibling();
        PopUpWindow scp = (Instantiate(Resources.Load(popUp), transform) as GameObject).GetComponent<PopUpWindow>(); // ���������� �̸� ����� ���� UI�� �����ϰ�, PopUpWindow ������Ʈ�� scp�� �Ҵ�
        allClose += scp.OnClose;
        popUpList.Push(scp);
    }

    // �˾� ����
    public void ClosePopUp(PopUpWindow pw)
    {
        allClose -= pw.OnClose;
        popUpList.Pop();
        if(popUpList.Count == 0)
        {
            myNoTouch.SetActive(false);
        }
        else
        {
            myNoTouch.transform.SetSiblingIndex(myNoTouch.transform.GetSiblingIndex() - 1);
        }
    }

    // �˾�â �� ����� Ű ���ٸ� Update������ ����
    private void Update()
    {
        
    }
}
