using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTurret : MonoBehaviour
{
    public Sprite sellIcon;

    private GameObject sellObject;
    private bool isDragging = false;

    void Update()
    {
        if (isDragging)
        {
            // Vẽ sellIcon ở vị trí con trỏ chuột
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sellObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    void OnMouseDown()
    {
        // Tạo mới đối tượng sellIcon
        sellObject = new GameObject();
        SpriteRenderer spriteRenderer = sellObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sellIcon;

        // Đặt vị trí ban đầu của sellIcon là vị trí của đối tượng hiện tại
        sellObject.transform.position = transform.position;

        // Bật cờ isDragging để bắt đầu kéo
        isDragging = true;
    }

    void OnMouseUp()
    {
        // Lấy vị trí của con trỏ chuột
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Tạo một Collider2D tại vị trí của con trỏ chuột và tìm các đối tượng bên trong nó
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

        // Kiểm tra từng Collider2D trong danh sách và xóa đối tượng có tag "AllyTurret"
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("AllyTurret"))
            {
                GameObject.Find("Resources").GetComponent<Resource>().upGold(collider.gameObject.GetComponent<TurretController>().fallGold);
                Destroy(collider.gameObject);
                break; // Nếu đã xóa đối tượng, thoát khỏi vòng lặp
            }
        }

        // Xóa sellIcon và tắt cờ isDragging
        Destroy(sellObject);
        isDragging = false;
    }
}
