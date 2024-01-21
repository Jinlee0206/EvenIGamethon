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

    [Header("Game Object")]
    public Player player;
    public PoolManager pool;
    public Scanner scanner;
    public LevelUp uiLevelUp;

    /// <summary>
    /// ���͸� óġ�ϰ� �������� �ϰ� ����
    /// </summary>
    [Header("Player Info")]
    public int level; // �÷��̾��� ���� ����
    public int kill; // �÷��̾��� ���� ų��(UI �� ǥ������ ������ �켱 ���)
    public int exp; // ������� ���� ����ġ 0~100% ���� ǥ��
    public int[] nextExp = { 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420, 440, 460, 480, 500 }; // ���� ������ �ʿ��� ����ġ�� ���Ƿ� ���� Test��

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


    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    // ����ġ ���� �Լ�
    public void GetExp(int killExp)
    {
        exp += killExp;
        // �ʿ� ����ġ�� �����ϸ� ������
        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)] && level < 20)
        {
            level++;
            exp = 0;          // ����ġ �ʱ�ȭ
            uiLevelUp.Show(); // ������ UI �ѱ�
        }
    }

    // ���� �ð� ����
    public void Stop()
    {
        Time.timeScale = 0.0f;
    }

    // ���� �ð� �簳
    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

}