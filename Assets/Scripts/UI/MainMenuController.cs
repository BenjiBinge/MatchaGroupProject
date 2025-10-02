using System;
using UnityEngine;
using UnityEngine.InputSystem.Editor;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject OptionsScreen;
    public GameObject CreditsScreen;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Skip1()
    {
        SceneManager.LoadScene("Level2");
    }

    public void skip2()
    {
        SceneManager.LoadScene("Level3");
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

    public void CreditsOpen()
    {
        CreditsScreen.SetActive(true);
    }

    public void CreditsClose()
    {
        CreditsScreen.SetActive(false);
        OptionsScreen.SetActive(false);
    }
    
    private void Start()
    {
        OptionsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    private void Update()
    {
        
    }
}

