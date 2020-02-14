using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapWatcher : MonoBehaviour
{
    private int p1Laps, p2Laps;
    public GameObject score1, score2, ui;
    
    void Start()
    {
        p1Laps = 0;
        p2Laps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        p1Laps = score1.GetComponent<ScoreManager>().laps;
        p2Laps = score2.GetComponent<ScoreManager>().laps;

        if(p1Laps == 3 && p2Laps == 3)
        {
            UIController uic = ui.GetComponent<UIController>();

            uic.FindWinner();
            uic.ShowEndScreen(true);
        }
    }
}
