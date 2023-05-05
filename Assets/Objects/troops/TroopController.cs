using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour
{
    public string nameTroop;
    public int cost;
    public float loadTime;
    public float moveSpeed = 0.5f;

    public int HP;
    public int damage;
    public float attackRange;
    public float reloadTime;

    public int fallGold;
    public int fallExp;

    public float posY;

    public int state;
    public static readonly int IDLE = 1;
    public static readonly int MOVE = 2;
    public static readonly int ATTACK = 3;
    public static readonly int DIE = 4;

    private BoxCollider2D bCollider;
    private Animator animator;
    private float countdown;
    private string enemy, enemyhold;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        animator = gameObject.GetComponent<Animator>();
        bCollider = gameObject.GetComponent<BoxCollider2D>();

        foreach (var item in animator.runtimeAnimatorController.animationClips)
        {
            if (item.name.Contains("attack"))
            {
                reloadTime = item.length;
            }
        }

        countdown = reloadTime;

        if (gameObject.tag == "Ally")
        {
            CommonData.troopPositions.Add(transform);
        }
        else if (gameObject.tag == "Enemy")
        {
            CommonData.botPositions.Add(transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Ally")
        {
            getAllyState();
        }
        else if (gameObject.tag == "Enemy")
        {
            getEnemyState();
        }
        switch (state)
        {
            case 1:

                break;
            case 2:
                if (gameObject.tag == "Ally")
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                else if (gameObject.tag == "Enemy")
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                break;
            case 3:
                if (countdown < 0)
                {
                    Attack();
                }
                else
                {
                    countdown -= Time.deltaTime; // Trừ giảm số giây đã trôi qua
                }
                break;
            case 4:
                Destroy(this.gameObject, 3);
                break;
        }
        
    }

    private void Attack()
    {
        if (gameObject.tag == "Ally")
        {
            enemy = "Enemy";
            enemyhold = "EnemyHold";
        }
        else if(gameObject.tag == "Enemy")
        {
            enemy = "Ally";
            enemyhold = "AllyHold";
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange + gameObject.GetComponent<BoxCollider2D>().size.x / 2);
        // Tìm object gần nhất có tag "enemy" để tấn công
        Collider2D nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D target in hits)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < minDistance && (target.CompareTag(enemy) || target.CompareTag(enemyhold)))
            {
                nearestEnemy = target;
                minDistance = distance;
            }
        }
        if (nearestEnemy != null)
        {
            if (nearestEnemy.CompareTag(enemy))
            {
                nearestEnemy.GetComponent<TroopController>().TakeDamage(this.damage);
            }
            else if (nearestEnemy.CompareTag(enemyhold))
            {
                nearestEnemy.GetComponentInParent<StrongHoldController>().TakeDamage(this.damage);
            }
            countdown = reloadTime; // Đặt lại biến countdown
        }
    }

    private void getAllyState()
    {
        int index = CommonData.troopPositions.IndexOf(transform);

        if (index > 0)
        {
            Transform previousUnit = CommonData.troopPositions[index - 1];
            BoxCollider2D colliderPU = previousUnit.GetComponent<BoxCollider2D>();

            // Kiểm tra va chạm với previousUnit
            if (Math.Abs(transform.localPosition.x - previousUnit.localPosition.x) >= (bCollider.size.x / 2 + colliderPU.size.x / 2 + 0.3f))
            {
                setStateMove();
                return;
            }
            else
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange + gameObject.GetComponent<BoxCollider2D>().size.x / 2);
                foreach (Collider2D hit in hits)
                {
                    if (hit.CompareTag("Enemy") || hit.CompareTag("EnemyHold"))
                    {
                        setStateAttack();
                        return;
                    }
                }
                setStateIdle();
                return;
            }
        }
        else if (index == 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange + gameObject.GetComponent<BoxCollider2D>().size.x / 2);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy") || hit.CompareTag("EnemyHold"))
                {
                    setStateAttack();
                    return;
                }
            }
            setStateMove();
            return;
        }

    }

    private void getEnemyState()
    {
        int index = CommonData.botPositions.IndexOf(transform);

        if (index > 0)
        {
            Transform previousUnit = CommonData.botPositions[index - 1];
            BoxCollider2D colliderPU = previousUnit.GetComponent<BoxCollider2D>();

            // Kiểm tra va chạm với previousUnit
            if (Math.Abs(transform.localPosition.x - previousUnit.localPosition.x) >= (bCollider.size.x / 2 + colliderPU.size.x / 2 + 0.3f))
            {
                setStateMove();
                return;
            }
            else
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange + gameObject.GetComponent<BoxCollider2D>().size.x / 2);
                foreach (Collider2D hit in hits)
                {
                    if (hit.CompareTag("Ally") || hit.CompareTag("AllyHold"))
                    {
                        setStateAttack();
                        return;
                    }
                }
                setStateIdle();
                return;
            }
        }
        else if (index == 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange + gameObject.GetComponent<BoxCollider2D>().size.x / 2);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Ally") || hit.CompareTag("AllyHold"))
                {
                    setStateAttack();
                    return;
                }
            }
            setStateMove();
            return;
        }

    }

    private void setStateIdle()
    {
        state = IDLE;
        animator.Play("idle");
    }
    private void setStateMove()
    {
        state = MOVE;
        animator.Play("move");
    }
    private void setStateAttack()
    {
        state = ATTACK;
        animator.Play("attack");
    }
    private void setStateDie()
    {
        state = DIE;
        animator.Play("die");
        if (gameObject.tag == "Ally")
        {
            CommonData.troopPositions.RemoveAt(0);
        }
        else if (gameObject.tag == "Enemy")
        {
            CommonData.botPositions.RemoveAt(0);
            GameObject.Find("Resources").GetComponent<Resource>().upGold(this.fallGold);
            GameObject.Find("Resources").GetComponent<Resource>().upExp(this.fallExp);
        }
        gameObject.tag = "Untagged";
    }

    public void TakeDamage(int enemyDamage)
    {
        this.HP -= enemyDamage;
        if (this.HP <= 0)
        {
            setStateDie();
        }
    }

}
