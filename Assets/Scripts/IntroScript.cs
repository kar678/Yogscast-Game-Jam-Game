using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroScript : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Body;
    int TextNumber = 0;
    float timeSinceLastClick;

    // Start is called before the first frame update
    void Start()
    {
        Body.text = "Welcome Humble Player. You have just woken up, its a fine morning in December but everything is not fine. You have just remembered you still have to buy presents for your close family and you only have 12 days left!";
    }

    // Update is called once per frame
    void Update()
    {
        if(TextNumber == 1)
        {
            Body.text = "You've noticed most items are sold out or there is only 1 left, it looks like you are going to have to fight through to get the right present for everyone.";
        }

        if(TextNumber == 2)
        {
            Body.text = "Good luck, just remember don't be too slow";
        }

        if(TextNumber == 3)
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    public void NextText()
    {
        if(timeSinceLastClick < Time.time)
        {
            TextNumber++;
            timeSinceLastClick = Time.time + 1;
        }
    }


}
