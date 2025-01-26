using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public AudioClip openShop;
    public List<AudioClip> buyAudios;

    public TMP_Text playerMoneyText;
    private GameManager gameManager;
    private float playerMoney;
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        playerMoney = gameManager.GetPlayerMoney();
        playerMoneyText.text = "" + playerMoney;
        audioManager.PlaySFX(openShop);

    }

    void Buy()
    {
        audioManager.PlaySFX(buyAudios[Random.Range(0, buyAudios.Count)]);
    }

}
