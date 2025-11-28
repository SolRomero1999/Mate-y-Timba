using UnityEngine;

public class CartaComodin : Carta
{
    public void ConfigurarValorInicial(Cell celda, Tablero tablero)
    {
        int col = celda.column;
        int fila = celda.row;
        bool encontroCartaEnFilasPermitidas = false;
        int maxValor = 0;
        Sprite spriteACopiar = null;

        Debug.Log($"Buscando cartas para comodín en columna {col} y fila {fila}...");

        for (int f = 0; f < tablero.rows; f++)
        {
            Transform t = tablero.ObtenerCelda(col, f);
            if (t == null) continue;

            Cell c = t.GetComponent<Cell>();
            if (c == null) continue;
            if (!c.isOccupied) continue;
            if (c.carta == null) continue;

            Debug.Log($"Encontrada carta en columna [{col},{f}] - Valor: {c.carta.valor}, Fila permitida: {f >= 0 && f <= 3}");

            if (f >= 0 && f <= 3)
            {
                if (c.carta.valor >= 1 && c.carta.valor <= 12)
                {
                    encontroCartaEnFilasPermitidas = true;
                    if (c.carta.valor > maxValor)
                    {
                        maxValor = c.carta.valor;
                        spriteACopiar = c.carta.frente;
                    }
                }
            }
        }

        for (int c = 0; c < tablero.columns; c++)
        {
            Transform t = tablero.ObtenerCelda(c, fila);
            if (t == null) continue;

            Cell cell = t.GetComponent<Cell>();
            if (cell == null) continue;
            if (!cell.isOccupied) continue;
            if (cell.carta == null) continue;

            Debug.Log($"Encontrada carta en fila [{c},{fila}] - Valor: {cell.carta.valor}, Fila permitida: {fila >= 0 && fila <= 3}");

            if (fila >= 0 && fila <= 3)
            {
                if (cell.carta.valor >= 1 && cell.carta.valor <= 12)
                {
                    encontroCartaEnFilasPermitidas = true;
                    if (cell.carta.valor > maxValor)
                    {
                        maxValor = cell.carta.valor;
                        spriteACopiar = cell.carta.frente;
                    }
                }
            }
        }

        if (encontroCartaEnFilasPermitidas && maxValor > 0)
        {
            valor = maxValor;
            if (spriteACopiar != null)
            {
                frente = spriteACopiar;
                MostrarFrente();
            }
            Debug.Log($"Comodín configurado - Copió carta: Valor {valor}");
        }
        else
        {
            valor = 12; 
            Debug.Log($"Comodín configurado - Sin cartas válidas, valor por defecto: {valor}");
        }
    }
}