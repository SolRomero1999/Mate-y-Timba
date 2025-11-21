using UnityEngine;

public class CartaHover : MonoBehaviour
{
    private Vector3 posicionInicial;
    private SpriteRenderer sr;

    public float hoverOffsetY = 0.3f;
    public int sortingOrderHovered = 100;

    private int sortingOrderOriginal;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        posicionInicial = transform.localPosition;
        sortingOrderOriginal = sr.sortingOrder;
    }

    void OnMouseEnter()
    {
        // Levantar la carta
        transform.localPosition = posicionInicial + new Vector3(0, hoverOffsetY, 0);

        // Poner arriba de todas las otras
        sr.sortingOrder = sortingOrderHovered;
    }

    void OnMouseExit()
    {
        // Volver a su posición original
        transform.localPosition = posicionInicial;

        // Restaurar sorting original
        sr.sortingOrder = sortingOrderOriginal;
    }
    
}
