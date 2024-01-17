using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Time, Health, Wave}
    public InfoType type;

    Text lvText;
    Text timeText;
    Text waveText;
    Slider expSlider;
    Slider hpSlider;

    public Spawner spawner;

    private void Awake()
    {
        lvText = GetComponent<Text>();
        timeText = GetComponent<Text>();
        waveText = GetComponent<Text>();
        expSlider = GetComponent<Slider>();
        hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Inst.exp;
                float maxExp = GameManager.Inst.nextExp[Mathf.Min(GameManager.Inst.level, GameManager.Inst.nextExp.Length - 1)];
                expSlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                lvText.text = string.Format("Lv.{0:F0}", GameManager.Inst.level); // ���ڿ� ����, 0��° ���� �� 
                break;
            case InfoType.Time:
                float time = GameManager.Inst.gameTime;
                int min = Mathf.FloorToInt(time / 60); // ���� �Լ� ���
                int sec = Mathf.FloorToInt(time % 60);
                timeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.Inst.wall.health;
                float maxHealth = GameManager.Inst.wall.maxHealth;
                hpSlider.value = curHealth / maxHealth;
                break;
            case InfoType.Wave:
                waveText.text = string.Format("Wave : {0:D2} / {1:D2}", spawner.currentWave ,spawner.maxWave);
                break;

        }
    }
}
