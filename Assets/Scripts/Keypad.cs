using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class Keypad : MonoBehaviour
{
    public TextMeshProUGUI panelText;
    public Camera mainCam;

    public bool inRange = false;
    public bool playerFrozen;
    public bool solved = false;

    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;

    Vector3 camPos;

    public GameObject charCon;
    //public Canvas canvas;

    private string fourDigitCode;
    // Start is called before the first frame update
    void Start()
    {
        panelText.text = string.Empty;
        fourDigitCode = "1957";
        //camPos = mainCam.transform.position;
    }


    
    // this script is set to active(false) initially and activated when the interaction button is pressed
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.I) && inRange && !solved && !GameObject.FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().inNoteRange)
        //if (inRange)
        {

                playerFrozen = true;


                //un-parent camera from player and set position
                mainCam.transform.SetParent(null);
                mainCam.transform.localPosition = new Vector3(3.3499999f, 1.7f, -7.5f);

            //Keypad.FindObjectOfType<Tooltip>().gameObject.SetActive(false);
            
            //GameObject.Find("TooltipManager").SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ResetPlayerAndCam();
        }
        HandleInput();
    }

    

    private void ResetPlayerAndCam()
    {
        ReactivateMovement();
        mainCam.transform.SetParent(charCon.transform);
        mainCam.transform.localPosition = new Vector3(-0.0189999994f, 1.42499995f, 0f);
        //reset bools to resume interaction
        //inRange = false;
        playerFrozen = false;
        //Keypad.FindObjectOfType<Tooltip>().gameObject.SetActive(true);
    }

    void ChangeColour()
    {
        panelText.color = Color.black;
    }

    private void ResetKeypad()
    {

        panelText.text = string.Empty;
        panelText.color = Color.black;
    }

    public void HandleInput()
    {
        
        if(playerFrozen && inRange)
        {
            //DeactivateMovement();

            if (Input.GetKeyDown(KeyCode.KeypadEnter) && panelText.text.Length == 4)
            {
                //code validation
                if(panelText.text == fourDigitCode)
                {
                    panelText.text = string.Empty;
                    panelText.color = Color.green;
                    panelText.text += "Success";
                    FindObjectOfType<AudioManager>().Play("KeypadSuccess");

                    //re-parent the camera to player and set position
                    ResetPlayerAndCam();

                    //disable possiblity of interaction if code is correct
                    //this.GetComponent<BoxCollider>().enabled = false;
                    GameObject.Find("DoorTrigger").SetActive(false);


                    //door animation
                    animator1.SetBool("isCodeRight", true);
                    animator2.SetBool("isCodeRight", true);
                    animator3.SetBool("isCodeRight", true);
                    animator4.SetBool("isCodeRight", true);

                    GameObject.Find("ExitText").SetActive(false);

                    //Destroy(GameObject.Find("TooltipBox"));
                    //this.GetComponent<Collider>().enabled = false;
                    GameObject.Find("KeypadStandard").GetComponent<Collider>().enabled = false;
                    //this.GetComponent <Collider>().enabled = false;
                    inRange = false;
                    solved = true;
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("KeypadDenied");
                    panelText.text = string.Empty;
                    panelText.color = Color.red;
                    panelText.text += "Denied";

                    Invoke("ResetKeypad", 1f);
                }

                
            }
            if (Input.GetKeyDown(KeyCode.KeypadPeriod) && panelText.text != "Denied")
            {
                if (panelText.text.Length == 0)
                {
                    return;
                }
                else
                {
                    string newText;
                    newText = panelText.text.Remove(panelText.text.Length - 1);
                    panelText.text = newText;
                }

                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (panelText.text.Length > 3)
            {
                return;
            }




            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                panelText.text += "1";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                panelText.text += "2";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                panelText.text += "3";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                panelText.text += "4";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                panelText.text += "5";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                panelText.text += "6";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                panelText.text += "7";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                panelText.text += "8";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                panelText.text += "9";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                panelText.text += "0";
                FindAnyObjectByType<AudioManager>().Play("ButtonClick");
            }



            DeactivateMovement();
        }
        

        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            inRange = true;

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            inRange = false;
    }

    public void DeactivateMovement()
    {
        FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().enabled = false;
    }

    public void ReactivateMovement()
    {
        FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().enabled = true;
    }
}
