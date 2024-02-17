using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject pauseMenu;
    public GameObject victoryUI;
    public GameObject gameOverUI;
    public GameObject tutorialUI;

    public Button speedControlButton; // ��ư �߰�
    public Button victoryUINoButton;
    public Button victoryUIYesButton;
    public Button gameOverUINoButton;
    public Button gameOverUIYesButton;
    public TextMeshProUGUI gameOverUITipText;

    public GameObject speed_2times;  // 2��� ��ư �̹��� 

    public Image[] rewards;

    private void Awake()
    {
        base.Initialize();

        InitSpeedControllBtn();
        UpdateSpeedControllBtn();
    }

    private void Start()
    {
        if (!BackendGameData.Instance.UserGameData.isAdRemoved)
        {
            AdmobManager.instance.ShowInterstitialAd();
            PauseGame();
        }
    }

    void InitSpeedControllBtn()
    {
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(ToggleGameSpeed);
            speed_2times = speedControlButton.gameObject.transform.GetChild(0).gameObject;
        }
    }

    public void UpdateSpeedControllBtn()
    {
        if(GameManager.Inst.isGameSpeedIncreased)
        {
            GameManager.Inst.isGameSpeedIncreased = true;
            speed_2times.SetActive(true);
        }
        else
        {
            GameManager.Inst.isGameSpeedIncreased = false;
            speed_2times.SetActive(false);
        }
    }

    public void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateSpeedControllBtn();
    }

    public void PauseGame()
    {
        GameManager.Inst.Stop();                // ���� �Ŵ����� �ִ� Stop �Լ��� ���� (UIManager�� GameTime�� �����ϴ� �� Non-Logical)
        pauseMenu.SetActive(true);
    }

    public void GoToHome()
    {
        BackendGameData.Instance.GameDataUpdate();
        BackendGameData.Instance.ClearDataUpdate();
        BackendGameData.Instance.StarDataUpdate();
        GameManager.Inst.Resume();
        SceneManager.LoadScene("Lobby");
    }

    public void ResumeGame()
    {
        UpdateSpeedControllBtn();               // �ӵ� ��ư ����ȭ
        pauseMenu.SetActive(false);             // Pause ���� �ӵ� �������� ���� ����
        GameManager.Inst.Resume();
    }

    public void RetryGame()
    {
        GameManager.Inst.Resume();
        //StageSelect.instance.speedIncreased = GameManager.Inst.isGameSpeedIncreased;
        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strStageStartUI);
        //SceneManager.LoadScene("Battle_Proto");
    }

    // ���� �ð� ���� �Լ�
    public void ToggleGameSpeed()
    {
        GameManager.Inst.isGameSpeedIncreased = !GameManager.Inst.isGameSpeedIncreased; // ���� ���

        if (GameManager.Inst.isGameSpeedIncreased)
        {
            Debug.Log("���� �ӵ� : 1.5���");
            Time.timeScale = 1.5f; // ���� �ӵ� 1.5���
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
            PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strStageStartUI);
            //RetryGame();
        }
        else if (StageSelect.instance.stage == 5 && StageSelect.instance.chapter < 4)
        {
            StageSelect.instance.stage = 1;
            StageSelect.instance.chapter++;
            PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strStageStartUI);
            //RetryGame();
        }
        else if (StageSelect.instance.stage == 5 && StageSelect.instance.chapter == 4) // ������ ��
        {
            GoToHome();
        }
    }

    // Ʃ�丮�� Open, Close �Լ�
    public void OpenTutorial()
    {
        pauseMenu.SetActive(false);
        tutorialUI.SetActive(true);
        AdmobManager.instance.DestroyBannerView();
    }

    public void CloseTutorial()
    {
        pauseMenu.SetActive(true);
        tutorialUI.SetActive(false);
        AdmobManager.instance.LoadAd();
    }

    public void TMPDOText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}