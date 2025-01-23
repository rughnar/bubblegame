using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Awake()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
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
        // Aplica la rotación al objeto
        //if (angle >= -80 && angle <= 80)
        //{
        //float step = rotationSpeed * Time.deltaTime; // Calcula el paso de rotación
        //Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        //}

    }
}
