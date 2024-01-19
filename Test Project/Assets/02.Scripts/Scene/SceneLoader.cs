using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    private void Awake()
    {
        base.Initialize_DontDestroyOnLoad();
    }

    public void ChangeScene(int i)
    {
        StartCoroutine(Loading(i));
    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync(2);            // ��׶��忡�� �񵿱�� ���� �ε�
        AsyncOperation op = SceneManager.LoadSceneAsync(i);     // �ڷ�ƾ�� ���� ��Ȳ�� Ȯ���ϱ� ����
        op.allowSceneActivation = false;                        // �� �ε��� ������ �ٷ� Ȱ��ȭ �ǰ� �ϴ� �� �� => false

        Slider slider = FindObjectOfType<Slider>();

        float targetProgress = 0.9f;                            // �ε��� ���� ������ �����
        float smoothTimeInitial = 0.5f;                         // �ʱ⿡ �ε� �ٰ� �� ���� ������ ���Ǵ� �ð�
        float smoothTimeFinal = 0.2f;                           // 90% ���ķ� ����� ���� ������ ���Ǵ� �ð�
        float velocity = 0.0f;                                  // ������ ���Ǵ� �ӵ�

        while (!op.isDone)
        {
            float currentProgress = op.progress / 0.9f;                                         // ���� �������� ����� ���

            // slider.value = Mathf.Lerp(slider.value, currentProgress, Time.deltaTime * 7f);   // Lerp�� ���� ����

            // �ʱ⿡ �ε� �ٰ� �� ���� ����
            if (currentProgress < targetProgress)
            {
                slider.value = Mathf.SmoothDamp(slider.value, targetProgress, ref velocity, smoothTimeInitial);
            }
            // 90% ���ķ� ����� ���� ����
            else
            {
                slider.value = Mathf.SmoothDamp(slider.value, currentProgress, ref velocity, smoothTimeFinal);
            }

            Debug.Log(slider.value);

            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(1.5f);          // Debug�� ������ �ε��Ϸ�Ǹ� 1�� ������ �� �� Ȱ��ȭ
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
