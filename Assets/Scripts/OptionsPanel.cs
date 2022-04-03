using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    public ButtonSound butt;
    public bool mouseMode;
    public float mouseSense, wheelSense;
    public Slider mouseSlider, wheelSlider;
    public Toggle mouseToggle;
    public TMP_InputField mouseInput, wheelInput;
    void Start()
    {
        //TempFunction();
        SetUpPanel();
        if (!butt)
        {
            Debug.Log("we need butt here");
        }
    }
    //private void TempFunction()
    //{
    //    PlayerPrefs.SetInt("MouseMode", 0);
    //    PlayerPrefs.SetFloat("MouseSensitivity", 0.5f);
    //    PlayerPrefs.SetFloat("WheelSensitivity", 25);
    //}
    private void SetUpPanel()
    {
        mouseMode = PlayerPrefs.GetInt("MouseMode") == 1;
        wheelSense = PlayerPrefs.GetFloat("WheelSensitivity");
        mouseSense = PlayerPrefs.GetFloat("MouseSensitivity");
        Debug.Log("MouseSense = " + mouseSense + ", and wheelsense = " + wheelSense);
        SetUpMouse(mouseSense);
        SetUpWheel(wheelSense);
        SetUpToggle(mouseMode);
    }
    private void SetUpMouse(float val)
    {
        mouseSlider.value = val;
        mouseInput.text = val.ToString("F2");
    }
    private void SetUpWheel(float val)
    {
        wheelSlider.value = val;
        wheelInput.text = val.ToString("F2");
    }
    private void SetUpToggle(bool b)
    {
        mouseToggle.isOn = b;
    }

    public void WheelSliderChanged(System.Single f)
    {
        wheelSense = f;
        wheelInput.text = wheelSense.ToString("F2");
    }
    public void MouseSliderChanged(System.Single f)
    {
        mouseSense = f;
        mouseInput.text = mouseSense.ToString("F2");
    }
    public void WheelInputChanged(string st)
    {
        float val;
        if (float.TryParse(st, out val))
        {
            wheelSense = val;
            wheelSlider.value = val;
        }
    }
    public void MouseInputChanged(string st)
    {
        float val;
        if (float.TryParse(st, out val))
        {
            mouseSense = val;
            mouseSlider.value = val;
        }
    }
    public void ToggleChanged(bool b)
    {
        butt.OnButtonPress();
        mouseMode = b;
    }
    void Update()
    {
        
    }
    public void SaveStats()
    {
        Debug.Log("MouseSense = " + mouseSense + ", and wheelsense = " + wheelSense);
        if (mouseMode)
        {
            PlayerPrefs.SetInt("MouseMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MouseMode", 0);
        }
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSense);
        PlayerPrefs.SetFloat("WheelSensitivity", wheelSense);
    }
}
