using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopScroller : MonoBehaviour
{
    public float uvChangeAmount = 0.0001f;
    public GameObject player;
    [SerializeField] private RawImage _img;
    private Vector2 uvRectPosition;
    private Vector3 lastPosition;
    void Update()
    {
        float deltaY = player.transform.position.y - lastPosition.y;
        if (Mathf.Abs(deltaY) > Mathf.Epsilon)
        {
            uvRectPosition = _img.uvRect.position;
            uvRectPosition.y += deltaY > 0 ? uvChangeAmount : -uvChangeAmount;
            _img.uvRect = new Rect(uvRectPosition, _img.uvRect.size);
            lastPosition = player.transform.position;
        }


    }


}

