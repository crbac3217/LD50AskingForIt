using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float shakeAmount, camShakeAmount;
    private Vector2 defaultPos;
    private Vector3 defaultCamPos;
    public GameManager gm;
    private bool hit, back;
    public GameObject cam, parent;
    private void Start()
    {
        hit = false;
        back = false;
        defaultPos = transform.localPosition;
        defaultCamPos = cam.transform.localPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sperm"))
        {
            hit = true;
            gm.Blocked();
        }
    }
    private void Update()
    {
        if (hit)
        {
            Vector2 dir = -parent.transform.up.normalized;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - shakeAmount);
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x + dir.x * camShakeAmount, cam.transform.localPosition.y + dir.y * camShakeAmount, -10);
            if (Mathf.Abs(defaultPos.y - transform.localPosition.y) > Mathf.Abs(shakeAmount)*12)
            {
                hit = false;
                back = true;
            }
        }
        else if (back)
        {
            if ((Vector2)transform.localPosition != defaultPos)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, defaultPos, Mathf.Abs(shakeAmount));
            }
            else
            {
                back = false;
            }
            if (cam.transform.localPosition != defaultCamPos)
            {
                cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, defaultCamPos, Mathf.Abs(camShakeAmount));
            }
        }
    }
}
