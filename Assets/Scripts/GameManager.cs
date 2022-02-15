using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    private GameObject startButton;
    private GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.FindGameObjectWithTag("StartGameButton");
        quitButton = GameObject.FindGameObjectWithTag("QuitGameButton");
    }

    // Update is called once per frame
    void Update()
    {
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