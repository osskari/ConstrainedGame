using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidmarkManager : MonoBehaviour
{
    public List<TrailRenderer> wheels;

    void BeginDraw()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            wheels[i].emitting = true;
        }
    }

    void EndDraw()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            wheels[i].emitting = false;
        }
    }
    
}
