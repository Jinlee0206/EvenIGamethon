using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        nextExp = new int[] { 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420, 440, 460, 480, 500 };
        Application.targetFrameRate = 60;
        isSelectingCard = false;
        isGameSpeedIncreased = false;

        // �ӽ� Stage01 �׳� �ھƳ���
        AudioManager.Inst.StopBgm();
        AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Chapter01);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
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
