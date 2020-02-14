using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Rigidbody2D body;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI activeScoreText;
    public TextMeshProUGUI comboText;
    public Image indicator;
    //Driving under this times give the player a time bonus
    private float parTime = 100f;
    //Exceeding this time give the player a penalty
    private float penaltyTime = 120f;
    public AudioSource tyreSound, wallHit;
    public GameObject UISystem;
    public int playerNum;

    private int stage;
    [HideInInspector]
    public int laps;
    private float lapTimer;
    private bool startLapTimer = false;

    private int score = 0;
    private int activeScore = 0;
    private int timeBonus = 0;
    private int comboMultiplier = 1;
    public SkidmarkManager skids;
    private float driftThreshold = 0.7f;
    //Is the player currently above the drift threshold
    private bool isDrifting = false;
    //Which 'direction' the drift is
    private int previousDriftSign = 0;
    //time between changin drift direction
    private float timeBetweenDrifts = 100f;
    //time since last drift
    private float timeSinceLastDrift = 100;
    //How much time is allowed to pass before loosing combo between drifts
    private float timeBetweenDriftThreshold = 2f;
    private float gainActiveScoreThreshold = 2.2f;
    private bool indicatorBool = true;

    private Coroutine activeCoroutineScoreFlash;

    // Start is called before the first frame update
    void Start()
    {
        SetComboText();
        SetScoreText();
        SetActiveScoreText();
        activeCoroutineScoreFlash = null;
        stage = 0;
        laps = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!wallHit.isPlaying)
        {
            wallHit.Play();
        }
        if (collision.collider.CompareTag("TrackCollider"))
        {
            if (activeCoroutineScoreFlash == null)
            {
                indicator.fillAmount = 0;
                indicatorBool = false;
                activeCoroutineScoreFlash = StartCoroutine("ActiveScoreCollisionFlash", 0.3f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawSkidMarks();
        if (startLapTimer)
        {
            lapTimer += Time.deltaTime;
            if (!isDrifting)
            {
                tyreSound.Stop();
            }
            else if (isDrifting && !tyreSound.isPlaying)
            {
                tyreSound.Play();
            }
        }

        //Lerp milli timeSinceLastDrift og gainActiveScorethreshold
        if(indicatorBool)
        {
            indicator.fillAmount = Mathf.Lerp(1f, 0f, timeSinceLastDrift / gainActiveScoreThreshold);
        }

    }

    private void FixedUpdate()
    {

        timeBetweenDrifts += Time.fixedDeltaTime;
        timeSinceLastDrift += Time.fixedDeltaTime;
        Vector2 perpendicular = Quaternion.AngleAxis(body.angularVelocity > 0 ? -90 : 90, Vector3.forward) * new Vector2(0.0f, 0.5f);
        float counterNormal = Vector2.Dot(body.velocity.normalized, body.GetRelativeVector(perpendicular.normalized));


        //If player crashes, reset combo and active points

        //Moment  the player falls below drift threshold
        if (isDrifting && (Mathf.Abs(counterNormal) < driftThreshold))
        {
            //Set variable depending on drift direction
            previousDriftSign = counterNormal > 0 ? 1 : -1;
            //Reset timer
            timeBetweenDrifts = 0;
            timeSinceLastDrift = 0;
        }

        //Moment player starts drifting
        if (!isDrifting && (Mathf.Abs(counterNormal) > driftThreshold))
        {
            indicatorBool = true;
            //Check if player is drifting in another direction then before and is within x time
            if ((previousDriftSign * counterNormal) < 0 && timeBetweenDrifts < timeBetweenDriftThreshold)
            {
                comboMultiplier += 1;
            }

            //Set combo text and drift sign variable
            SetComboText();
            previousDriftSign = counterNormal > 0 ? 1 : -1;
        }

        //If timeBetweenDrifts exceeds X time , add active score and reset active score/multipliers
        if (timeSinceLastDrift > gainActiveScoreThreshold)
        {
            //Add to overall score and reset current and multiplier
            score += activeScore;
            activeScore = 0;
            comboMultiplier = 1;

            SetScoreText();
            SetActiveScoreText();
            SetComboText();
        }
        AddToActiveScore(counterNormal);
    }

    void AddToActiveScore(float counterNormal)
    {
        if (Mathf.Abs(counterNormal) > driftThreshold)
        {
            timeSinceLastDrift = 0;
            isDrifting = true;
            activeScore += Mathf.Abs(Mathf.RoundToInt((((body.velocity.magnitude * 0.5f) * (counterNormal * 10f)) * 0.1f)) * comboMultiplier);
            SetActiveScoreText();
        }
        else
        {
            isDrifting = false;
        }
    }

    void DrawSkidMarks()
    {
        if (isDrifting)
        {
            skids.BeginDraw();
        }
        else
        {
            skids.EndDraw();
        }
    }

    void SetComboText()
    {
        comboText.text = "x" + comboMultiplier.ToString();
        if (comboMultiplier == 1)
        {
            comboText.color = new Color32(255, 255, 255, 255);
            comboText.fontSize = 24;
        }
        else if (comboMultiplier == 2)
        {
            comboText.color = new Color32(0, 112, 221, 255);
            comboText.fontSize = 28;
        }
        else if (comboMultiplier == 3)
        {
            comboText.color = new Color32(150, 15, 238, 255);
            comboText.fontSize = 32;
        }
        else
        {
            comboText.color = new Color32(255, 128, 0, 255);
            comboText.fontSize = 36;
        }
    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    void SetActiveScoreText()
    {
        if (activeCoroutineScoreFlash == null)
        {
            activeScoreText.text = activeScore.ToString();
            activeScoreText.color = new Color(255, 255, 255);
        }
    }

    IEnumerator ActiveScoreCollisionFlash(float delay)
    {
        if (activeScore != 0)
        {
            activeScoreText.color = new Color(255, 0, 0);
        }

        yield return new WaitForSeconds(delay);
        comboMultiplier = 1;
        activeScore = 0;
        SetComboText();
        SetActiveScoreText();
        activeCoroutineScoreFlash = null;
    }

    void SetTimeBonusPoints()
    {
        float timeDifferencePar = parTime - lapTimer;
        float timeDifferencePenalty = penaltyTime - lapTimer;
        if (timeDifferencePar >= 0)
        {
            timeBonus = 4000 + (int)timeDifferencePar * 200;
        }
        else if (timeDifferencePenalty >= 0)
        {
            timeBonus = 0;
        }
        else
        {
            timeBonus = -4000 + (int)timeDifferencePenalty * 200;
        }
    }

    public void enterStage(int stage)
    {
        if (this.stage == 0 && laps == 0)
        {
            startLapTimer = true;
        }

        if (stage == 0 && this.stage == 3)
        {
            laps++;
            this.stage = 0;
        } else if (stage - 1 == this.stage)
        {
            this.stage = stage;
        }

        if (laps == 3)
        {
            startLapTimer = false;
            CarMovement m = GetComponent<CarMovement>();
            CarMovement1 m1 = GetComponent<CarMovement1>();
            if (m) m.enabled = false;
            if (m1) m1.enabled = false;
            SetTimeBonusPoints();
            UIController uic = UISystem.GetComponent<UIController>();
            score += activeScore;
            activeScore = 0;
            uic.SetScores(playerNum, score, timeBonus);
        }

    }
 }