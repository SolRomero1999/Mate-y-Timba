using UnityEngine;
using System.Collections;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public GameController game;
    public GameObject panelResultado;
    public TextMeshProUGUI mensajeFinal;
    public float delayIA = 1f;
    private bool turnoJugador = true;

    #region Inicio
    private void Start()
    {
        if (panelResultado != null)
            panelResultado.SetActive(false);

        mensajeFinal.gameObject.SetActive(false);

        StartCoroutine(IniciarTurnos());
    }

    private IEnumerator IniciarTurnos()
    {
        yield return new WaitForSeconds(1f);
        IniciarTurnoJugador();
    }
    #endregion

    #region Turno Jugador
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

        VerificarFinDePartida();
        StartCoroutine(TurnoIA());
    }

    public void ForzarFinTurnoJugador()
    {
        turnoJugador = false;
        StartCoroutine(TurnoIA());
    }
    #endregion

    #region Turno IA
    private IEnumerator TurnoIA()
    {
        Debug.Log("TURNO DE LA IA");

        yield return new WaitForSeconds(delayIA);

        if (game.manoIAActual.Count < 5)
        {
            game.RobarCartaIA();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            game.IA_JugarCarta();
            yield return new WaitForSeconds(0.5f);
        }

        VerificarFinDePartida();
        IniciarTurnoJugador();
    }
    #endregion

    #region Fin de Partida
    public void VerificarFinDePartida()
    {
        bool jugadorTieneCeldas = game.tablero.HayCeldasDisponiblesJugador();
        bool iaTieneCeldas = game.tablero.HayCeldasDisponiblesIA();
        bool mazoVacio = game.mazo.cartas.Count == 0;

        if (!jugadorTieneCeldas || !iaTieneCeldas || mazoVacio)
        {
            FinalizarPartida();
        }
        Debug.Log($"[DEBUG FIN] JugadorCeldas:{jugadorTieneCeldas} | IACeldas:{iaTieneCeldas} | MazoVacio:{mazoVacio}");
    }

    private void FinalizarPartida()
    {
        ScoreManager sm = FindFirstObjectByType<ScoreManager>();
        sm.ActualizarPuntajes();

        int puntosJugador = int.Parse(sm.puntajeTotalJugador.text);
        int puntosIA = int.Parse(sm.puntajeTotalIA.text);
        int diferencia = Mathf.Abs(puntosJugador - puntosIA);

        panelResultado.SetActive(true);
        mensajeFinal.gameObject.SetActive(true);

        bool jugadorGana = puntosJugador > puntosIA;

        if (!LevelManager.reglasEliminacionActivas)
        {
            if (jugadorGana)
            {
                mensajeFinal.text = $"<color=#4CFF4C>¡TUTORIAL COMPLETADO!</color>\nGanaste por {diferencia} puntos.";
                Debug.Log("GANÓ EL JUGADOR EN EL TUTORIAL → Diálogo post tutorial");
                StartCoroutine(VolverAlNieto_PostTutorial());
            }
            else
            {
                mensajeFinal.text = $"<color=#FF4C4C>DERROTA</color>\nVolvamos a intentar el tutorial…";
                Debug.Log("PERDIÓ EL JUGADOR EN EL TUTORIAL → Reiniciar tutorial");
                StartCoroutine(ReiniciarTutorial());
            }

            Time.timeScale = 0f;
            return;
        }

        if (jugadorGana)
        {
            mensajeFinal.text = $"<color=#4CFF4C>VICTORIA</color>\nGanaste por {diferencia} puntos.";
            Debug.Log("VICTORIA DEL JUGADOR EN NIVEL NORMAL");

            LevelManager.NextLevel();
            StartCoroutine(VolverAlNieto_Normal());
        }
        else
        {
            mensajeFinal.text = $"<color=#FF4C4C>DERROTA</color>\nLa IA ganó por {diferencia} puntos.";
            Debug.Log("DERROTA – repetir nivel normal");
            StartCoroutine(RepetirNivelNormal());
        }

        Time.timeScale = 0f;
    }

    private IEnumerator ReiniciarTutorial()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        LevelManager.StartLevelTuto();
    }

    private IEnumerator VolverAlNieto_PostTutorial()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        LevelManager.GoToDialogue_PostTutorial();
    }

    private IEnumerator VolverAlNieto_Normal()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        LevelManager.GoToDialogue();
    }

    private IEnumerator RepetirNivelNormal()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        LevelManager.StartLevelNormal();
    }
    #endregion
}