using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBuildTower { get; set; } // Ÿ�Ͽ� Ÿ���� �Ǽ��Ǿ� �ִ��� �˻��ϴ� ����

    private void Awake()
    {
        IsBuildTower = false;
    }
}
