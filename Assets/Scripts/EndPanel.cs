using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : PopPanel
{
    public UiImageAnim anim;

    public override void Retract()
    {
        image.rectTransform.localScale = new Vector2(image.rectTransform.localScale.x - speed, image.rectTransform.localScale.y);
        if (image.rectTransform.localScale.x <= 0.8f)
        {
            image.rectTransform.localScale = new Vector2(0.8f, 0.8f);
            retract = false;
            StartCoroutine(AnimBegin());
        }
    }
    private IEnumerator AnimBegin()
    {
        yield return new WaitForSeconds(0.5f);
        anim.begin = true;
    }
}
