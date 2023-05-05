using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int damage;
    private float speed;

    private GameObject target;

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (transform.position == target.transform.position)
            {
                target.GetComponent<TroopController>().TakeDamage(this.damage);
                Destroy(this.gameObject);
            }
        }
    }

    public void setValue(Sprite sprite, GameObject target, int damage, float speed)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }
}
