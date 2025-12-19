using UnityEngine;
using System.Collections.Generic;

public class Tablero : MonoBehaviour
{
    public Transform[,] celdas;

    [Header("Dimensiones del tablero")]
    public int columns = 4;
    public int rows = 8;

    [Header("Distribución de filas")]
    public int filasJugador = 4;
    public int filasIA = 4;

    #region Unity
    private void Awake()
    {
        celdas = new Transform[columns, rows];

        if (filasJugador + filasIA != rows)
        {
            Debug.LogWarning($"[TABLERO] filasJugador ({filasJugador}) + filasIA ({filasIA}) != rows ({rows}). Ajustando...");
            filasIA = rows - filasJugador;
        }
    }
    #endregion

    #region Registro y acceso
    public void RegistrarCelda(int col, int fila, Transform celda)
    {
        if (col < 0 || col >= columns || fila < 0 || fila >= rows)
        {
            Debug.LogWarning($"[TABLERO] Celda fuera de rango: ({col},{fila})");
            return;
        }

        celdas[col, fila] = celda;
    }

    public Transform ObtenerCelda(int col, int fila)
    {
        if (col < 0 || col >= columns || fila < 0 || fila >= rows)
            return null;

        return celdas[col, fila];
    }
    #endregion

    #region Propietarios de filas
    public bool EsFilaJugador(int fila) => fila >= 0 && fila < filasJugador;

    public bool EsFilaRival(int fila) =>
        fila >= filasJugador && fila < filasJugador + filasIA;
    #endregion

    #region Celdas IA
    public Cell ObtenerCeldaLibreIA()
    {
        int inicio = filasJugador;

        for (int fila = inicio; fila < inicio + filasIA; fila++)
        {
            for (int col = 0; col < columns; col++)
            {
                Transform t = celdas[col, fila];
                if (t == null) continue;

                Cell celda = t.GetComponent<Cell>();
                if (celda != null && !celda.isOccupied)
                    return celda;
            }
        }

        return null;
    }

    public List<Cell> ObtenerCeldasLibresIA()
    {
        List<Cell> libres = new List<Cell>();
        int inicio = filasJugador;

        for (int fila = inicio; fila < inicio + filasIA; fila++)
        {
            for (int col = 0; col < columns; col++)
            {
                Transform t = celdas[col, fila];
                if (t == null) continue;

                Cell celda = t.GetComponent<Cell>();
                if (celda != null && !celda.isOccupied)
                    libres.Add(celda);
            }
        }

        return libres;
    }
    #endregion

    #region Disponibilidad
    public bool HayCeldasDisponiblesJugador()
    {
        for (int fila = 0; fila < filasJugador; fila++)
        {
            for (int col = 0; col < columns; col++)
            {
                Transform t = celdas[col, fila];
                if (t == null) continue;

                Cell celda = t.GetComponent<Cell>();
                if (celda != null && !celda.isOccupied)
                    return true;
            }
        }
        return false;
    }

    public bool HayCeldasDisponiblesIA()
    {
        int inicio = filasJugador;

        for (int fila = inicio; fila < inicio + filasIA; fila++)
        {
            for (int col = 0; col < columns; col++)
            {
                Transform t = celdas[col, fila];
                if (t == null) continue;

                Cell celda = t.GetComponent<Cell>();
                if (celda != null && !celda.isOccupied)
                    return true;
            }
        }
        return false;
    }
    #endregion
}