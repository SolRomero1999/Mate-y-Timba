using UnityEngine;
using System.Linq;

public class IA_Abuelo : IA_Base
{
    public override void JugarTurno()
    {
        if (game.manoIAActual.Count == 0) return;

        //Elegir SIEMPRE la carta de menor valor
        Carta cartaElegida = game.manoIAActual
            .OrderBy(c => c.valor)
            .First();

        Cell celda = tablero.ObtenerCeldaLibreIA();

        if (celda == null) return;

        cartaElegida.ColocarEnCelda(celda);
        cartaElegida.MostrarFrente();
        game.manoIAActual.Remove(cartaElegida);

        ScoreManager sm = FindFirstObjectByType<ScoreManager>();
        if (sm != null) sm.ActualizarPuntajes();

        Debug.Log($"[IA ABUELO] Jugó carta {cartaElegida.valor}");
    }
}
