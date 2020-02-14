using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop2 : MonoBehaviour
{
    ShopSearchZone SearchZone;
    bool ShownHint = false;
    PlayerHUDController PlayerHud;

    // Start is called before the first frame update
    void Start()
    {
        SearchZone = GetComponent<ShopSearchZone>();
        PlayerHud = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SearchZone)
        {
            if (SearchZone.capturePercent == 100 && ShownHint == false)
            {
                ShownHint = true;

                string hTitle = "Balfords";
                string hBody = "You Looked around Balfords but didn't manage to find anything of use and concluded it was raided by robbers. Hopefully the next shop has a bike.";
                PlayerHud.ShowTip(hTitle, hBody);
            }
        }
    }
}
