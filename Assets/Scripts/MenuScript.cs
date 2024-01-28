using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject buttons;
    public GameObject howPlay;
    public GameObject credit;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credit.SetActive(true);
        buttons.SetActive(false);
    }

    public void HowToPlay()
    {
        buttons.SetActive(false);
        howPlay.SetActive(true);
        credit.SetActive(false);
    }

    public void Back()
    {
        buttons.SetActive(true);
        howPlay.SetActive(false);
        credit.SetActive(false);
    }
}
