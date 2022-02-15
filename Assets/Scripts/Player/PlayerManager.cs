using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

    private GameObject startButton;
    private GameObject quitButton;
    private Inventory inventory;
    [SerializeField] private GameObject inventoryParent;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.FindGameObjectWithTag("StartGameButton");
        quitButton = GameObject.FindGameObjectWithTag("QuitGameButton");
    }

    private void Awake()
    {
        List<GameObject> objects = FindGameObjectInChildWithTag(inventoryParent, "InventoryItemSlot");

        inventory = new Inventory(objects);
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

    public static List<GameObject> FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                objects.Add(t.GetChild(i).gameObject);
            }
        }

        return objects;
    }
}