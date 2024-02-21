using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public static LevelUpUI Inst = null;
    public TextMeshProUGUI cornCost;
    public TextMeshProUGUI breadCost;

    public TextMeshProUGUI infoText;

    private void Awake()
    {
        Inst = this;
    }

    private void OnEnable()
    {
        cornCost.text = BackendGameData.Instance.UserGameData.cornCostToLevelUp[BackendGameData.Instance.UserGameData.level - 1].ToString();
        breadCost.text = BackendGameData.Instance.UserGameData.levelUpData[BackendGameData.Instance.UserGameData.level - 1].ToString();
        infoText.text = "��������\n��� ��ų ������ +" + (BackendGameData.Instance.UserGameData.damageUpgradeAmount[BackendGameData.Instance.UserGameData.level] - BackendGameData.Instance.UserGameData.damageUpgradeAmount[BackendGameData.Instance.UserGameData.level - 1]).ToString();
    }

    public void ChangeCornText()
    {
        cornCost.text = BackendGameData.Instance.UserGameData.cornCostToLevelUp[BackendGameData.Instance.UserGameData.level - 1].ToString();
        breadCost.text = BackendGameData.Instance.UserGameData.levelUpData[BackendGameData.Instance.UserGameData.level - 1].ToString();
    }
}
