using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class DialogueScript: MonoBehaviour
{
    [TextArea]
    public string[] lines;
    public TMP_Text dialogueText;
    
    
    public float characterPerSecond = 90;
    private bool isFinished = false;
    private int currentLine = 0;
    

    private void Start()
    {
        isFinished = true;
    }

    private void Update()
    {
        if (currentLine != lines.Length && isFinished)
        { 
            StartCoroutine(TypeTextUncapped(lines[currentLine]));
            currentLine++;
            isFinished = false;
        }
        else if (currentLine == lines.Length && isFinished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / characterPerSecond;
        string textBuffer = null;

        char[] chars = line.ToCharArray();

        int i = 0;
        while (i  < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }

        if (i == chars.Length)
        {
            yield return new WaitForSeconds(3f);
            isFinished = true;
        }
    }
}