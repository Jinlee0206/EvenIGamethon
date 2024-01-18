using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public Button speedControlButton; // ��ư �߰�

    private bool isGameSpeedIncreased = false; // �⺻ 1���

    private void Start()
    {
        if(speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(ToggleGameSpeed);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Lobby");
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
        SceneManager.LoadScene("Home_Proto");
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

    public void ToggleGameSpeed()
    {
        isGameSpeedIncreased = !isGameSpeedIncreased; // ���� ���

        if (isGameSpeedIncreased)
        {
            Debug.Log("���� �ӵ� : 1.5���");
            Time.timeScale = 1.5f; // ���� �ӵ� 2���
        }
        else
        {
            Debug.Log("���� �ӵ� : 1���");
            Time.timeScale = 1.0f; // ���� �ӵ���
        }
    }
}
