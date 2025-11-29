using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Carta : MonoBehaviour
{
    public Image imagen;
    private Carta cartaReal;
    private UI_Items controlador;

    public void Configurar(Carta carta, UI_Items ctrl)
    {
        cartaReal = carta;
        controlador = ctrl;
        imagen.sprite = carta.frente;
    }

    public void Seleccionar()
    {
        controlador.ElegirCarta(cartaReal);
    }

    public void OnClick()
    {
        controlador.SeleccionarCartaParaDescartar(cartaReal);
    }
}
