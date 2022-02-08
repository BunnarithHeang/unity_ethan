using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject startButton;
    private GameObject quitButton;

    public void Start()
    {
        startButton = GameObject.FindGameObjectWithTag("StartGameButton");
        quitButton = GameObject.FindGameObjectWithTag("QuitGameButton");
    }

    public void SetClick()
    {
        startButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
