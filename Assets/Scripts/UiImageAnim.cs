using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiImageAnim : MonoBehaviour
{
    public Sprite[] anim = new Sprite[] { };
    public bool begin = false;
    private Image image;
    private int animIndex;
    // Start is called before the first frame update
    void Start()
    {
        begin = false;
        animIndex = 0;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animIndex <= anim.Length - 1 && begin)
        {
            image.sprite = anim[animIndex];
            animIndex++;
        }
    }
}
