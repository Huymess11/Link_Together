using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunnedEffect : Singleton<StunnedEffect>
{
    Image stunnedImage;

    private void Start()
    {
        stunnedImage = GetComponent<Image>();   
    }
    public void Effect()
    {
        StartCoroutine(PlayEffect());
    }
    IEnumerator PlayEffect()
    {
        stunnedImage.DOFade(1,0.1f);
        yield return new WaitForSeconds(0.3f);
        stunnedImage.DOFade(0, 0.1f);
    }
}
