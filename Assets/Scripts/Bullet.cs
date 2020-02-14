using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 10;
    public Rigidbody2D rb;
    public GameObject ImpactEffect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletLife());
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(1.2f);

        if (ImpactEffect)
        {
            Instantiate(ImpactEffect, transform.position, transform.rotation);
        }

        UnityEngine.Object.Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {

            if (ImpactEffect)
            {
                Instantiate(ImpactEffect, transform.position, transform.rotation);
            }

            UnityEngine.Object.Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy")
        {
            AIController ai = col.gameObject.GetComponent<AIController>();
            ai.CurrentHitPoints = ai.CurrentHitPoints - Damage;
        }
    }
}
