using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject backgroundPanel, startMenu, endScreen, score1, score2;
    public Countdown timer;
    private int p1Score, p2Score;
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
        if (endScreen.activeInHierarchy || Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SetScores(int playerNumber, int pScore, int pBonus)
    {
        if(playerNumber == 1)
        {
            p1Score = pScore + pBonus;
            TextMeshProUGUI[] p1Text = score1.GetComponentsInChildren<TextMeshProUGUI>(true);
            p1Text[1].text = "Score: " + pScore;
            p1Text[2].text = "Time Bonus: " + pBonus;
            p1Text[3].text = "Final Score: " + p1Score;
        }
        else if(playerNumber == 2)
        {
            p2Score = pScore + pBonus;
            TextMeshProUGUI[] p2Text = score2.GetComponentsInChildren<TextMeshProUGUI>(true);
            p2Text[1].text = "Score: " + pScore;
            p2Text[2].text = "Time Bonus: " + pBonus;
            p2Text[3].text = "Final Score: " + p2Score;
        }
    }

    public void FindWinner()
    {
        endScreen.GetComponentsInChildren<TextMeshProUGUI>(true)[0].text = p1Score > p2Score ? "Player 1 wins!" : p2Score > p1Score ? "Player 2 wins!" : "It's a draw!";
    }
}
