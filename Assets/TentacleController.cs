using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    public Vector2 force;
    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = FindObjectOfType<PlayerController>();
        //FaceCenter();

    }

    void FixedUpdate()
    {
        if (_spriteRenderer.flipX) transform.position = new Vector2(-11.87f, transform.position.y);
        else
        {
            transform.position = new Vector2(11.75f, transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController.ApplyForce(force);
        }
    }

    private bool IsOffCamera()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
    }

    public void FaceCenter()
    {
        _spriteRenderer.flipX = transform.position.x >= _playerController.gameObject.transform.position.x ? true : false;
    }
}
