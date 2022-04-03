using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarParent : MonoBehaviour
{
    public bool mouseMode;
    public float wheelSensitivity, mouseSensitivity;
    public GameObject ActualBar;

    private void Start()
    {
        SetUpStat();
    }
    public void SetUpStat()
    {
        mouseMode = PlayerPrefs.GetInt("MouseMode") > 0;
        wheelSensitivity = PlayerPrefs.GetFloat("WheelSensitivity");
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
    }
    void Update()
    {
        if (mouseMode)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + Input.mousePosition.x * mouseSensitivity);
        }
        else
        {
            transform.Rotate(Vector3.forward * wheelSensitivity * Input.mouseScrollDelta.y);
        }
    }
}
