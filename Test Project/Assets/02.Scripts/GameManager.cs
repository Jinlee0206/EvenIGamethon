using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// ���׸� Signleton �������� ���� ����ƽ ������ ���� �ʿ䰡 ����
    /// </summary>
    //public static GameManager instance; 

    public Player player;
    public PoolManager pool;
    public Scanner scanner;
    public float gameTime;
    public float waveChangeTime;

    /// <summary>
    /// ���͸� óġ�ϰ� �������� �ϰ� ����
    /// </summary>
    [Header("Player Info")]
    public int level; // �÷��̾��� ���� ����
    public int kill; // �÷��̾��� ���� ų��(UI �� ǥ������ ������ �켱 ���)
    public int exp; // ������� ���� ����ġ 0~100% ���� ǥ��
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // ���� ������ �ʿ��� ����ġ�� ���Ƿ� ���� Test��

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
    public void GetExp()
    {
        exp++;
        // �ʿ� ����ġ�� �����ϸ� ������
        if(exp == nextExp[level])
        {
            level++;
            exp = 0; // ����ġ �ʱ�ȭ
        }
    }
}