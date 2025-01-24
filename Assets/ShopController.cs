using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public TMP_Text playerMoneyText;
    private GameManager gameManager;
    private float playerMoney;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMoney = gameManager.GetPlayerMoney();
        playerMoneyText.text = "" + playerMoney;
    }

}
