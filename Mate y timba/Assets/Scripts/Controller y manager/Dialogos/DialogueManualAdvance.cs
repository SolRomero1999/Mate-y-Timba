using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManualAdvance : MonoBehaviour
{
    public TMP_Text dialogueText;
    [TextArea] public string[] lines;
    public float charsPerSecond = 40f;

    public Button continuarButton;

    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        continuarButton.onClick.AddListener(NextLine);
        dialogueText.text = "";
        index = 0;
        NextLine();
    }

    private void NextLine()
    {
        if (index >= lines.Length && !isTyping)
        {
            Debug.Log("Fin diálogo → iniciar nivel");
            LevelManager.StartLevel();
            return;
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);

            int safeIndex = Mathf.Clamp(index, 0, lines.Length - 1);

            dialogueText.text = lines[safeIndex];
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
