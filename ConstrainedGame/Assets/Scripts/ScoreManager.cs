using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public Rigidbody2D body;
    private int score;
    private int activeScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI activeScoreText;
    public TextMeshProUGUI comboText;
    public SkidmarkManager skids;
    private float driftThreshold = 0.7f;
    private int comboMultiplier = 1;
    //Is the player currently above the drift threshold
    private bool isDrifting = false;
    //Which 'direction' the drift is
    private int previousDriftSign = 0;
    //time between changin drift direction
    private float timeBetweenDrifts = 100f;
    //How much time is allowed to pass before loosing combo between drifts
    private float timeBetweenDriftThreshold = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(comboMultiplier);
        timeBetweenDrifts += Time.fixedDeltaTime;
        Vector2 perpendicular = Quaternion.AngleAxis(body.angularVelocity > 0 ? -90 : 90, Vector3.forward) * new Vector2(0.0f, 0.5f);
        float counterNormal = Vector2.Dot(body.velocity.normalized, body.GetRelativeVector(perpendicular.normalized));

        if (isDrifting)
        {
            skids.BeginDraw();
        } else
        {
            skids.EndDraw();
        }
        //If player crashes, reset combo and active points

        //Moment  the player falls below drift threshold
        if(isDrifting && (Mathf.Abs(counterNormal) < driftThreshold))
        {
            //Set variable depending on drift direction
            previousDriftSign = counterNormal > 0 ? 1 : -1;
            //Reset timer
            timeBetweenDrifts = 0;
        }

        //Moment player starts drifting
        if(!isDrifting && (Mathf.Abs(counterNormal) > driftThreshold))
        {
            //Check if player is driftin in another direction then before and is within x time
            if ((previousDriftSign * counterNormal) < 0 && timeBetweenDrifts < timeBetweenDriftThreshold)
            {
                comboMultiplier += 1;
            }
            else
            {
                //Reset combo multiplier
                comboMultiplier = 1;
            }

            //Set combo text and drift sign variable
            comboText.text = "x" + comboMultiplier.ToString();
            previousDriftSign = counterNormal > 0 ? 1 : -1;
        }

        //If timeBetweenDrifts exceeds X time , add active score and reset active score/multipliers 

        AddToActiveScore(counterNormal);

    }

    void AddToActiveScore(float counterNormal)
    {
        if (Mathf.Abs(counterNormal) > driftThreshold)
        {
            isDrifting = true;
            activeScore += Mathf.Abs(Mathf.RoundToInt((((body.velocity.magnitude * 0.5f) * (counterNormal * 10f)) * 0.1f)) * comboMultiplier);
            activeScoreText.text = activeScore.ToString();
        }
        else
        {
            isDrifting = false;
        }
    }
}
