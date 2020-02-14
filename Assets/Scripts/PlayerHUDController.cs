using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHUDController : MonoBehaviour
{
    public Slider HealthSlider;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ReloadText;
    PlayerController PlayerC;
    public GameObject TipObject;
    public TextMeshProUGUI TipTitle;
    public TextMeshProUGUI TipBody;
    public Button ContinueButton;
    public bool ShowingTip;
    public bool IsReloading = false;
    public GameObject Finish;
    public GameObject Failed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerC)
        {
            HealthSlider.value = PlayerC.CurrentHitPoints / PlayerC.MaxHitPoints;
            HealthText.text = PlayerC.CurrentHitPoints + "/" + PlayerC.MaxHitPoints;
        }
    }

    public void ShowTip(string Title, string Body)
    {
        ShowingTip = true;

        TipTitle.text = Title;
        TipBody.text = Body;
        TipObject.SetActive(true);
        ContinueButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideTip()
    {
        ShowingTip = false;

        TipObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetReloading(bool Reloading)
    {
        ReloadText.gameObject.SetActive(Reloading);
        IsReloading = Reloading;
    }

    public void ShowFinishPanel()
    {
        Finish.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowFailedPanel()
    {
        Failed.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitMission()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
    }
}
