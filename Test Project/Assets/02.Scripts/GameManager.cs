using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// ���׸� Signleton �������� ���� ����ƽ ������ ���� �ʿ䰡 ����
    /// </summary>
    //public static GameManager instance; 

    [Header("Game Control")]
    public bool isLive;
    public float gameTime;
    public float waveChangeTime;
    public bool isGameSpeedIncreased;

    [Header("Game Object")]
    public Player player;
    public PoolManager pool;
    public Scanner scanner;
    public LevelUp[] uiLevelUps;                // LevelUpUI �迭�� ����

    /// <summary>
    /// ���͸� óġ�ϰ� �������� �ϰ� ����
    /// </summary>
    [Header("Player Info")]
    public int level; // �÷��̾��� ���� ����
    public int kill; // �÷��̾��� ���� ų��(UI �� ǥ������ ������ �켱 ���)
    public int exp; // ������� ���� ����ġ 0~100% ���� ǥ��
    public int[] nextExp; // ���� ������ �ʿ��� ����ġ�� ���Ƿ� ���� Test��
    public int seed;  // �̹� ���ӿ��� ȹ���� �عٶ�� ��
    public bool isSelectingCard;  // ī�� ���� ���� ������ ��, �ٸ� PopUpâ ���� �Ұ�

    #region
    /// <summary>
    /// Player ���� ü��, �ִ� ü�¿� ���õ� Data �ε� �̰� ����ü�� ����� �ν� ������ ������ ���� ���� �ʿ�
    /// �ӽ� UI Test�� �ӽ� �ۼ��̶� �����Ͻø� �˴ϴ�.
    /// </summary>
    /// 
    [HideInInspector]
    public Wall wall; // �� ����, HP �����͸� ���� ������ ����
    #endregion

    private void Awake()
    {
        base.Initialize();
        //instance = this; // ���׸� Singleton ��ũ��Ʈ �ȿ� Initialize()�� ���� �ڱ� �ڽſ� �Ҵ��ϴ� �Լ��� �̸� �����س���
    }

    private void Start()
    {
        kill = 0;
        exp = 0;
        seed = 0;
        level = 0;
        nextExp = new int[] { 60, 90, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420, 440, 460 };
        Application.targetFrameRate = 60;
        isSelectingCard = false;
        isGameSpeedIncreased = false;
        Resume();

        // �ӽ� Stage01 �׳� �ھƳ���
        AudioManager.Inst.StopBgm();
        switch(StageSelect.instance.chapter)
        {
            case 1:
                AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Chapter01);
                break;
            case 2:
                AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Chapter02);
                break;
            default:
                AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Chapter01);
                break;
        }
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.L))
        {
            foreach(var uiLevelUp in uiLevelUps)
            {
                uiLevelUp.Show();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Inst.PauseGame();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            seed = 100;
        }
    }

    // ����ġ ���� �Լ�
    public void GetExp(int killExp)
    {
        if (level == 20) return;
        exp += killExp;
        Debug.Log("����ġ ȹ��" + killExp);
        // �ʿ� ����ġ�� �����ϸ� ������
        // if(exp >= nextExp[level] && level < 20)
        if (exp >= nextExp[level])
        {
            level++;
            exp -= nextExp[level - 1];          // ����ġ �ʱ�ȭ
            foreach (var uiLevelUp in uiLevelUps) uiLevelUp.Show(); // ������ UI �ѱ�
            AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_In_Game_Level_Up);
        }
    }

    public void GetSeed(int getSeed)
    {
        if (seed >= 100)
        {
            seed = 100;
            return;               // �õ� ���Ѽ� 100��
        }
        seed += getSeed;
    }

    // ���� �ð� ����
    public void Stop()
    {
        Time.timeScale = 0f;
    }

    // ���� �ð� �簳
    public void Resume()
    {
        if (isGameSpeedIncreased)
        {
            Time.timeScale = 1.5f;
        }
        else Time.timeScale = 1.0f;
    }
}
