using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;

    private int i;
    private bool hasFinished;

    void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if(text.text == lines[i])
        {
            Invoke("NextLine", 1.2f);
        }
    }

    void StartDialogue()
    {
        i = 0;
        StartCoroutine(TypeLine(0));
        //play sound here how did i get here
        FindObjectOfType<AudioManager>().Play("StartScream");
    }

    IEnumerator TypeLine(int line)
    {
        //if(hasFinished == false)
        //{
        hasFinished = false;
            foreach (char c in lines[line].ToCharArray())
            {
                text.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(1f);
            ClearText();
            hasFinished = true;
        //}
        
        
    }

    void NextLine()
    {
        if(i < lines.Length - 1)
        {
            i++;
            //StartCoroutine (TypeLine(i));

        }
    }

    void ClearText()
    {
        text.text = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DoorTrigger" && hasFinished)
        {
            StartCoroutine(TypeLine(1));
            FindObjectOfType<AudioManager>().Play("Code");
        }
    }
}
