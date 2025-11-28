using UnityEngine;
using UnityEngine.InputSystem;

public class ItemComodin : MonoBehaviour
{
    public GameObject comodinPrefab;
    private bool esperando = false;
    private Tablero tablero;

    void Start()
    {
        tablero = FindFirstObjectByType<Tablero>();
    }

    public void Activar()
    {
        esperando = true;
    }

    void Update()
    {
        if (!esperando) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!hit) return;

            Cell celda = hit.collider.GetComponent<Cell>();
            if (celda == null || celda.isOccupied) return;

            ColocarComodin(celda);
        }
    }

    private void ColocarComodin(Cell celda)
    {
        esperando = false;

        GameObject obj = Instantiate(comodinPrefab, celda.transform.position, Quaternion.identity, celda.transform);
        CartaComodin carta = obj.GetComponent<CartaComodin>();

        celda.SetOccupied(carta);
        carta.ConfigurarValorInicial(celda, tablero);

        ScoreManager sm = FindFirstObjectByType<ScoreManager>();
        if (sm != null) 
        {
            sm.ActualizarPuntajes();
            Debug.Log($"Puntajes actualizados inmediatamente para comodín (valor: {carta.valor})");
        }

        Debug.Log("Comodín colocado en columna " + celda.column);
    }
}