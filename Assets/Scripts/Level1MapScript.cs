using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1MapScript : MonoBehaviour
{
    PlayerHUDController PlayerHud;
    public bool ObjectiveCompleted;
    bool Done = false;
    public string Objective;
    public TextMeshProUGUI ObjectiveText;
    public Toggle CheckBox;
    public GameObject ExitZone;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHud = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUDController>();
        StartCoroutine(StartMission());
        ObjectiveText.text = Objective;
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectiveCompleted && Done == false)
        {
            Done = true;
            CheckBox.isOn = true;
            ExitZone.SetActive(true);
        }
    }

    IEnumerator StartMission()
    {
        yield return new WaitForSeconds(1);

        if(PlayerHud)
        {
            string hTitle = "You've Arrived, But...";
            string hBody = "While on your way to the location of these three stores someone came up to and told you to watch out for killer snowmen. So you have equiped your gun ready for anything. When you arrvied you discovered the town is deserted and the only things here are those snowmen and the bike you need. You are ready.";
            PlayerHud.ShowTip(hTitle, hBody);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            string hTitle = "Searching the Shop";
            string hBody = "When you are near a shop go into it then you'll start to search it. But while doing this more Snowmen might be attracted to your location. Be on the look out.";
            PlayerHud.ShowTip(hTitle, hBody);
        }
    }
}
