using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongHoldController : MonoBehaviour
{
    public int HP = 500;

    public GameObject bloodBar;

    private float scaleY;

    private void Start()
    {
        scaleY = bloodBar.transform.localScale.y;
    }

    private void Update()
    {
        if (HP < 0)
        {
            Time.timeScale = 0;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else

#endif
        }
    }

    public void TakeDamage(int enemyDamage)
    {
        
        float reduce = ((float)enemyDamage / 500f);
        bloodBar.transform.localScale = new Vector3(bloodBar.transform.localScale.x, bloodBar.transform.localScale.y - scaleY * reduce, 0);
        //bloodBar.GetComponent<SpriteRenderer>().size = new Vector2(bloodBar.GetComponent<SpriteRenderer>().size.x, bloodBar.GetComponent<SpriteRenderer>().size.y - reduce);
        this.HP -= enemyDamage;
    }

}
