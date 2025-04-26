using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public GameObject gun;
    public GameObject gun_template;
    public GameObject int_template;
    public bool player_detection = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.E))
        {
            gun.SetActive(false);
            gun_template.SetActive(true);
            int_template.SetActive(false);
            CarManNPC.dialogueLines.Clear();
            CarManNPC.dialogueLines.Add("\"What?! That's not possible!\"");
            CarManNPC.dialogueLines.Add("\"Where did you find that gun?\"");
            CarManNPC.dialogueLines.Add("\"Yes, it's mine. Although it doesn't matter where or how you found it, I haven't used it to kill anyone!\"");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "PlayerBody")
        {
            player_detection = true;
            int_template.SetActive(true);
        }
    }

    private void OnTriggerExit()
    {
        player_detection = false;
        int_template.SetActive(false);
    }
}
