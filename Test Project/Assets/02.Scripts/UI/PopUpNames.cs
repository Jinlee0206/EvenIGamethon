using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �˾� �̸��� ����
public class PopUpNames
{
    public string strStageSelectUI { get; set; }
    public string strSettingsUI { get; set; }
    public string strExplainStaminaUI { get; set; }
    public string strExplainCornUI { get; set; }
    public string strProfileUI { get; set; }
    public string strTowerUI { get; set; }
    public string strLevelUpUI { get; set; }
    public string strStageStartUI { get; set; }

    public PopUpNames(string stageSelectUI, string optionsUI, string explainStaminaUI, string explainCornUI, string profileUI, string towerUI, string levelUpUI, string stageStartUI)
    {
        strStageSelectUI = stageSelectUI;
        strSettingsUI = optionsUI;
        strExplainStaminaUI = explainStaminaUI;
        strExplainCornUI = explainCornUI;
        strProfileUI = profileUI;
        strTowerUI = towerUI;
        strLevelUpUI = levelUpUI;
        strStageStartUI = stageStartUI;
    }

}
