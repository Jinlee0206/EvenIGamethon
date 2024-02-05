using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public static StageSelect instance;

    public int chapter;
    public int stage;
    public int max_chapter;
    public int min_chapter;

    //public bool speedIncreased; // �ٱ����� �ʴ� ���� �ӽ� ����

    private void Awake()
    {
        Initialize();
        //speedIncreased = false;
        chapter = 1;
        stage = 1;
        max_chapter = 4;
        min_chapter = 1;
    }

    protected void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ ���ӿ�����Ʈ�� �ı����� �ʴ´� 
        }
        else
        {
            Destroy(this);
        }
    }

    public void SceneLoad()
    {
        GameManager.Inst.Resume();          // ���� �簳
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle_Proto");

        // �ε��� �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // �ε��� �Ϸ�� �Ŀ� ȣ��
        UIManager.Inst.UpdateSpeedControllBtn();
    }

    public void OnClickStage1()
    {
        stage = 1;
    }

    public void OnClickStage2()
    {
        stage = 2;
    }

    public void OnClickStage3()
    {
        stage = 3;
    }

    public void OnClickStage4()
    {
        stage = 4;
    }

    public void OnClickStage5()
    {
        stage = 5;
    }
}
