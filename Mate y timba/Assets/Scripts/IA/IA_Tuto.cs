using UnityEngine;
using System.Linq;

public class IA_Tuto : IA_Base
{
    public override void RobarCartaIA()
    {
        if (game.manoIAActual.Count < 5)
            game.RobarCartaIA();
        else
            JugarTurno();
    }

    public override void JugarTurno()
    {
        if (game.manoIAActual.Count == 0) return;

        Carta carta = game.manoIAActual
            .OrderBy(c => c.valor)
            .First();

        Cell celda = tablero.ObtenerCeldaLibreIA();
        if (celda == null) return;

        carta.ColocarEnCelda(celda);
        carta.MostrarFrente();
        game.manoIAActual.Remove(carta);

        Debug.Log($"[IA TUTO] Jugó {carta.valor}");
    }
}