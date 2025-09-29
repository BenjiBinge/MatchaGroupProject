using System;
using UnityEngine;
using UnityEngine.InputSystem.Editor;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject OptionsScreen;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsMenuOpen()
    {
        OptionsScreen.SetActive(true);
    }

    public void OptionsMenuClose()
    {
        OptionsScreen.SetActive(false);
    }
    private void Start()
    {
        
        OptionsScreen.SetActive(false);
    }

    private void Update()
    {
        
    }
}

