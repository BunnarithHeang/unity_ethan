using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private Button startOverButton;

    public void SetClick()
    {
        startButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void Awake()
    {
        if (startOverButton != null)
        {
            startOverButton.onClick.AddListener(StartOver);
        }       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartOver()
    {
        SceneManager.LoadScene("Intro");
    }
}