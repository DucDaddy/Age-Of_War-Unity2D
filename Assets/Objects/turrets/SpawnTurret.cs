using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTurret : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject turretSpot;
    public GameObject ghostTurret;

    public Text infoTurret;

    private void Start()
    {
        turretSpot = GameObject.Find("Turret Spot");
    }

    private void OnMouseDown()
    {
        ghostTurret = Instantiate(turretPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        ghostTurret.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ghostTurret.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        DestroyImmediate(ghostTurret.GetComponent<TurretController>());
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        ghostTurret.transform.position = newPosition;
    }

    private void OnMouseUp()
    {
        Destroy(ghostTurret);
        // Lấy vị trí của con trỏ chuột
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Tạo một Collider2D tại vị trí của con trỏ chuột và tìm các đối tượng bên trong nó
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("AllyTurret"))
            {
                return;
            }
        }

        BoxCollider2D[] boxColliders = turretSpot.GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D boxCollider in boxColliders)
        {
            if (boxCollider.OverlapPoint(mousePosition) && GameObject.Find("Resources").GetComponent<Resource>().gold >= turretPrefab.GetComponent<TurretController>().cost)
            {
                Vector2 spawnPosition = boxCollider.bounds.center;
                GameObject turret = Instantiate(turretPrefab, spawnPosition, Quaternion.identity);
                turret.tag = "AllyTurret";
                turret.GetComponent<SpriteRenderer>().sortingOrder = 1;
                GameObject.Find("Resources").GetComponent<Resource>().downGold(turretPrefab.GetComponent<TurretController>().cost);
                break;
            }
        }

    }

    private void OnMouseEnter()
    {
        infoTurret.text = turretPrefab.GetComponent<TurretController>().cost.ToString() + "$ - " + turretPrefab.GetComponent<TurretController>().nameTurret;
    }

    private void OnMouseExit()
    {
        infoTurret.text = "";
    }

}