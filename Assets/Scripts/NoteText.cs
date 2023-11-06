using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            text.gameObject.SetActive(true);
        }
    }
}
