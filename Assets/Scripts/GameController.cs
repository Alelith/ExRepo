using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [Tooltip("Objetos a ser instanciados")]
    [SerializeField]
    private GameObject[] prefabObjects;

    [Tooltip("Número de objetos a instanciar")]
    [Range(0, 20)]
    [SerializeField]
    public int objectNumber;

    [Tooltip("Posición máxima")]
    [SerializeField]
    public Vector3 maxPosition;

    [Tooltip("Posición mínima")]
    [SerializeField]
    public Vector3 minPosition;

    [Tooltip("Controlador del jugador")]
    [SerializeField]
    public PlayerController player;

    private ArrayList posiciones = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        posiciones.Add(Physics.OverlapBox(new Vector3(maxPosition.x + 1, maxPosition.y + 1, maxPosition.z + 1), prefabObjects[0].transform.localScale / 2, Quaternion.identity));
        GeneratePickUp();
    }

    /// <summary>
    /// Método para la generación de los pick up
    /// </summary>
    private void GeneratePickUp()
    {
        for (int i = 0; i < objectNumber; i++)
        {
            Vector3 initialPos = new Vector3(maxPosition.x + 1, maxPosition.y + 1, maxPosition.z + 1);
            while (posiciones.Contains(Physics.OverlapBox(initialPos, prefabObjects[0].transform.localScale / 2, Quaternion.identity)))
            {
                //Crea la posición inicial de los objetos
                initialPos = new Vector3(Random.Range(minPosition.x, maxPosition.x), 0.75f, Random.Range(minPosition.z, maxPosition.z));

            }
            posiciones.Add(Physics.OverlapBox(initialPos, prefabObjects[0].transform.localScale / 2, Quaternion.identity));
            //Instancia un objeto al juego y lo escala
            Instantiate(prefabObjects[Random.Range(0, prefabObjects.Length - 1)], initialPos, Quaternion.identity);
        }
        player.SetInitialValues(objectNumber);
    }
}
