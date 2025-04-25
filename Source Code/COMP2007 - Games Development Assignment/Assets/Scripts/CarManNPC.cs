using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class CarManNPC : MonoBehaviour
{
    public GameObject d_template;
    public GameObject canvas;
    bool playerDetection = false;

    public static List<string> dialogueLines = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueLines.Add("It works!");
        dialogueLines.Add("It actually works!");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection && Input.GetKeyDown(KeyCode.E) && !FPSController.dialogue)
        {
            canvas.SetActive(true);
            FPSController.dialogue = true;
            for (int i = 0; i < dialogueLines.Count; i++)
            {
                NewDialogue(dialogueLines[i]);
            }
            canvas.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void NewDialogue(string text)
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.SetParent(canvas.transform, false);
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "PlayerBody")
            playerDetection = true;
    }
    private void OnTriggerExit()
    {
        playerDetection = false;
    }
}
