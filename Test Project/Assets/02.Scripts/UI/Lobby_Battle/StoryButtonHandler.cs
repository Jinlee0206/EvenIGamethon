using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryButtonHandler : MonoBehaviour
{
    int currentChapter;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        currentChapter = 1;

        ChapterButtonHandler[] chapterButtonHandlers = FindObjectsOfType<ChapterButtonHandler>();
        if (chapterButtonHandlers != null)
        {
            foreach(var cbh in chapterButtonHandlers)
            {
                cbh.OnChapterChanged.AddListener(UpdateChapter);
            }
        }
        else
        {
            Debug.LogError("ChapterButtonHandler�� ã�� �� �����ϴ�.");
        }
    }
    
    // UnityEvent ����
    void UpdateChapter()
    {
        currentChapter = StageSelect.instance.chapter;
        Debug.Log(currentChapter);
        CutSceneManager.Inst.cutSceneType = (CutSceneData.CutSceneType)currentChapter;
        Debug.Log(CutSceneManager.Inst.cutSceneType);
    }

    public void LoadChapterCutScene()
    {
        // �񵿱� �ε尡 �Ϸ�� �Ŀ� PlayCutScene �Լ��� ȣ��
        SceneManager.LoadSceneAsync("CutScene").completed += OnLoadCutSceneComplete; // SceneManager.LoadSceneAsync�� ���
    }

    // �ε� �Ϸ� �ݹ��� ����Ͽ� �ε尡 �Ϸ�� �Ŀ� PlayCutScene�� ȣ��
    private void OnLoadCutSceneComplete(AsyncOperation operation)
    {
        if (operation.isDone)
        {
            // �� �ε尡 �Ϸ�� �Ŀ� PlayCutScene�� ȣ��
            CutSceneManager.Inst.PlayCutScene((CutSceneData.CutSceneType)currentChapter);
        }
    }
}
