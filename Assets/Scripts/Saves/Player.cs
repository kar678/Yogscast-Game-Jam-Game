using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int PresentsToCollect;
    public int PresentsCollected;
    public TextMeshProUGUI PresentText;
    MainGameLevelScript MGLS;
    System.Random rnd = new System.Random();

    void Start()
    {
        if (PlayerPrefs.GetInt("SaveSlot", 0) > 0)
        {
            MGLS = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLevelScript>();

            if (SaveSystem.CheckFileExsits(PlayerPrefs.GetInt("SaveSlot")))
            {
                PlayerData data = SaveSystem.LoadPlayer(PlayerPrefs.GetInt("SaveSlot"));

                PresentsToCollect = data.PresentsToCollect;
                PresentsCollected = data.PresentsCollected;
            }
            else
            {
                PresentsToCollect = rnd.Next(3, 5);
                PresentsCollected = 0;

                SaveSystem.SavePlayer(this, PlayerPrefs.GetInt("SaveSlot"));
            }
        }
    }

    private void FixedUpdate()
    {
        if (PresentText)
        {
            PresentText.text = "Presents To Collect: " + PresentsCollected + "/" + PresentsToCollect;
        }

        if(MGLS)
        {
            MGLS.PresentsToCollect = PresentsToCollect;
            MGLS.PresentsCollected = PresentsCollected;
            MGLS.StartButtons();
        }
    }

    public void SaveUserData()
    {
        if (PlayerPrefs.GetInt("SaveSlot", 0) > 0)
        {
            SaveSystem.SavePlayer(this, PlayerPrefs.GetInt("SaveSlot"));
        }
    }
}
