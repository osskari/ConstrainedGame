using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Countdown : MonoBehaviour
{
    public GameObject lights;
    public Color32 offColor, onColor;
    public float countdownDuration;
    public AudioSource beepCount, beepStart;
    private float currentTime, startTime, lightInterval;
    private bool isRunning;
    private int lastAmount;
    private List<Image> lightList;
    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;
        startTime = Time.unscaledTime;
        lightList = new List<Image>();
        lightInterval = countdownDuration/7;
        lastAmount = 0;
        foreach (Transform child in lights.transform)
        {
            foreach (Transform subchild in child.transform)
            {
                lightList.Add(subchild.gameObject.GetComponent<Image>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentTime = Time.unscaledTime - startTime;  
            if(currentTime > countdownDuration)
            {
                DisplayTimeGraphic(false);
                TimerDone();
            }
            else
            {
                DisplayTimeGraphic(true);
            }
        }
    }

    public void StartTimer()
    {
        startTime = Time.unscaledTime;
        currentTime = 0;
        isRunning = true;
    }

    private void TimerDone()
    {
        isRunning = false;
        Time.timeScale = 1f;
        lastAmount = 0;
        beepStart.Play();
    }

    private void DisplayTimeGraphic(bool show)
    {
        if(show)
        {
            if(!lights.activeInHierarchy)
            {
                lights.SetActive(true);
            }

            var onLights = (int)(currentTime/lightInterval);

            if(onLights == 6)
            {
                currentTime = countdownDuration + 1;
            }
            else
            {
                TurnOnLights(onLights);
            }
        }
        else if(lights.activeInHierarchy)
        {
            TurnOnLights(0);
            lights.SetActive(false);
        }
    }

    private void TurnOnLights(int amount)
    {
        if(amount > 0 && amount < 6 && amount > lastAmount)
        {
            lastAmount = amount;
            beepCount.Play();
        }
        for(var i = 0; i < lightList.Count; i++)
        {
            lightList[i].color = i >= amount ? offColor : onColor;
        }
    }
    
}
