using UnityEngine;

public class ItemMate : MonoBehaviour
{
    public UI_Items ui;

    private void Start()
    {
        ui = FindFirstObjectByType<UI_Items>();
    }

    private void OnMouseDown()
    {
        ui.ActivarMate();
        Destroy(gameObject);
    }
}
