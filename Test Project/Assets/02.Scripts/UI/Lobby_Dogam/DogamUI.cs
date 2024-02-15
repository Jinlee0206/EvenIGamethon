using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DogamUI : MonoBehaviour
{
    public Button t_HamzziBtn;
    public Button t_MonsterBtn;
    public Button t_Skill;

    public GameObject hamzzi;
    public GameObject monster;
    public GameObject skill;

    public Button h_PrevBtn;
    public Button h_NextBtn;
    public GameObject[] hamzzis;
    int h_curIdx;

    public Button m_PrevBtn;
    public Button m_NextBtn;
    public GameObject[] monsters;
    int m_curIdx;
    public TextMeshProUGUI chapterText;

    public Button s_PrevBtn;
    public Button s_NextBtn;
    public GameObject[] skills;
    int s_curIdx;
    public TextMeshProUGUI skillText;

    // ��
    public GameObject lobbyGrid; // �κ� �׸���
    public GameObject dogamGrid; // ���� �׸���

    void Awake()
    {
        Init();
    }

    void Init()
    {
        // ���� ��� ��ư
        t_HamzziBtn.onClick.AddListener(() => OnClickTopBtn(hamzzi));
        t_MonsterBtn.onClick.AddListener(() => OnClickTopBtn(monster));
        t_Skill.onClick.AddListener(() => OnClickTopBtn(skill));
        t_HamzziBtn.onClick.Invoke();   // ���� �ܽ��ͷ� �ʱ�ȭ

        // �ܽ��� ��ư
        h_curIdx = 0;
        h_PrevBtn.onClick.AddListener(() => OnClickPrevBtn());
        h_NextBtn.onClick.AddListener(() => OnClickNextBtn());
        h_PrevBtn.gameObject.SetActive(false);
        h_NextBtn.gameObject.SetActive(true);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Voice);

        // ���� ��ư �� é��
        m_curIdx = 0;
        m_PrevBtn.onClick.AddListener(() => OnClickPrevBtn_M());
        m_NextBtn.onClick.AddListener(() => OnClickNextBtn_M());
        m_PrevBtn.gameObject.SetActive(false);
        m_NextBtn.gameObject.SetActive(true);

        // ��ų ��ư �� �ؽ�Ʈ
        s_curIdx = 0;
        s_PrevBtn.onClick.AddListener(() => OnClickPrevBtn_S());
        s_NextBtn.onClick.AddListener(() => OnClickNextBtn_S());
        s_PrevBtn.gameObject.SetActive(false);
        s_NextBtn.gameObject.SetActive(true);

        // ��
        lobbyGrid = FindObjectOfType<Grid>().gameObject.transform.GetChild(0).gameObject;
        dogamGrid = FindObjectOfType<Grid>().gameObject.transform.GetChild(2).gameObject;

        OnPopupOpened();
    }

    private void LateUpdate()
    {
        chapterText.text = string.Format($"0{m_curIdx + 1}");
        if (s_curIdx == 0) skillText.text = string.Format($"���� ��ų");
        else skillText.text = string.Format($"�뺴�� ��ų");
    }

    private void OnClickTopBtn(GameObject objectToActivate)
    {
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_UI);
        hamzzi.SetActive(objectToActivate == hamzzi);
        monster.SetActive(objectToActivate == monster);
        skill.SetActive(objectToActivate == skill);

        // ���� BGM ���� �۾� ���⿡
        if (objectToActivate == hamzzi)
        {
            AudioManager.Inst.StopBgm();
            AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_IllustratedGuideHamster);
        }
        else if (objectToActivate == monster)
        {
            AudioManager.Inst.StopBgm();
            AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_IllustratedGuideMonster);
        }
        else if (objectToActivate == skill)
        {
            AudioManager.Inst.StopBgm();
            AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_IllustratedGuideArchive);
        }

    }

    //private void OnClickNextBtn(int idx, GameObject[] gameObjects)
    //{
    //    idx = (idx + 1) % gameObjects.Length;
    //    Debug.Log(idx);
    //    UpdateHamsters(idx, gameObjects);
    //}

    //private void OnClickPrevBtn(int idx, GameObject[] gameObjects)
    //{
    //    idx = (idx - 1 + gameObjects.Length) % gameObjects.Length;
    //    Debug.Log(idx);
    //    UpdateHamsters(idx, gameObjects);
    //}

    //private void UpdateHamsters(int idx, GameObject[] gameObjects)
    //{
    //    for (int i = 0; i < gameObjects.Length; i++)
    //    {
    //        gameObjects[i].SetActive(i == idx);
    //        Debug.Log(gameObjects[i].gameObject.activeSelf);
    //    }
    //}

    // �ܽ���
    private void OnClickNextBtn()
    {
        h_curIdx = (h_curIdx + 1) % hamzzis.Length;
        //Debug.Log(h_curIdx);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
        UpdateHamsters();
    }

    private void OnClickPrevBtn()
    {
        h_curIdx = (h_curIdx - 1 + hamzzis.Length) % hamzzis.Length;
        //Debug.Log(h_curIdx);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
        UpdateHamsters();
    }

    private void UpdateHamsters()
    {
        for (int i = 0; i < hamzzis.Length; i++)
        {
            hamzzis[i].SetActive(i == h_curIdx);
            //Debug.Log(hamzzis[i].gameObject.activeSelf);
        }

        // ù ��° �������� ���õ� ���
        if (h_curIdx == 0)
        {
            h_PrevBtn.gameObject.SetActive(false);
            h_NextBtn.gameObject.SetActive(true);
        }
        // ������ �������� ���õ� ���
        else if (h_curIdx == hamzzis.Length - 1)
        {
            h_PrevBtn.gameObject.SetActive(true);
            h_NextBtn.gameObject.SetActive(false);
        }
        // �߰� �������� ���õ� ���
        else
        {
            h_PrevBtn.gameObject.SetActive(true);
            h_NextBtn.gameObject.SetActive(true);
        }

        switch (h_curIdx) 
        {
            case 0:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Main_Hamster_Voice);
                break;
            case 1:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Voice_01);
                break;
            case 2:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Voice_02);
                break;
            case 3:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Voice_03);
                break;
            case 4:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Voice_04);
                break;
            case 5:
                AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Sub_Hamster_Voice_05);
                break;
        }

    }

    // ����
    private void OnClickNextBtn_M()
    {
        m_curIdx = (m_curIdx + 1) % monsters.Length;
        //Debug.Log(m_curIdx);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
        UpdateMonsters();
    }

    private void OnClickPrevBtn_M()
    {
        m_curIdx = (m_curIdx - 1 + monsters.Length) % monsters.Length;
        //Debug.Log(m_curIdx);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
        UpdateMonsters();
    }

    private void UpdateMonsters()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].SetActive(i == m_curIdx);
            Debug.Log(monsters[i].gameObject.activeSelf);
        }

        // ù ��° �������� ���õ� ���
        if (m_curIdx == 0)
        {
            m_PrevBtn.gameObject.SetActive(false);
            m_NextBtn.gameObject.SetActive(true);
        }
        // ������ �������� ���õ� ���
        else if (m_curIdx == monsters.Length - 1)
        {
            m_PrevBtn.gameObject.SetActive(true);
            m_NextBtn.gameObject.SetActive(false);
        }
        // �߰� �������� ���õ� ���
        else
        {
            m_PrevBtn.gameObject.SetActive(true);
            m_NextBtn.gameObject.SetActive(true);
        }
    }

    // ��ų
    private void OnClickNextBtn_S()
    {
        s_curIdx = (s_curIdx + 1) % skills.Length;
        //Debug.Log(s_curIdx);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
        UpdateSkills();
    }

    private void OnClickPrevBtn_S()
    {
        s_curIdx = (s_curIdx - 1 + skills.Length) % skills.Length;
        //Debug.Log(s_curIdx);
        AudioManager.Inst.PlaySfx(AudioManager.SFX.SFX_Book_Effect);
        UpdateSkills();
    }

    private void UpdateSkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].SetActive(i == s_curIdx);
            Debug.Log(skills[i].gameObject.activeSelf);
        }

        // ù ��° �������� ���õ� ���
        if (s_curIdx == 0)
        {
            s_PrevBtn.gameObject.SetActive(false);
            s_NextBtn.gameObject.SetActive(true);
        }
        // ������ �������� ���õ� ���
        else if (s_curIdx == skills.Length - 1)
        {
            s_PrevBtn.gameObject.SetActive(true);
            s_NextBtn.gameObject.SetActive(false);
        }
        // �߰� �������� ���õ� ���
        else
        {
            s_PrevBtn.gameObject.SetActive(true);
            s_NextBtn.gameObject.SetActive(true);
        }
    }

    // ��
    public void OnPopupOpened()
    {
        lobbyGrid.SetActive(false);
        dogamGrid.SetActive(true);
    }

    public void OnPopupClosed()
    {
        lobbyGrid.SetActive(true);
        dogamGrid.SetActive(false);
        AudioManager.Inst.StopBgm();
        AudioManager.Inst.PlayBgm(AudioManager.BGM.BGM_Lobby);
    }


}
