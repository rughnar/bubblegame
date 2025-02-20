using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CannonController : MonoBehaviour

{
    public AudioClip shoot;
    public float fireForce = 25;
    public float rotationSpeed = 10f;
    public VelocityBarController velocityBar;
    public GameObject cannonBase;
    private PlayerController bubbleController;
    private PlayerInputActions playerInputActions;
    private InputAction _fire;
    private Vector3 direction;
    private GameManager gameManager;
    private Animator cannonBaseAnimator;
    private AudioManager audioManager;

    void Awake()
    {
        bubbleController = FindObjectOfType<PlayerController>();
        velocityBar = FindObjectOfType<VelocityBarController>();
        bubbleController.SoftDisable();
        playerInputActions = new PlayerInputActions();
        gameManager = FindObjectOfType<GameManager>();
        cannonBaseAnimator = cannonBase.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }


    private void OnEnable()
    {
        _fire = playerInputActions.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;


    }

    private void OnDisable()
    {
        _fire.Disable();
    }


    void Update()
    {
        // Obtiene la posición del mouse en la pantalla
        Vector3 mousePosition = Input.mousePosition;

        // Convierte la posición del mouse a un punto en el mundo
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        // Calcula la dirección desde el objeto hacia la posición del mouse
        direction = mousePosition - transform.position;

        // Calcula el ángulo de rotación en base a la dirección
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Vector2.SignedAngle(Vector2.right, direction) - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Fire(InputAction.CallbackContext callbackContext)
    {

        StartCoroutine(FireWithDelay());

    }

    private IEnumerator FireWithDelay()
    {
        cannonBaseAnimator.SetTrigger("pull");
        yield return new WaitForSeconds(0.4f);
        audioManager.PlaySFX(shoot);
        bubbleController.SoftEnable();
        float force = velocityBar.GetValue();
        bubbleController.ApplyForce(direction.normalized * force * fireForce);
        gameManager.PlayerWasShot();
        this.gameObject.gameObject.SetActive(false);
    }
}
