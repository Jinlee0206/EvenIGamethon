using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ObjectDetector : MonoBehaviour
{
    public static ObjectDetector instance;

    [SerializeField] TowerSpawner towerSpawner;
    public TextMeshProUGUI towerText;

    Camera mainCamera;
    Ray ray;
    RaycastHit hit;
    private Transform hitTransform;

    public Tile TilePos { get; set; }

    private void Awake()
    {
        instance = this;
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
                    //Debug.Log(TilePos.transform.position.x);

                    if (!GameManager.Inst.isSelectingCard && PopUpManager.Inst.popUpList.Count < 1)         // ī�� ����â�� �������� �ʰ�, �˾�â�� ���� ���� �ʴٸ� (�ߺ�UI ���� ����)
                    {
                        if (TilePos != null && !TilePos.IsBuildTower) // Ÿ���� �Ǽ��Ǿ� ���� �ʴٸ�
                        {
                            PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strTowerUI);
                        }
                        else if (TilePos != null && TilePos.IsBuildTower)    // Ÿ���� �Ǽ��Ǿ� �ִٸ�
                        {
                            Debug.Log("���׷��̵� UI �˾�");
                            PopUpManager.Inst.CreatePopup(PopUpManager.Inst.PopUpNames.strTowerUpgradeSellUI);
                        }
                        else return;
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

    public void RefreshText()
    {
        towerText.text = " ";
        StartCoroutine(TextClose());
    }

    public IEnumerator TextClose()
    {
        instance.towerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        instance.towerText.gameObject.SetActive(false);
    }

}