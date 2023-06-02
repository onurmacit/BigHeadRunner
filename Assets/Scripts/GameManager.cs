using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject StartMenuPanel;
    public GameObject SucessPanel;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonTapped()
    {
        StartMenuPanel.gameObject.SetActive(false);
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerScript = playerGO.GetComponent<PlayerController>();
        playerScript.GameStarted();
    }

    public void NextLevelButtonTapped()
    {
        SucessPanel.gameObject.SetActive(false);
        LevelController.instance.NextLevel();
    }

    public void ShowSucessMenu()
    {
        SucessPanel.gameObject.SetActive(true);
    }

}
