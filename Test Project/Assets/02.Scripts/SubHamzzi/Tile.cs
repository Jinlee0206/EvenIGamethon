using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Ÿ�Ͽ� Ÿ���� �Ǽ��Ǿ� �ִ��� �˻��ϴ� ����
    public bool IsBuildTower { get; set; }

    private void Awake()
    {
        IsBuildTower = false;
    }
}
