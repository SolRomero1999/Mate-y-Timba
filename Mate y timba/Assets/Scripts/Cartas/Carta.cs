using UnityEngine;
using UnityEngine.InputSystem;

public class Carta : MonoBehaviour
{
    public int valor;
    public string palo;
    public Sprite frente;
    public Sprite dorso;

    private SpriteRenderer sr;
    private Vector3 posicionOriginal;
    private float alturaHover = 0.5f;
    private float velocidadMovimiento = 15f;
    private bool estaEnHover = false;
    private bool hoverAnterior = false;
    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    private Mouse mouse;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
        mouse = Mouse.current;
    }

    private void Start()
    {
        posicionOriginal = transform.localPosition;
    }

    private void Update()
    {
        bool hoverActual = EstaMouseSobreCarta();
        
        if (hoverActual != hoverAnterior)
        {
            estaEnHover = hoverActual;
            hoverAnterior = hoverActual;
        }

        if (transform.parent != null && transform.parent.name != "Mazo")
        {
            Vector3 posicionObjetivo = estaEnHover ? 
                posicionOriginal + Vector3.up * alturaHover : 
                posicionOriginal;
            
            transform.localPosition = Vector3.Lerp(transform.localPosition, posicionObjetivo, velocidadMovimiento * Time.deltaTime);
        }
    }

    private bool EstaMouseSobreCarta()
    {
        if (mainCamera == null || boxCollider == null || mouse == null) return false;
        
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouse.position.ReadValue());
        return boxCollider.OverlapPoint(mouseWorldPos);
    }

    public void MostrarFrente()
    {
        sr.sprite = frente;
    }

    public void MostrarDorso()
    {
        sr.sprite = dorso;
    }

    public void SetPosicionOriginal(Vector3 nuevaPosicion)
    {
        posicionOriginal = nuevaPosicion;
        transform.localPosition = nuevaPosicion;
    }
}