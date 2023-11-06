using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DotScript : MonoBehaviour
{
    Image image;
    Vector3 scale;
    private void Start()
    {
        image = GameObject.Find("Dot").GetComponent<Image>();
        image.color = Color.grey;
        scale = image.transform.localScale;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            image.color = Color.red;
            image.transform.localScale *= 1.5f;
            Invoke("SwitchColour", 0.1f);
            Invoke("ResetScale", 0.1f);
        }
    }

    private void SwitchColour()
    {
        image.color = Color.grey;
    }

    private void ResetScale()
    {
        image.transform.localScale = scale;
    }

    //private void OnMouseOver()
    //{
    //    if (CompareTag("Hoverable"))
    //    {
    //        image.color = Color.red;
    //    }
        
    //}
}
