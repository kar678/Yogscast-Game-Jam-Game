using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    PlayerHUDController PlayerHud;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHud = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUDController>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            PlayerHud.ShowFinishPanel();
        }
    }
}
