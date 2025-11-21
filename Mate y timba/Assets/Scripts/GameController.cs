using UnityEngine;

public class GameController : MonoBehaviour
{
    public Mazo mazo;                      // asignar en el editor
    public Transform manoJugador;          // asignar en el editor
    public GameObject cartaPrefab;         // prefab de Carta
    public Sprite dorsoCarta;              // asignar
    public Sprite[] frentes;               // 52 sprites del mazo de póker

    void Start()
    {
        CrearMazo();
        mazo.Barajar();
        RepartirCartasIniciales(5);
    }

    void CrearMazo()
    {
        for(int i = 0; i < frentes.Length; i++)
        {
            GameObject nueva = Instantiate(cartaPrefab);
            Carta c = nueva.GetComponent<Carta>();

            c.frente = frentes[i];
            c.dorso = dorsoCarta;

            // Asignar valores opcionalmente si querés lógica:
            c.valor = (i % 13) + 1;
            c.palo = "SinUsarPorAhora";

            c.MostrarDorso();

            // Ponerlas como hijas del mazo (invisibles excepto el dorso)
            nueva.transform.SetParent(mazo.transform);
            nueva.transform.localPosition = Vector3.zero;

            mazo.cartas.Add(c);
        }
    }

    void RepartirCartasIniciales(int cantidad)
    {
        float spacing = 1.5f;
        float totalWidth = (cantidad - 1) * spacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cantidad; i++)
        {
            Carta robada = mazo.RobarCarta();

            if (robada != null)
            {
                robada.transform.SetParent(manoJugador);

                float x = startX + (i * spacing);

                robada.transform.localPosition = new Vector3(x, 0, 0);
                robada.MostrarFrente();
            }
        }
    }
}
