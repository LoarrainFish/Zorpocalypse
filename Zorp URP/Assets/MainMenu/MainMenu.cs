using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas MainMenuCanvas;
    public Canvas Controls;


    void Start()
    {
        Controls.gameObject.SetActive(false);
        MainMenuCanvas.gameObject.SetActive(true);
    }


    void Update()
    {

    }

    void Play()
    {
        SceneManager.LoadScene("Level One");
        Debug.Log("Test");
    }

    void ControlsPopup()
    {
        MainMenuCanvas.gameObject.SetActive(false);
        Controls.gameObject.SetActive(true);
    }   

    void Quit()
    {
        Application.Quit();
    }
}
