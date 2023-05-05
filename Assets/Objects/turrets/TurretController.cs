using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public string nameTurret;
    public int fallGold;
    public int cost;

    public int damage;
    public float attackRange;
    public float reloadTime;

    public Sprite bulletSprite;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float barrelDistance; //tọa độ X từ đầu nòng đến gốc tọa độ của súng
    public float angle;

    public int state;
    public static readonly int IDLE = 1;
    public static readonly int ATTACK = 2;

    private Animator animator;
    private float countdown;
    private string enemy;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        foreach (var item in animator.runtimeAnimatorController.animationClips)
        {
            if (item.name.Contains("attack"))
            {
                reloadTime = item.length;
            }
        }

        countdown = reloadTime;
    }

    void Update()
    {
        getState();
        switch (state)
        {
            case 1:

                break;
            case 2:
                if (countdown < 0)
                {
                    Attack();
                }
                else
                {
                    countdown -= Time.deltaTime; // Trừ giảm số giây đã trôi qua
                }
                break;
        }

    }

    private void Attack()
    {
        enemy = "Enemy";

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        // Tìm object gần nhất có tag "enemy" để tấn công
        Collider2D nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D target in hits)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < minDistance && target.CompareTag(enemy))
            {
                nearestEnemy = target;
                minDistance = distance;
            }
        }
        if (nearestEnemy != null)
        {

            // Tính toán góc giữa vị trí của object và vị trí của nearestEnemy
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Tạo ra một quaternion mới từ góc tính được và set cho rotation của object
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = rotation;

            countdown = reloadTime; // Đặt lại biến countdown
        }
    }

    private void getState()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                setStateAttack();
                return;
            }
        }
        setStateIdle();
    }

    private void setStateIdle()
    {
        state = IDLE;
        animator.Play("idle");
    }
    private void setStateAttack()
    {
        state = ATTACK;
        animator.Play("attack");
    }

    private void spawnBullet()
    {
        enemy = "Enemy";

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        // Tìm object gần nhất có tag "enemy" để tấn công
        Collider2D nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D target in hits)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < minDistance && target.CompareTag(enemy))
            {
                nearestEnemy = target;
                minDistance = distance;
            }
        }
        if (nearestEnemy != null)
        {
            // Tính toán điểm trung gian giữa vị trí hiện tại của turret và vị trí của nearestEnemy
            Vector2 midPoint = Vector2.Lerp(transform.position, nearestEnemy.transform.position, 0.5f);

            // Tính toán vector barrelDirection từ vị trí hiện tại của turret đến điểm trung gian
            Vector2 barrelDirection = (midPoint - (Vector2)transform.position).normalized;

            // Tính toán vị trí spawnBullet của viên đạn bằng cách cộng tọa độ của turret với khoảng cách barrelDistance nhân với vector barrelDirection
            Vector2 spawnBullet = (Vector2)transform.position +
    new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * Math.Abs(barrelDistance);

            // Sinh ra object mới từ bulletPrefab tại vị trí spawnBullet và quay nó về phía nearestEnemy
            GameObject newBullet = Instantiate(bulletPrefab, spawnBullet, Quaternion.identity);
            newBullet.transform.right = nearestEnemy.transform.position - newBullet.transform.position;
            newBullet.gameObject.GetComponent<BulletController>().setValue(this.bulletSprite, nearestEnemy.gameObject, this.damage, this.bulletSpeed);

        }
    }

}