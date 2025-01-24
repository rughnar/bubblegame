using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;

public class VelocityBarController : MonoBehaviour
{

    public float stepChangeValue = 0.1f;
    public float maxFillAmount = 1f;
    public float timeToWait = 0.1f;
    private Image image;
    private int inverter = 1;

    void Awake()
    {
        image = GetComponent<Image>();
        StartCoroutine(ChangeFillAmount());
    }

    IEnumerator ChangeFillAmount()
    {
        yield return new WaitForSeconds(timeToWait);
        if (image.fillAmount >= maxFillAmount || image.fillAmount <= 0)
        {
            inverter *= -1;
            image.fillAmount = image.fillAmount + 0.001f * inverter;

        }
        else image.fillAmount = Mathf.Clamp(image.fillAmount + stepChangeValue * inverter, 0, maxFillAmount);
        StartCoroutine(ChangeFillAmount());
    }

    public float GetValue()
    {
        return image.fillAmount;
    }

}
