using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class EnemyController : MonoBehaviour
{
    public float attackPoints = 10;
    public float velocity = 1f;
    public float hp;
    public AudioClip destroy;
    private Rigidbody2D _rb;
    private AudioManager _audioManager;
    //private EnemyManager _enemyManager;
    private SpriteRenderer _spriteRenderer;
    private int flipX;
    
    private GameObject _player;
    private bool moveNormally = true;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
        //_enemyManager = FindObjectOfType<EnemyManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    void FixedUpdate()
    {
        if (moveNormally)
        {
            flipX = _spriteRenderer.flipX ? -1 : 1;
            _rb.MovePosition(new Vector2(_rb.position.x + velocity * 0.01f * flipX, _rb.position.y));
        }
    }

    public void ReduceHealth(float hp)
    {
        this.hp -= hp;
        if (this.hp <= 0)
        {
            //audioManager.PlaySFX(destroy);
            //_enemyManager.EnemyTakenDown();

            Destroy(gameObject, 0.05f);
        }
        else
        {
            StartCoroutine(GoFromColorToColorIn(0.2f, Color.red, Color.white));
        }

    }


    IEnumerator GoFromColorToColorIn(float seconds, Color colorFrom, Color colorTo)
    {
        _spriteRenderer.color = colorFrom;
        yield return new WaitForSeconds(seconds);
        _spriteRenderer.color = colorTo;
    }

   /* void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }*/

    public void FaceCenter()
    {
        _spriteRenderer.flipX = transform.position.x >= _player.transform.position.x ?  true : false ;
    }


    public void MoveNormally(bool state)
    {
        moveNormally = state;
    }
    public void ToggleMoveNormally()
    {
        if (moveNormally) moveNormally = false;
        else moveNormally = true;
    }
}
