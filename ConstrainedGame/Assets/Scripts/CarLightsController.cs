using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightsController : MonoBehaviour
{
    public GameObject leftBrake, rightBrake;
    public KeyCode key, altKey;
    private Light lbLight, rbLight;
    // Start is called before the first frame update
    void Start()
    {
        lbLight = leftBrake.GetComponent<Light>();
        rbLight = rightBrake.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(key) || Input.GetKey(altKey))
        {
            lbLight.intensity = 4f;
            rbLight.intensity = 4f;
        }
        else
        {
            lbLight.intensity = 10f;
            rbLight.intensity = 10f;
        }
    }
}
