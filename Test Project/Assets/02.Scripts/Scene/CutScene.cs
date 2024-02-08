using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public Image comicImage;
    public Sprite[] comicFrames;
    public Button nextButton;
    public Button prevButton;

    public int currentFrameIndex = 0;

    private void Start()
    {
        ShowCurrentFrame(); // ù��° ������ �����ֱ�
    }

    private void ShowCurrentFrame()
    {
        if (currentFrameIndex >= 0 && currentFrameIndex < comicFrames.Length)
        {
            comicImage.sprite = comicFrames[currentFrameIndex];
        }
        UpdateButtonInteractivity();
    }

    private void UpdateButtonInteractivity()
    {
        // ���� ��ư�� �� �̻� ��ȣ�ۿ����� �ʾƾ� �ϴ� ���
        nextButton.interactable = currentFrameIndex < comicFrames.Length - 1;

        // ���� ��ư�� �� �̻� ��ȣ�ۿ����� �ʾƾ� �ϴ� ���
        prevButton.interactable = currentFrameIndex > 0;
    }


    public void ShowNextFrame()
    {
        currentFrameIndex++;
        if (currentFrameIndex >= comicFrames.Length)
        {
            // ��ȭ�� ���� �������� ���, ���� �۾� ����
            // ���⿡ �ʿ��� �۾� �߰�
        }
        else
        {
            ShowCurrentFrame();
        }
    }

    public void ShowPreviousFrame()
    {
        if (currentFrameIndex <= 0) return;
        currentFrameIndex--;
        ShowCurrentFrame();
    }
}
