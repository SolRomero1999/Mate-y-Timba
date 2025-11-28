using UnityEngine;

public class ItemCerveza : MonoBehaviour
{
    private GameController game;

    private void Start()
    {
        game = FindFirstObjectByType<GameController>();
    }

    private void OnMouseDown()
    {
        Usar();
    }

    public void Usar()
    {
        if (game.manoActual.Count >= game.maxCartasMano)
        {
            Debug.Log("No puedes usar la cerveza, tienes 5 cartas.");
            return;
        }

        Debug.Log("Usaste CERVEZA → revelando 3 cartas.");
        game.UI_Items.RevelarCartasParaCerveza();
        Destroy(gameObject);
    }
}

