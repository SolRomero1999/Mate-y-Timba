using UnityEngine;

public class Carta : MonoBehaviour
{
    public int valor;          // 1–13 en la baraja de póker
    public string palo;        // trébol, corazón, etc.
    public Sprite frente;
    public Sprite dorso;

    private SpriteRenderer sr;
    public bool bocaArriba = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void MostrarFrente()
    {
        bocaArriba = true;
        sr.sprite = frente;
    }

    public void MostrarDorso()
    {
        bocaArriba = false;
        sr.sprite = dorso;
    }
}
