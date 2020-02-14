using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMenu : MonoBehaviour
{
    Player Save;

    private void Start()
    {
        Save = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(Save, PlayerPrefs.GetInt("SaveSlot"));
    }
}
