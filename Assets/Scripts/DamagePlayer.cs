using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    PlayerController Player;
    bool InRange;
    float TimeSinceLastDamage;
    float TimeBetweenDamage = 1f;


    // Update is called once per frame
    void Update()
    {
        if(InRange == true && Player && TimeSinceLastDamage < Time.time)
        {
            TimeSinceLastDamage = Time.time + TimeBetweenDamage;
            Player.TakeDamage(0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            Player = hitInfo.gameObject.GetComponent<PlayerController>();
            InRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            InRange = false;
        }
    }
}
