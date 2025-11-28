using UnityEngine;
using System.Collections.Generic;

public class UI_Items : MonoBehaviour
{
    public Transform panelOpciones;           
    public GameObject cartaUIPrefab;         
    public List<Carta> cartasMostradas = new List<Carta>();

    private GameController game;

    private void Start()
    {
        game = FindFirstObjectByType<GameController>();
        panelOpciones.gameObject.SetActive(false);
    }

    public void RevelarCartasParaCerveza()
    {
        cartasMostradas.Clear();

        int cantidad = Mathf.Min(3, game.mazo.cartas.Count);

        for (int i = 0; i < cantidad; i++)
        {
            Carta c = game.mazo.RobarCarta();
            cartasMostradas.Add(c);
        }

        panelOpciones.gameObject.SetActive(true);

        foreach (Transform child in panelOpciones)
            Destroy(child.gameObject);

        foreach (Carta c in cartasMostradas)
        {
            GameObject ui = Instantiate(cartaUIPrefab, panelOpciones);
            ui.GetComponent<UI_Carta>().Configurar(c, this);
        }
    }

    public void ElegirCarta(Carta seleccionada)
    {
        GameController gc = game;

        seleccionada.transform.SetParent(gc.manoJugador);
        seleccionada.enMano = true;
        gc.manoActual.Add(seleccionada);
        gc.ReordenarMano();
        seleccionada.MostrarFrente();

        foreach (Carta c in cartasMostradas)
        {
            if (c != seleccionada)
            {
                gc.mazo.cartas.Add(c);
                c.transform.SetParent(gc.mazo.transform);
                c.MostrarDorso();
            }
        }

        gc.mazo.Barajar();

        panelOpciones.gameObject.SetActive(false);
    }
}
