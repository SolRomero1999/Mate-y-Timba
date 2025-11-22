using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public GameController game;  
    public float delayIA = 1f;    

    private bool turnoJugador = true;

    private void Start()
    {
        StartCoroutine(IniciarTurnos());
    }

    private IEnumerator IniciarTurnos()
    {
        yield return new WaitForSeconds(1f);
        IniciarTurnoJugador();
    }

    private void IniciarTurnoJugador()
    {
        turnoJugador = true;
        FindFirstObjectByType<GameController>().jugadorYaRobo = false;
        Debug.Log("TURNO DEL JUGADOR");
    }

    public void TerminarTurnoJugador()
    {
        if (!turnoJugador) return;

        turnoJugador = false;
        Debug.Log("Jugador terminó su turno");

        StartCoroutine(TurnoIA());
    }

    private IEnumerator TurnoIA()
    {
        Debug.Log("TURNO DE LA IA");

        yield return new WaitForSeconds(delayIA);

        if (game.manoIAActual.Count < 5)
        {
            Debug.Log("IA roba carta");
            game.RobarCartaIA();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            Debug.Log("IA juega carta");

            game.IA_JugarCarta();
            yield return new WaitForSeconds(0.5f);
        }

        IniciarTurnoJugador();
    }
}