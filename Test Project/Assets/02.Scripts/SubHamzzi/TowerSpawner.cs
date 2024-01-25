using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        if (tile.IsBuildTower == true) return;           // ���� Ÿ�� �Ǽ��Ǿ� ������ Ÿ���Ǽ� X

        tile.IsBuildTower = true;                        // Ÿ�� �Ǽ��Ǿ� �������� ����

        Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);  // �ӽ�
        // GameManager.Inst.pool.Get(2); Ǯ�Ŵ��� Ȱ��
    }
}
