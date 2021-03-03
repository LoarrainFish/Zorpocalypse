using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas MainMenuCanvas;


    void Start()
    {
    }


    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene("Level One");
        Debug.Log("Test");
    }  

    public void Quit()
    {
        Application.Quit();
    }

    
}
