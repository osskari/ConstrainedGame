using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject backgroundPanel, startMenu, endScreen;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            ShowStartMenu(false);
            Restart();
        }
        if(Input.GetKey(KeyCode.R))
        {
            Restart();
        }
        if(Input.GetKey(KeyCode.B)) ShowEndScreen(true);
    }

    public void ShowStartMenu(bool show)
    {
        if(backgroundPanel.activeInHierarchy == show && startMenu.activeInHierarchy == show) return;
        backgroundPanel.SetActive(show);
        startMenu.SetActive(show);
        endScreen.SetActive(false);
        Time.timeScale = show ? 0f : 1f;
    }

    public void ShowEndScreen(bool show)
    {
        if(backgroundPanel.activeInHierarchy == show && endScreen.activeInHierarchy == show) return;
        backgroundPanel.SetActive(show);
        endScreen.SetActive(show);
        startMenu.SetActive(false);
        Time.timeScale = show ? 0f : 1f;
    }

    public void Restart()
    {
        if(endScreen.activeInHierarchy)
        {
            ShowEndScreen(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
