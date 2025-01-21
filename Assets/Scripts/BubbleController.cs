using UnityEngine;
using TMPro;

public class BubbleController : MonoBehaviour
{

    public TMP_Text heightText;
    public TMP_Text velocityText;
    private Rigidbody2D _rb2D;



    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        heightText.text = "" + _rb2D.position.y;
        velocityText.text = "" + _rb2D.velocity;

    }

}
