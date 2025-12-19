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
    private bool partidaTerminada = false;

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
    }

    public void TerminarTurnoJugador()
    {
        if (!turnoJugador) return;

        turnoJugador = false;
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
        yield return new WaitForSeconds(delayIA);

        if (game.manoIAActual.Count < 5)
        {
            game.ia.RobarCartaIA();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            game.ia.JugarTurno();
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
    }

    private void FinalizarPartida()
    {
        if (partidaTerminada) return;
        partidaTerminada = true;

        ScoreManager sm = FindFirstObjectByType<ScoreManager>();
        sm.ActualizarPuntajes();

        int puntosJugador = int.Parse(sm.puntajeTotalJugador.text);
        int puntosIA = int.Parse(sm.puntajeTotalIA.text);

        panelResultado.SetActive(true);
        mensajeFinal.gameObject.SetActive(true);

        bool jugadorGana = puntosJugador > puntosIA;

        if (LevelManager.CurrentLevel == 0)
        {
            if (jugadorGana)
            {
                mensajeFinal.text = "¡TUTORIAL COMPLETADO!";
                LevelManager.tutorialDialogoVisto = true;
                LevelManager.UltimoNivelCompletado = 0;
                LevelManager.CurrentLevel = 1;
                StartCoroutine(VolverADialogo());
            }
            else
            {
                mensajeFinal.text = "REINTENTA EL TUTORIAL";
                StartCoroutine(ReiniciarTutorial());
            }

            Time.timeScale = 0f;
            return;
        }

        if (jugadorGana)
        {
            mensajeFinal.text = "VICTORIA";
            LevelManager.AvanzarNivel();
            StartCoroutine(VolverADialogo());
        }
        else
        {
            mensajeFinal.text = "DERROTA";
            LevelManagerFlags.VieneDeDerrota = true;
            StartCoroutine(VolverADialogo());
        }

        Time.timeScale = 0f;
    }

    private IEnumerator VolverADialogo()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        LevelManager.IrADialogo();
    }

    private IEnumerator ReiniciarTutorial()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        LevelManager.IniciarTutorial();
    }
    #endregion
}
