using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public Button buttonArrow;
    public Button buttonBomb;
    public Button buttonBlack;
    public Button buttonTank;
    public Button buttonHeal;

    TowerSpawner spawner;
    ObjectDetector objectDetector;
    PopUpWindow popUpWindow;

    private void Awake()
    {
        objectDetector = FindObjectOfType<ObjectDetector>();
        spawner = FindObjectOfType<TowerSpawner>();
        popUpWindow = GetComponent<PopUpWindow>();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        if (objectDetector == null || spawner == null)
        {
            Debug.LogError("ObjectDetector or TowerSpawner not assigned!");
            return;
        }

        if (buttonArrow != null)
        {
            buttonArrow.onClick.AddListener(() =>
            {
                Transform hitTransform = objectDetector.GetHitTransform();
                if (hitTransform != null)
                {
                    spawner.SpawnTower(hitTransform);
                    StartCoroutine(ClosePopUpAfterDelay());
                }
            });
        }

        // �������� ���� ����ȭ �ʿ�
        /*
        if (buttonBomb != null) {
            buttonBomb.onClick.AddListener(() => spawner.SpawnTower(objectDetector.GetHitTransform()));
        }
        if (buttonBlack != null) {
            buttonBlack.onClick.AddListener(() => spawner.SpawnTower(objectDetector.GetHitTransform()));

        }
        if (buttonTank != null) {
            buttonTank.onClick.AddListener(() => spawner.SpawnTower(objectDetector.GetHitTransform()));

        }
        if (buttonHeal != null) {
            buttonHeal.onClick.AddListener(() => spawner.SpawnTower(objectDetector.GetHitTransform()));
        }
        */
    }

    IEnumerator ClosePopUpAfterDelay()
    {
        yield return new WaitForSeconds(0.3f); // ���ϴ� ��� �ð� ����
        popUpWindow.OnClose();
    }

}
