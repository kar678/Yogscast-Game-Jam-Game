using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainGameLevelScript : MonoBehaviour
{
    public int PresentsCollected = 0;
    public int PresentsToCollect = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtons()
    {
        if (PresentsToCollect == 3)
        {

        }
        else if (PresentsToCollect == 4)
        {

        }
        else if (PresentsToCollect == 5)
        {

        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
