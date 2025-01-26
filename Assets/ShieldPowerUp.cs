using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public AudioClip beGrabbed;
    public float durationInSeconds = 5f;
    public Sprite playerWithShield;
    private SpriteRenderer spriteRenderer;
    private Collider2D coll;
    private PlayerController playerController;
    private AudioManager audioManager;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShieldPlayer());
            audioManager.PlaySFX(beGrabbed);
            spriteRenderer.enabled = false;
            coll.enabled = false;
        }
    }

    IEnumerator ShieldPlayer()
    {
        playerController.DisableCollider();
        playerController.ChangeTo(Color.yellow);
        yield return new WaitForSeconds(1f);
        playerController.EnableCollider();
        playerController.ChangeTo(Color.white);

    }
}
