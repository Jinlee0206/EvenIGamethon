using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject pauseMenu;
    public GameObject victoryUI;
    public GameObject gameOverUI;

    public Button speedControlButton; // ��ư �߰�
    public Button victoryUINoButton;
    public Button victoryUIYesButton;
    public Button gameOverUINoButton;
    public Button gameOverUIYesButton;

    GameObject speed_2times;  // 2��� ��ư �̹��� 

    public Image[] rewards;

    private void Awake()
    {
        this.Initialize();

        InitSpeedControllBtn();
    }

    void InitSpeedControllBtn()
    {
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(ToggleGameSpeed);
            speed_2times = speedControlButton.gameObject.transform.GetChild(0).gameObject;
            speed_2times.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void GoToHome()
    {
        GameManager.Inst.Resume();
        SceneManager.LoadScene("Lobby");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Battle_Proto");
    }

    // ���� �ð� ���� �Լ�
    public void ToggleGameSpeed()
    {
        GameManager.Inst.isGameSpeedIncreased = !GameManager.Inst.isGameSpeedIncreased; // ���� ���

        if (GameManager.Inst.isGameSpeedIncreased)
        {
            Debug.Log("���� �ӵ� : 1.5���");
            Time.timeScale = 1.5f; // ���� �ӵ� 2���
            speed_2times.SetActive(true);
        }
        else
        {
            Debug.Log("���� �ӵ� : 1���");
            Time.timeScale = 1.0f; // ���� �ӵ���
            speed_2times.SetActive(false);
        }
    }

    // Ŭ���� �� ���� ���������� �̵��ϴ� �Լ� (VictoryUI�� Yes�� ������ ��)
    public void GoToNextStage()
    {
        if (StageSelect.instance.stage < 5)
        {
            StageSelect.instance.stage++;
            RetryGame();
        }
        else if (StageSelect.instance.stage == 5 && StageSelect.instance.chapter < 4)
        {
            StageSelect.instance.stage = 1;
            StageSelect.instance.chapter++;
            RetryGame();
        }
        else if (StageSelect.instance.stage == 5 && StageSelect.instance.chapter == 4) // ������ ��
        {
            GoToHome();
        }
    }
}