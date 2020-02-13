using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject backgroundPanel, startMenu, endScreen;
    public Countdown timer;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
            ShowStartMenu(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        // TODO Remove later
        if (Input.GetKey(KeyCode.B)) ShowEndScreen(true);
    }

    public void ShowStartMenu(bool show)
    {
        if (backgroundPanel.activeInHierarchy == show && startMenu.activeInHierarchy == show) return;
        backgroundPanel.SetActive(show);
        startMenu.SetActive(show);
        endScreen.SetActive(false);
        if(show)
        {
            Time.timeScale = 0f;
        }
        else
        {
            timer.StartTimer();
        }
    }

    public void ShowEndScreen(bool show)
    {
        if (backgroundPanel.activeInHierarchy == show && endScreen.activeInHierarchy == show) return;
        backgroundPanel.SetActive(show);
        endScreen.SetActive(show);
        startMenu.SetActive(false);
        Time.timeScale = show ? 0f : 1f;
    }

    public void Restart()
    {
        if (endScreen.activeInHierarchy)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void WinningPlayer(string playerNumber)
    {
        endScreen.GetComponentsInChildren<TextMeshProUGUI>(true)[2].text = endScreen.GetComponentsInChildren<TextMeshProUGUI>(true)[2].text.Replace("X", playerNumber);
    }
}
