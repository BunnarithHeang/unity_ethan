using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;

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