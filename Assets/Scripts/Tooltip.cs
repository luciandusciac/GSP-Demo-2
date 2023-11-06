using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{

}


public class Tooltip : MonoBehaviour
{

    public Transform interactorSource;
    public float interactRange;

    public string message;

    public bool canInteract = false;

    private void OnMouseEnter()
    {
        if(canInteract)
        {
            if (gameObject.CompareTag("Pickup"))
            {
                gameObject.GetComponent<Outline>().enabled = true;
                TooltipManager._instance.ShowTooltip(message);
            }
            if (GameObject.FindObjectOfType<Keypad>().inRange || GameObject.FindObjectOfType<CharacterController>().GetComponent<ClickObjects>().inRange)
            {
                if (!GameObject.FindObjectOfType<Keypad>().playerFrozen && !GameObject.FindObjectOfType<Keypad>().solved)
                {
                    TooltipManager._instance.ShowTooltip(message);
                    gameObject.GetComponent<Outline>().enabled = true;
                }
            }
            if (interactorSource.gameObject.GetComponentInParent<PlayerMovement>().inNoteRange)
            {

                TooltipManager._instance.ShowTooltip(message);
                gameObject.GetComponent<Outline>().enabled = true;
            }
        }





        //if (gameObject.CompareTag("Pickup"))
        //{
        //    gameObject.GetComponent<Outline>().enabled = true;
        //    TooltipManager._instance.ShowTooltip(message);
        //}
        //if (GameObject.FindObjectOfType<Keypad>().inRange || GameObject.FindObjectOfType<CharacterController>().GetComponent<ClickObjects>().inRange)
        //{
        //    if (!GameObject.FindObjectOfType<Keypad>().playerFrozen && !GameObject.FindObjectOfType<Keypad>().solved)
        //    {
        //        TooltipManager._instance.ShowTooltip(message);
        //        gameObject.GetComponent<Outline>().enabled = true;
        //    }
        //}
        //if (GameObject.FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().inNoteRange)
        //{

        //    TooltipManager._instance.ShowTooltip(message);
        //    gameObject.GetComponent<Outline>().enabled = true;
        //}
        
        
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.RemoveTooltip();
        gameObject.GetComponent<Outline>().enabled = false;
        canInteract = false;
    }

    private void Update()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }

    private void Start()
    {
        interactorSource = GameObject.Find("Main Camera").transform;
    }
}
