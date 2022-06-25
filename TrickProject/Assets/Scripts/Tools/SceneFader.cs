using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public float fadeInDuration=1f;
    public float fadeOutDuration=2f;

    private Action<SceneFader> inBack;
    private Action<SceneFader> outBack;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

		DontDestroyOnLoad(gameObject);
	}

    public SceneFader SetColor(Color color)
	{
        GetComponentInChildren<Image>().color = color;
        return this;
    }

    ///// <summary>
    ///// 动画渐出渐进
    ///// </summary>
    ///// <returns></returns>
    //public IEnumerator FadeOutIn()
    //{
    //    yield return StartCoroutine(FadeOut(fadeOutDuration));
    //    yield return StartCoroutine(FadeIn(fadeInDuration));
    //}

    public void CanvasFadeIn()
    {
        //StartCoroutine(FadeIn(1));
        canvasGroup.alpha = 0;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, fadeInDuration).OnComplete(() => {
            inBack?.Invoke(this);
        });
    }
    public void CanvasFadeOut()
    {
        //StartCoroutine(FadeOut(3));
        canvasGroup.alpha = 1;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, fadeOutDuration).SetEase(Ease.InQuint).OnComplete(() => {
            outBack?.Invoke(this);
            Destroy(gameObject);
        });
    }

    /// <summary>
    /// 动画渐出
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float time)
    {
        //canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
        outBack?.Invoke(this);
        Destroy(gameObject);
	}

	/// <summary>
	/// 动画渐进
	/// </summary>
	/// <returns></returns>
	public IEnumerator FadeIn(float time)
    {
		canvasGroup.alpha = 0;
		while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
        inBack?.Invoke(this);
    }
    public SceneFader SetInCallback(Action<SceneFader> action)
	{
        inBack+= action;
        return this;
	}

    public SceneFader SetOutCallback(Action<SceneFader> action)
    {
        outBack+= action;
        return this;
    }

    public void SetNoIn()
	{
        canvasGroup.alpha = 1;
    }
}
