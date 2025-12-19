using UnityEngine;
using System.Linq;

public class IA_SegundoN : IA_Base
{
    #region Turno IA
    public override void RobarCartaIA()
    {
        if (IntentarEliminarCartaJugador())
            return;

        if (game.manoIAActual.Count < 5)
        {
            game.RobarCartaIA();
            return;
        }

        JugarTurno();
    }

    public override void JugarTurno()
    {
        if (IntentarEliminarCartaJugador())
            return;

        if (game.manoIAActual.Count == 0) return;

        Carta carta = game.manoIAActual[Random.Range(0, game.manoIAActual.Count)];
        Cell celda = tablero.ObtenerCeldaLibreIA();
        if (celda == null) return;

        carta.ColocarEnCelda(celda);
        carta.MostrarFrente();
        game.manoIAActual.Remove(carta);

        Debug.Log($"[IA NIÑEZ] Jugó {carta.valor} al azar");
    }
    #endregion

    #region Eliminación Reactiva
    private bool IntentarEliminarCartaJugador()
    {
        int inicioFilaJugador = 0;
        int finFilaJugador = tablero.filasJugador - 1;

        for (int col = 0; col < tablero.columns; col++)
        {
            for (int fila = inicioFilaJugador; fila <= finFilaJugador; fila++)
            {
                Cell celdaJugador = tablero
                    .ObtenerCelda(col, fila)?
                    .GetComponent<Cell>();
                if (celdaJugador == null || !celdaJugador.isOccupied)
                    continue;

                Carta cartaJugador = celdaJugador.GetComponentInChildren<Carta>();
                if (cartaJugador == null)
                    continue;

                int valorJugador = cartaJugador.valor;

                Carta cartaIA = game.manoIAActual
                    .FirstOrDefault(c => c.valor == valorJugador);

                if (cartaIA == null)
                    continue;

                Cell celdaLibreIA = ObtenerCeldaLibreIAEnColumna(col);
                if (celdaLibreIA == null)
                    continue;

                cartaIA.ColocarEnCelda(celdaLibreIA);
                cartaIA.MostrarFrente();
                game.manoIAActual.Remove(cartaIA);

                Debug.Log($"[IA NIÑEZ] Eliminó {valorJugador} en columna {col}");
                return true;
            }
        }

        return false;
    }

    private Cell ObtenerCeldaLibreIAEnColumna(int columna)
    {
        int inicioFilaIA = tablero.filasJugador;
        int finFilaIA = tablero.filasJugador + tablero.filasIA - 1;

        for (int fila = inicioFilaIA; fila <= finFilaIA; fila++)
        {
            Cell celda = tablero
                .ObtenerCelda(columna, fila)?
                .GetComponent<Cell>();

            if (celda != null && !celda.isOccupied)
                return celda;
        }

        return null;
    }
    #endregion
}

