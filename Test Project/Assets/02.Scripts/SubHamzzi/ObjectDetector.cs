using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] TowerSpawner towerSpawner;
    Camera mainCamera;
    Ray ray;
    RaycastHit hit;
    private Transform hitTransform;

    public Tile TilePos { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ� ���� ����
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 2D ����͸� ���� 3D ������ ������Ʈ�� ���콺�� �����ϴ� ���
            // ������ �ε����� ������Ʈ�� �����ؼ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    TilePos = hit.transform.GetComponent<Tile>();
                    Debug.Log(TilePos);

                    if (TilePos != null && !TilePos.IsBuildTower) // Ÿ���� �Ǽ��Ǿ� ���� �ʴٸ�
                    {
                        PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strTowerUI);
                    }
                    else return;
                }
            }
        }
    }

    public Transform GetHitTransform()
    {
        return hitTransform;
    }
}