using UnityEngine;
using TMPro;
using System.Collections;

public class BubbleController : MonoBehaviour
{
    public float initialForce = 100f;

    public TMP_Text heightText;
    public TMP_Text velocityText;
    private Rigidbody2D _rb2D;
    private float currentValue = 0f;
    private float targetValue = 0f;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdateHeight());
        StartCoroutine(UpdateVelocity());
        _rb2D.AddForce(Vector2.up * initialForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (_rb2D.velocity.y <= 0)
        {
            Time.timeScale = 0;
            velocityText.text = "" + 0;
            Debug.Log("Fin de juego");
        }
    }


    public float LimitDecimals(float value, int decimals)
    {
        float factor = Mathf.Pow(10, decimals);
        return Mathf.Round(value * factor) / factor;
    }

    private IEnumerator UpdateHeight()
    {
        heightText.text = "" + LimitDecimals(_rb2D.position.y, 1) + " m "; // Actualiza el texto con 1 decimal
        yield return new WaitForSeconds(0f); // Espera un frame antes de continuar
        StartCoroutine(UpdateHeight());
    }
    private IEnumerator UpdateVelocity()
    {
        velocityText.text = "" + LimitDecimals(_rb2D.velocity.y, 1) + " m/s"; // Actualiza el texto con 1 decimal
        yield return new WaitForSeconds(0f); // Espera un frame antes de continuar
        StartCoroutine(UpdateVelocity());
    }

    public void SoftDisable()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        this.enabled = false;
    }

    public void SoftEnable()
    {
        spriteRenderer.enabled = true;
        this.enabled = true;
    }
}
