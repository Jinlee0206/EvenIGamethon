using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListenerManager : MonoBehaviour
{
    private static EventListenerManager instance;

    // �� �̺�Ʈ �����ʿ� ���� �οﰪ��
    private bool isGameDataLoadListenerAdded = false;
    private bool isClearDataLoadListenerAdded = false;
    private bool isTowerDataLoadListenerAdded = false;
    private bool isStarDataLoadListenerAdded = false;

    // �ν��Ͻ� ������ ���� ������Ƽ
    public static EventListenerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventListenerManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(EventListenerManager).Name);
                    instance = singleton.AddComponent<EventListenerManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // ���ο� ������ EventListenerManager�� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    // �� �̺�Ʈ �����ʿ� ���� ���¸� ��ȯ�ϴ� �޼����
    public bool IsGameDataLoadListenerAdded => isGameDataLoadListenerAdded;
    public bool IsClearDataLoadListenerAdded => isClearDataLoadListenerAdded;
    public bool IsTowerDataLoadListenerAdded => isTowerDataLoadListenerAdded;
    public bool IsStarDataLoadListenerAdded => isStarDataLoadListenerAdded;

    // �� �̺�Ʈ �����ʿ� ���� ���¸� �����ϴ� �޼����
    public void SetGameDataLoadListenerAdded(bool value) => isGameDataLoadListenerAdded = value;
    public void SetClearDataLoadListenerAdded(bool value) => isClearDataLoadListenerAdded = value;
    public void SetTowerDataLoadListenerAdded(bool value) => isTowerDataLoadListenerAdded = value;
    public void SetStarDataLoadListenerAdded(bool value) => isStarDataLoadListenerAdded = value;
}

