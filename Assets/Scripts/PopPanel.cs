using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopPanel : MonoBehaviour
{
    public float speed = 0.04f;
    public bool expand, retract;
    public Image image;
    private void Start()
    {
        expand = true;
        retract = false;
        image = GetComponent<Image>();
        image.rectTransform.localScale = new Vector2(0, image.rectTransform.localScale.y);
    }
    private void Update()
    {
        if (expand)
        {
            Expand();
        }
        if (retract)
        {
            Retract();
        }
    }
    public virtual void Expand()
    {
        image.rectTransform.localScale = new Vector2(image.rectTransform.localScale.x + speed, image.rectTransform.localScale.y);
        if (image.rectTransform.localScale.x >= 0.9)
        {
            retract = true;
            expand = false;
        }
    }
    public virtual void Retract()
    {
        image.rectTransform.localScale = new Vector2(image.rectTransform.localScale.x - speed, image.rectTransform.localScale.y);
        if (image.rectTransform.localScale.x <= 0.8f)
        {
            image.rectTransform.localScale = new Vector2(0.8f, 0.8f);
            retract = false;
        }
    }
}
