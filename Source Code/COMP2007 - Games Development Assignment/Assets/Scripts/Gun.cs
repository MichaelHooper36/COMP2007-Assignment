using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel.Design;

public class Gun : MonoBehaviour
{
    public GameObject gun;
    public GameObject int_template;
    public GameObject tool_bar;
    public GameObject game_holder;
    public bool player_detection = false;

    List<Transform> item_list = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        item_list = new List<Transform>(game_holder.GetComponentsInChildren<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.E))
        {
            gun.SetActive(false);
            foreach (Transform child in item_list)
            {
                TextMeshProUGUI textComp = child.GetComponent<TextMeshProUGUI>();
                if (textComp != null && string.IsNullOrEmpty(textComp.text))
                {
                    textComp.text = "Gun";
                    break;
                }
            }
            if (!tool_bar.activeInHierarchy)
            {
                tool_bar.SetActive(true);
            }
            int_template.SetActive(false);
            Menu.gun_obtained = true;
            CarManNPC.dialogueLines.Clear();
            CarManNPC.dialogueLines.Add("\"What?! That's not possible!\"");
            CarManNPC.dialogueLines.Add("\"Where did you find that gun?\"");
            CarManNPC.dialogueLines.Add("\"Yes, it's mine. It doesn't matter where or how you found it, though. I haven't used it to kill anyone!\"");
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
