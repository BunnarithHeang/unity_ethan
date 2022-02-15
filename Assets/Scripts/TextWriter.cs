using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI textUI;
    private string title = "Ethan The Explorer";
    private int index = 1;

    public AudioSource longSound;
    public AudioSource shortSound;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        textUI = GetComponent<TMPro.TextMeshProUGUI>();

        if (textUI != null)
        {
            textUI.SetText("");
        }

        //gameManager = GameObject.FindGameObjectWithTag("GameManager") as GameManager;

        StartCoroutine("WriteText");
    }

    private IEnumerator WriteText()
    {
        while (index < title.Length)
        {
            Play();
            textUI.SetText(title.Substring(0, index));

            index++;

            float delay = 0.1f;

            if (title.Substring(0, index).EndsWith(" ")) {
                delay = 0.15f;
            }

            yield return new WaitForSeconds(delay);
        }

        gameManager.SetClick();
    }

    private void Play()
    {
        string text = textUI.text;

        if (text == "")
        {
            longSound.Play();
        }
        else if (text.EndsWith("Ethan ") || text.EndsWith("The "))
        {
            shortSound.Play();
        }
    }
}
