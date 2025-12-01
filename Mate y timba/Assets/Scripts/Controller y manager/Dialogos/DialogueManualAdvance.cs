using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManualAdvance : MonoBehaviour
{
    public TMP_Text dialogueText;

    [Header("Diálogo inicial")]
    [TextArea] public string[] linesInicial;

    [Header("Diálogo después del tutorial")]
    [TextArea] public string[] linesPostTutorial;

    public float charsPerSecond = 40f;
    public Button continuarButton;

    private string[] lines;  
    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        continuarButton.onClick.AddListener(NextLine);
        dialogueText.text = "";
        index = 0;

        lines = LevelManager.dialogoPostTutorial ? linesPostTutorial : linesInicial;

        NextLine();
    }

    private void NextLine()
    {
        if (index >= lines.Length && !isTyping)
        {
            Debug.Log("Fin diálogo");

            if (LevelManager.dialogoPostTutorial)
            {
                LevelManager.dialogoPostTutorial = false;
                LevelManager.StartLevelNormal();
            }
            else
            {
                LevelManager.StartLevelTuto();
            }

            return;
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = lines[Mathf.Clamp(index, 0, lines.Length - 1)];
            isTyping = false;
            return;
        }

        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
        index++;
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