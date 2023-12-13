using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Velocidad del jugador")]
    [SerializeField]
    private float speed;

    [Tooltip("Cuadro de texto de la puntuación")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Tooltip("Cuadro de texto de juego finalizado")]
    [SerializeField]
    private TextMeshProUGUI winText;

    [Tooltip("Cuadro de texto de juego tiempo")]
    [SerializeField]
    private TextMeshProUGUI timeText;

    [Tooltip("Cuadro de texto de juego perdido")]
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    private Rigidbody rb;

    private int segundos = 10;

    private float movementX;
    private float movementY;
    private int count;

    private int pickUp;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Función listener que espera movimiento
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(movementX, 0f, movementY);
        rb.AddForce(movementVector * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 125;

            scoreText.text = "Score: " + count.ToString("0000");
        }
        if ((pickUp * 125) == count)
        {
            winText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
    }

    public void SetInitialValues(int pickUp)
    {
        this.pickUp = pickUp;
    }

    private IEnumerator CuentaAtras()
    {
        segundos--;
        timeText.text = "Time: " + segundos.ToString("000");
        yield return new WaitForSeconds(1);
    }
}
