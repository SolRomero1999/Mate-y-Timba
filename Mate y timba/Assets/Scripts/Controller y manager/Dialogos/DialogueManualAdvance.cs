using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManualAdvance : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button continuarButton;

    [Header("Diálogos")]
    [TextArea] public string[] linesInicial;
    [TextArea] public string[] linesPostTutorial;
    [TextArea] public string[] linesPostNivel1;
    [TextArea] public string[] linesPostNivel2;

    public float charsPerSecond = 40f;

    private string[] lines;
    private int index = 0;
    private bool isTyping;
    private Coroutine typingCoroutine;

    private void Start()
    {
        Debug.Log($"=== DIALOGO START ===");
        Debug.Log($"CurrentLevel: {LevelManager.CurrentLevel}");
        Debug.Log($"UltimoNivelCompletado: {LevelManager.UltimoNivelCompletado}");
        Debug.Log($"tutorialDialogoVisto: {LevelManager.tutorialDialogoVisto}");
        Debug.Log($"EsPrimerDialogo(): {LevelManager.EsPrimerDialogo()}");
        Debug.Log($"EsPostTutorial(): {LevelManager.EsPostTutorial()}");
        
        continuarButton.onClick.AddListener(NextLine);
        dialogueText.text = "";

        SeleccionarDialogo();
        NextLine();
    }

    private void SeleccionarDialogo()
    {
        if (LevelManager.EsPrimerDialogo())
        {
            lines = linesInicial;
        }
        else if (LevelManager.EsPostTutorial())
        {
            lines = linesPostTutorial;
        }
        else
        {
            int nivelCompletado = LevelManager.UltimoNivelCompletado;
            if (nivelCompletado == 1)
                lines = linesPostNivel1;
            else if (nivelCompletado == 2)
                lines = linesPostNivel2;
            else
                lines = linesPostNivel2; 
        }
    }

    private void NextLine()
    {
        if (index >= lines.Length && !isTyping)
        {
            IrASiguienteEscena();
            return;
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = lines[Mathf.Clamp(index - 1, 0, lines.Length - 1)];
            isTyping = false;
            return;
        }

        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
        index++;
    }

    private void IrASiguienteEscena()
    {
        Debug.Log($"=== IR A SIGUIENTE ESCENA ===");
        Debug.Log($"TutorialVisto: {LevelManager.tutorialDialogoVisto}");
        Debug.Log($"CurrentLevel: {LevelManager.CurrentLevel}");
        Debug.Log($"UltimoNivelCompletado: {LevelManager.UltimoNivelCompletado}");
        
        if (!LevelManager.tutorialDialogoVisto)
        {
            Debug.Log("Iniciando TUTORIAL por primera vez");
            LevelManager.IniciarTutorial();
        }
        else
        {
            Debug.Log($"Iniciando NIVEL {LevelManager.CurrentLevel}");
            LevelManager.IniciarNivelActual();
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(1f / charsPerSecond);
        }

        isTyping = false;
    }

    private void OnDestroy()
    {
        continuarButton.onClick.RemoveListener(NextLine);
    }
}
