using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    [SerializeField]
    int triggerScore = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager sm = collision.GetComponentInParent<ScoreManager>();
        if (sm)
        {
            sm.enterStage(triggerScore); 
        }
    }
}
