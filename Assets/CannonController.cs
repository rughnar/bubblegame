using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public Image radialbar;
    public TMP_Text heightText;
    public TMP_Text velocityText;
    private BubbleController bubbleController;
    private PlayerInputActions playerInputActions;
    private InputAction _fire;

    void Awake()
    {

        bubbleController = FindObjectOfType<BubbleController>();
        bubbleController.SoftDisable();
        radialbar.enabled = true;
        heightText.enabled = false;
        velocityText.enabled = false;
        playerInputActions = new PlayerInputActions();
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
        Vector3 direction = mousePosition - transform.position;

        // Calcula el ángulo de rotación en base a la dirección
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Vector2.SignedAngle(Vector2.right, direction) - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Fire(InputAction.CallbackContext callbackContext)
    {
        radialbar.enabled = false;
        bubbleController.SoftEnable();
        heightText.enabled = true;
        velocityText.enabled = true;
        this.enabled = false;
    }
}
