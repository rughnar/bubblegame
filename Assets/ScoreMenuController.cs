using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMenuController : MonoBehaviour
{
    public GameObject scoreMenu;
    public TMP_Text maxHeightText;
    public TMP_Text maxVelocityText;
    public TMP_Text scoreText;
    public void SetMaxValues(float maxHeight, float maxVelocity, float score)
    {
        maxHeightText.text = "" + maxHeight;
        maxVelocityText.text = "" + maxVelocity;
        scoreText.text = "" + score;
    }

    public void SoftActive()
    {
        scoreMenu.SetActive(true);
    }
    public void SoftDisable()
    {
        scoreMenu.SetActive(false);

    }
}
