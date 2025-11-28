using UnityEngine;
using UnityEngine.InputSystem;

public class ItemComodinIcon : MonoBehaviour
{
    private ItemComodin item;

    void Start()
    {
        item = FindFirstObjectByType<ItemComodin>();
    }

    void OnMouseDown()
    {
        item.Activar();
        Debug.Log("Item Comodín activado. Selecciona una celda.");
    }
}
