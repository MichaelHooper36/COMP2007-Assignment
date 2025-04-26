using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictimNPC : MonoBehaviour
{
    public GameObject d_template;
    public GameObject d_text;
    public GameObject int_template;
    public bool player_detection = false;
    int dialogue_lines = 0;

    public static List<string> dialogueLines = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        int_template.SetActive(false);
        if (dialogueLines.Count == 0)
        {
            dialogueLines.Add("The victim lies cold in front of car...");
            dialogueLines.Add("No wounds, except from a hole in his stomache...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.E) && !FPSController.dialogue)
        {
            int_template.SetActive(false);
            d_template.SetActive(true);
            FPSController.dialogue = true;
            FPSController.canMove = false;
        }

        if (player_detection && FPSController.dialogue)
        {
            d_text.GetComponent<TextMeshProUGUI>().text = dialogueLines[dialogue_lines];
        }

        if (player_detection && FPSController.dialogue && Input.GetKeyDown(KeyCode.Return))
        {
            dialogue_lines++;
            if (dialogue_lines >= dialogueLines.Count)
            {
                dialogue_lines = 0;
                FPSController.dialogue = false;
                FPSController.canMove = true;
                d_template.SetActive(false);
                int_template.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "PlayerBody")
            player_detection = true;
        int_template.SetActive(true);
    }
    private void OnTriggerExit()
    {
        player_detection = false;
        int_template.SetActive(false);
    }
}
