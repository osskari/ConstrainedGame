using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidmarkManager : MonoBehaviour
{
    public List<TrailRenderer> wheels;

    public void BeginDraw()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            wheels[i].emitting = true;
        }
    }

    public void EndDraw()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            wheels[i].emitting = false;
        }
    }
    
}
