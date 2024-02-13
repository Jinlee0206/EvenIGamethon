using UnityEngine;
using Google.Play.AppUpdate;
using Google.Play.Common;
using System.Collections;

public class InAppUpdate : MonoBehaviour
{
    AppUpdateManager appUpdateManager;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.Log("�ξ� ������Ʈ ����");
#elif UNITY_ANDROID
        StartCoroutine(CheckForUpdate());
#endif
    }

    IEnumerator CheckForUpdate()
    {
        appUpdateManager = new AppUpdateManager();

        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
            appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfoOperation;

        if(appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            //������Ʈ ����
            if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                //��� ������Ʈ(������Ʈ ���ϸ� ���� �Ұ�)
                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);
                yield return startUpdateRequest;

                while(!startUpdateRequest.IsDone)
                {
                    if(startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        Debug.Log("������Ʈ �ٿ�ε尡 �������Դϴ�.");
                    }
                    else if(startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.Log("������Ʈ�� �Ϸ�Ǿ����ϴ�.");
                    }
                    yield return null;
                }

                var result = appUpdateManager.CompleteUpdate();
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return (int)startUpdateRequest.Status;
            }
            //������Ʈ�� ���� ���
            else if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.Log("������Ʈ ����");
                yield return (int)UpdateAvailability.UpdateAvailable;
            }
            else
            {
                Debug.Log("���� ����");
                yield return (int)UpdateAvailability.Unknown;
            }
        }
        else
        {
            //appUpdateInfoOperation.Error ���
            Debug.Log("Error!");
        }
    }
}
