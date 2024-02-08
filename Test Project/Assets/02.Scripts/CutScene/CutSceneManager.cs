using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static CutSceneData;

public class CutSceneManager : Singleton<CutSceneManager>
{
    public CutSceneData[] cutSceneData;
    Image cutSceneImage;

    int currentFrameIndex = 0;
    CutSceneData currentCutSceneData;
    public CutSceneType cutSceneType;

    private void Awake()
    {
        base.Initialize();
        Init();
    }

    void Init()
    {
        cutSceneImage = GameObject.Find("CutSceneImage").GetComponent<Image>();
        Button nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        Button prevButton = GameObject.Find("PrevButton").GetComponent<Button>();
        nextButton.onClick.AddListener(ShowNextFrame);
        prevButton.onClick.AddListener(ShowPreviousFrame);
    }

    void Start()
    {
        // ���� �� ������ �ƾ� ����
        PlayCutScene(cutSceneType);
    }

    // �ƾ� ��� �Լ�
    public void PlayCutScene(CutSceneType cutSceneType)
    {
        // �־��� �ƾ� Ÿ�Կ� �ش��ϴ� CutSceneData�� ã��
        currentCutSceneData = GetCutSceneData(cutSceneType);
        if (currentCutSceneData != null)
        {
            // �ƾ� ��� ó��
            currentFrameIndex = 0;
            ShowFrame(currentCutSceneData.frameSprites[currentFrameIndex]);
        }
    }

    // �־��� �ƾ� Ÿ�Կ� �ش��ϴ� CutSceneData�� ã�� �Լ�
    private CutSceneData GetCutSceneData(CutSceneType cutSceneType)
    {
        foreach (CutSceneData data in cutSceneData)
        {
            if (data.cutSceneType == cutSceneType)
            {
                return data;
            }
        }
        return null; // �ش��ϴ� �ƾ� �����͸� ã�� ���� ���
    }

    // ���� �������� �����ִ� �Լ�
    public void ShowPreviousFrame()
    {
        if (currentFrameIndex <= 0) return;
        currentFrameIndex--;
        ShowCurrentFrame();
    }

    // ���� �������� �����ִ� �Լ�
    public void ShowNextFrame()
    {
        int frameCount = currentCutSceneData.frameSprites.Length;

        if (currentFrameIndex >= frameCount - 1)
        {
            if (cutSceneType == CutSceneType.Opening) MoveToFirstPlay();
            else MoveToLobby();
        }
        else
        {
            // ���� ������ ǥ��
            currentFrameIndex++;
            ShowCurrentFrame();
        }
    }

    // ���� �������� �����ִ� �Լ�
    private void ShowCurrentFrame()
    {
        // ���� �������� ��������Ʈ�� ǥ��
        if (currentFrameIndex >= 0 && currentFrameIndex < currentCutSceneData.frameSprites.Length)
        {
            ShowFrame(currentCutSceneData.frameSprites[currentFrameIndex]);
        }
    }

    // ��������Ʈ�� �̹����� ǥ���ϴ� �Լ�
    private void ShowFrame(Sprite sprite)
    {
        cutSceneImage.sprite = sprite;
    }

    // ���� �������� �ƴ����� FirstPlay ������ ������ �˻�
    void MoveToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    void MoveToFirstPlay()
    {
        SceneManager.LoadScene("FirstPlay");
    }
}