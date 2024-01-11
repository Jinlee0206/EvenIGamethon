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

    private void Awake()
    {
        base.Initialize();
        //instance = this; // ���׸� Singleton ��ũ��Ʈ �ȿ� Initialize()�� ���� �ڱ� �ڽſ� �Ҵ��ϴ� �Լ��� �̸� �����س���
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

}