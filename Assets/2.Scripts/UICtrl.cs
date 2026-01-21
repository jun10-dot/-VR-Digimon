using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UICtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject hp;
    [SerializeField]
    private GameObject hunger;
    [SerializeField]
    private GameObject affinity;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject sound;


    private bool HUDisOn;

    public InputActionProperty yButton;
    public InputActionProperty bButton;

    private float timer;
    private float delay;

    void Start()
    {
        HUDisOn = false;
        timer = 0.0f;
        delay = 1.0f;

        hp.SetActive(false);
        hunger.SetActive(false);
        affinity.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (yButton.action.ReadValue<float>() > 0.1f && !HUDisOn)
            {
                hp.SetActive(true);
                hunger.SetActive(true);
                affinity.SetActive(true);
                HUDisOn = true;
                timer = 0.0f;
            }
            else if (yButton.action.ReadValue<float>() > 0.1f && HUDisOn)
            {
                hp.SetActive(false);
                hunger.SetActive(false);
                affinity.SetActive(false);
                HUDisOn = false;
                timer = 0.0f;
            }

            // 사운드 창
            if (bButton.action.ReadValue<float>() > 0.1f && menu.activeSelf == false)
            {
                menu.SetActive(true);
                mainMenu.SetActive(true);
                sound.SetActive(false);
                timer = 0.0f;
            }
            else if (bButton.action.ReadValue<float>() > 0.1f && menu.activeSelf == true)
            {
                menu.SetActive(false);
                mainMenu.SetActive(false);
                sound.SetActive(false);
                timer = 0.0f;
            }
        }
    
        
    }
}
