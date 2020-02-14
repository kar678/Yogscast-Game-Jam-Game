using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop1 : MonoBehaviour
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

                string hTitle = "Bike On";
                string hBody = "You Looked around Bike On and managed to find the perfect bike for Toddy but you have attacted the attention of a lot of snowmen! Get back to the entrance of the town.";
                PlayerHud.ShowTip(hTitle, hBody);
            }
        }
    }
}
