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
    public Image cutSceneImage;
    public Button nextButton;
    public Button prevButton;

    int currentFrameIndex = 0;
    CutSceneData currentCutSceneData;
    public CutSceneType cutSceneType;

    void Awake()
    {
        Init();
        base.Initialize_DontDestroyOnLoad();
    }

    void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� ������ �̹����� ��ư�� ã�Ƽ� ����
        if (scene.name == "CutScene")
        {
            cutSceneImage = GameObject.Find("CutSceneImage").GetComponent<Image>();
            nextButton = GameObject.Find("NextButton").GetComponent<Button>();
            prevButton = GameObject.Find("PrevButton").GetComponent<Button>();

            if (nextButton != null)
            {
                nextButton.onClick.AddListener(ShowNextFrame);
            }

            if (prevButton != null)
            {
                prevButton.onClick.AddListener(ShowPreviousFrame);
            }
        }
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