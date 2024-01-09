using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action GameStartedEvent;
    public static event Action NextLevelButtonTappedEvent;
    public static event Action ShowSuccessMenuEvent;

    public GameObject StartMenuPanel;
    public GameObject SuccessPanel;

    private PlayerController playerController;

    private void Start()
    {
        StartMenuPanel.SetActive(true);
        playerController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
    }

    public void StartButtonTapped()
    {
        StartMenuPanel.SetActive(false);

        playerController?.GameStarted();
        GameStartedEvent?.Invoke();
    }

    public void NextLevelButtonTapped()
    {
        SuccessPanel.SetActive(false);
        NextLevelButtonTappedEvent?.Invoke();
    }

    public void ShowSuccessMenu()
    {
        SuccessPanel.SetActive(true);
        ShowSuccessMenuEvent?.Invoke();
    }
}