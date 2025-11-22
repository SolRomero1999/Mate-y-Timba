using UnityEngine;

public class ClickMazo : MonoBehaviour
{
    public GameController game;

private void OnMouseDown()
    {
        Debug.Log("Click detectado en el mazo.");
        game.IntentarRobarCarta();
    }
}
