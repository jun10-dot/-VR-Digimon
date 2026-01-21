using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BtnCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject leftRay;
    [SerializeField]
    private GameObject rightRay;
    [SerializeField]
    private GameObject leftDirect;
    [SerializeField]
    private GameObject rightDirect;

    public InputActionProperty lTrigger;
    public InputActionProperty rTrigger;

    private bool lIsOn;
    private bool rIsOn;

    private float timer;
    private float delay;

    void Start()
    {
        lIsOn = false;
        rIsOn = false;
        timer = 0.0f;
        delay = 1.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (lTrigger.action.ReadValue<float>() > 0.1f && !lIsOn)
            {
                leftDirect.SetActive(false);
                leftRay.SetActive(true);
                lIsOn = true;
                timer = 0.0f;
            }
            else if (lTrigger.action.ReadValue<float>() > 0.1f && lIsOn)
            {
                leftRay.SetActive(false);
                leftDirect.SetActive(true);
                lIsOn = false;
                timer = 0.0f;
            }

            // if (rTrigger.action.ReadValue<float>() > 0.1f && !rIsOn)
            // {
            //     rightDirect.SetActive(false);
            //     rightRay.SetActive(true);
            //     rIsOn = true;
            //     timer = 0.0f;
            // }
            // else if (rTrigger.action.ReadValue<float>() > 0.1f && rIsOn)
            // {
            //     rightRay.SetActive(false);
            //     rightDirect.SetActive(true);
            //     rIsOn = false;
            //     timer = 0.0f;
            // }
        }
    }
}
