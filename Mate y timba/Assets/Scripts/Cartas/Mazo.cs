using UnityEngine;
using System.Collections.Generic;

public class Mazo : MonoBehaviour
{
    public List<Carta> cartas = new List<Carta>();

    public void Barajar()
    {
        for (int i = 0; i < cartas.Count; i++)
        {
            Carta temp = cartas[i];
            int randomIndex = Random.Range(i, cartas.Count);
            cartas[i] = cartas[randomIndex];
            cartas[randomIndex] = temp;
        }
    }

    public Carta RobarCarta()
    {
        if (cartas.Count == 0) return null;

        Carta c = cartas[0];
        cartas.RemoveAt(0);
        return c;
    }
}