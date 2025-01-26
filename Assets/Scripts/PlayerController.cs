using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public TMP_Text heightText;
    public TMP_Text velocityText;
    private Rigidbody2D _rb2D;
    private SpriteRenderer spriteRenderer;
    private bool hasLaunched;
    private float maxVelocityReached;

    private bool reachedLowVelocity = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdateHeight());
        StartCoroutine(UpdateVelocity());
        hasLaunched = false;
        _rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        maxVelocityReached = 0f;
    }

    void Update()
    {
        if (_rb2D.velocity.magnitude > maxVelocityReached) maxVelocityReached = _rb2D.velocity.magnitude;

        if (!reachedLowVelocity && _rb2D.velocity.y >= 0.02f && _rb2D.velocity.y <= 0.1f) StartCoroutine(DisableGravityFor(1f));
        if (reachedLowVelocity && (_rb2D.velocity.x > 0.1 || _rb2D.velocity.y > 0.1)) reachedLowVelocity = false;
        if (_rb2D.velocity.x < 0.0001 && _rb2D.velocity.x > 0 && _rb2D.velocity.y < 0.0001 & _rb2D.velocity.y > 0) _rb2D.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {

        if (hasLaunched && _rb2D.velocity.y <= 0 && _rb2D.velocity.x <= 0)
        {
            _rb2D.velocity = Vector2.zero;
            _rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            FindObjectOfType<GameManager>().End(LimitDecimals(maxVelocityReached, 0), LimitDecimals(_rb2D.position.y, 1));
            Debug.Log("Fin de juego");
            this.enabled = false;
        }


    }


    public IEnumerator DisableGravityFor(float seconds)
    {
        reachedLowVelocity = true;
        float oldGravityValue = _rb2D.gravityScale;
        _rb2D.gravityScale = 0;
        yield return new WaitForSeconds(seconds);
        _rb2D.gravityScale = oldGravityValue;
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
        velocityText.text = "" + LimitDecimals(_rb2D.velocity.magnitude, 1) + " m/s"; // Actualiza el texto con 1 decimal
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
        _rb2D.constraints = RigidbodyConstraints2D.None;
    }

    public void ApplyForce(Vector2 force)
    {
        _rb2D.AddForce(force, ForceMode2D.Impulse);
        hasLaunched = true;
    }
}
