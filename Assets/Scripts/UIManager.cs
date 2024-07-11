using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject UIText;
    public bool Paused;
    public int mainMenu;
    public int restart;
    public AudioSource UISfx;
    public AudioClip ButtonClip;

    public GameObject player;
    private Player_Controller playerController;
    private string playerTag = "Player";

    private void Start()
    {
        if (player != null)
        {
            playerController = player.GetComponent<Player_Controller>();
        }
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape) && playerController.isAlive)
        {
            PauseMenu();
        }
    }

    public void UISfxPlayer()
    {
        UISfx.clip = ButtonClip;
        UISfx.Play();
    }

    public void PauseMenu()
    {

        if(!Paused)
        {
            Time.timeScale = 0f;
            MenuPanel.SetActive(true);
            UIText.SetActive(false);
            Paused = true;
        }

        else if (Paused)
        {
            Time.timeScale = 1f;
            MenuPanel.SetActive(false);
            UIText.SetActive(true);
            Paused = false;
        }
    }


    public void P2Play()
    {
        if(Paused)
        {
            Time.timeScale = 1f;
            MenuPanel.SetActive(false);
            UIText.SetActive(true);
            Paused = false;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(restart);
    }
}
