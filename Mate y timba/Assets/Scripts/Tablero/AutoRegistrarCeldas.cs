using UnityEngine;

public class AutoRegistrarCeldas : MonoBehaviour
{
    private Tablero tablero;

    public Transform boardParent;

    private void Awake()
    {
        tablero = GetComponent<Tablero>();

        if (boardParent == null)
            boardParent = this.transform;

        RegistrarCeldasManuales();
    }

    private void RegistrarCeldasManuales()
    {
        Cell[] todasLasCeldas = boardParent.GetComponentsInChildren<Cell>();

        foreach (Cell c in todasLasCeldas)
        {
            tablero.RegistrarCelda(c.column, c.row, c.transform);
        }

        Debug.Log($"Se registraron {todasLasCeldas.Length} celdas hechas a mano.");
    }
}

