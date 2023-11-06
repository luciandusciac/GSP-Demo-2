using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ClickObjects : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 size;

    [Header("Text")]
    public TextMeshProUGUI panelText;
    public TextMeshProUGUI exitText;

    public GameObject charCon;
    public Camera mainCam;


    public bool inRange;
    public bool playerFrozen;
    private bool pickedUpNote;

    [Header("Animation")]
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;

    public GameObject inventory;

    [Header("Notes")]
    public RawImage helpImage;
    public RawImage wokeUp;
    public RawImage securityBriefing;

    private RawImage currentImage;
    private GameObject currentImg;

    private string fourDigitCode;
    private bool inventoryActive = false;
    //public static InventorySystem inventorySystem;

    public TextMeshProUGUI noteText1;

    private void Start()
    {
        fourDigitCode = "1957";
        //noteText1.enabled = false;
        //inventorySystem = GetComponent<InventorySystem>();
    }


    void Update()
    {
        //check if can toggle inventory first
        ToggleInventory();

        if (Input.GetKeyDown(KeyCode.I) && inRange && !GameObject.FindObjectOfType<Keypad>().solved && !GameObject.FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().inNoteRange)
        {
            playerFrozen = true;


            //un-parent camera from player and set position
            mainCam.transform.SetParent(null);
            mainCam.transform.localPosition = Vector3.MoveTowards(mainCam.transform.localPosition, new Vector3(3.3499999f, 1.7f, -7.5f), 5);
            mainCam.transform.LookAt(GameObject.Find("KeypadStandard").transform.position);
            exitText.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentImg.GetComponent<TextMeshProUGUI>().enabled = true;
            //GameObject.Find("NoteText").SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ResetPlayerAndCam();
            ClearNote();

        }
        
        PressButton();



    }

    private void ResetSize()
    {
        hit.collider.transform.localScale = size;
    }


    void ClearNote()
    {
        if(pickedUpNote)
        {
            currentImage.gameObject.SetActive(false);
            currentImg.gameObject.SetActive(true);
            mainCam.GetComponent<LookAround>().enabled = true;
            exitText.gameObject.SetActive(false);
            pickedUpNote = false;
            //currentImage.GetComponent<NoteText>().enabled = false;
        }
        
    }

    void DisplayNote()
    {
        hit.collider.gameObject.SetActive(false);

        if (hit.collider.gameObject.name == "HelpMe")
        {
            
            currentImage = helpImage;
            //currentImg = hit.collider.gameObject;
        }
        else if(hit.collider.gameObject.name == "WokeUp")
        {
            currentImage = wokeUp;
            //if (Input.GetKeyDown(KeyCode.T))
            //{
            //    noteText1.enabled = true;
            //}
            //hit.collider.gameObject.GetComponent<NoteText>().enabled = true;
            //currentImage.GetComponent<NoteText>().enabled = true;
        }
        else if (hit.collider.gameObject.name == "SecurityBriefing")
        {
            currentImage = securityBriefing;
        }
        currentImage.gameObject.SetActive(true);
        FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().enabled = false;
        mainCam.GetComponent<LookAround>().enabled = false;

        exitText.gameObject.SetActive(true);

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    currentImg.GetComponent<TextMeshProUGUI>().enabled = true;
        //}
    }

    void PressButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                //if (Input.GetMouseButtonDown(0))
                //{
                //note input
                if (hit.collider.CompareTag("Note") && inRange)
                {
                    pickedUpNote = true;
                    currentImg = hit.collider.gameObject;
                    DisplayNote();

                }

                if (hit.collider.CompareTag("Pickup"))
                {
                    //InventorySystem.instance.InitializeInventory();
                }



                //keypad button input
                if (hit.collider.CompareTag("Button"))
                {
                    if (playerFrozen && inRange)
                    {

                        FindObjectOfType<CharacterController>().GetComponent<PlayerMovement>().enabled = false;


                        if (hit.collider.name == "bttnEnter" && panelText.text.Length == 4)
                        {
                            //code validation
                            if (panelText.text == fourDigitCode)
                            {
                                panelText.text = string.Empty;
                                panelText.color = Color.green;
                                panelText.text += "Success";
                                FindObjectOfType<AudioManager>().Play("KeypadSuccess");

                                //re - parent the camera to player and set position
                                ResetPlayerAndCam();


                                //disable possiblity of interaction if code is correct
                                FindObjectOfType<Keypad>().GetComponent<Collider>().enabled = false;
                                GameObject.Find("DoorTrigger").SetActive(false);


                                //door animation
                                animator1.SetBool("isCodeRight", true);
                                animator2.SetBool("isCodeRight", true);
                                animator3.SetBool("isCodeRight", true);
                                animator4.SetBool("isCodeRight", true);


                                //Destroy(GameObject.Find("TooltipBox"));
                                GameObject.Find("KeypadStandard").GetComponent<Collider>().enabled = false;
                                inRange = false;
                                GameObject.FindObjectOfType<Keypad>().solved = true;
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
                        if (hit.collider.name == "bttnDelete" && panelText.text != "Denied")
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
                        }
                        if (panelText.text.Length > 3)
                        {
                            return;
                        }
                        if (hit.collider.name == "bttn1")
                        {
                            panelText.text += "1";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn2")
                        {
                            panelText.text += "2";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn3")
                        {
                            panelText.text += "3";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn4")
                        {
                            panelText.text += "4";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn5")
                        {
                            panelText.text += "5";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn6")
                        {
                            panelText.text += "6";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn7")
                        {
                            panelText.text += "7";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn8")
                        {
                            panelText.text += "8";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn9")
                        {
                            panelText.text += "9";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }
                        if (hit.collider.name == "bttn0")
                        {
                            panelText.text += "0";
                            //FindAnyObjectByType<AudioManager>().Play("ButtonClick");
                        }

                        size = hit.collider.transform.localScale;
                        hit.collider.transform.localScale *= 0.7f;
                        //hit.transform.localScale *= 0.9f;
                        Invoke("ResetSize", 0.01f);
                        FindObjectOfType<AudioManager>().Play("ButtonClick");




                    }
                }

                //}
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
            inRange = true;


    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
            inRange = false;
    }

    private void ResetKeypad()
    {
        
        panelText.text = string.Empty;
        panelText.color = Color.black;
    }

    private void ResetPlayerAndCam()
    {
        FindObjectOfType<Keypad>().ReactivateMovement();
        mainCam.transform.SetParent(charCon.transform);
        mainCam.transform.localPosition = new Vector3(-0.0189999994f, 1.42499995f, 0f);
        //inRange = false;
        playerFrozen = false;
        exitText.gameObject.SetActive(false);
    }

    private void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventoryActive)
            {
                //InventorySystem.instance.ListItems();
                inventory.SetActive(false);
                inventoryActive = false;
                charCon.GetComponent<PlayerMovement>().enabled = true;
                mainCam.GetComponent<LookAround>().enabled = true;
            }
            else
            {
                inventory.SetActive(true);
                inventoryActive = true;
                charCon.GetComponent<PlayerMovement>().enabled = false;
                mainCam.GetComponent<LookAround>().enabled = false;
                //InventorySystem.instance.InitializeInventory();
            }
            //playerFrozen = true;


        }
    }

}
