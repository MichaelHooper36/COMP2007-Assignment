using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using System.ComponentModel.Design;

public class Knife : MonoBehaviour
{
    public GameObject knife;
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
            knife.SetActive(false);
            foreach (Transform child in item_list)
            {
                TextMeshProUGUI textComp = child.GetComponent<TextMeshProUGUI>();
                if (textComp != null && string.IsNullOrEmpty(textComp.text))
                {
                    textComp.text = "Knife";
                    break;
                }
            }
            if (!tool_bar.activeInHierarchy)
            {
                tool_bar.SetActive(true);
                Menu.tool_bar_active = true;
            }
            int_template.SetActive(false);
            Menu.knife_obtained = true;
            KnifeNPC.dialogueLines.Clear();
            KnifeNPC.dialogueLines.Add("\"You managed to find my conveniently placed kitchen knife.\"");
            KnifeNPC.dialogueLines.Add("\"Thank you, I've been looking everywhere for it!\"");
            KnifeNPC.dialogueLines.Add("...");
            KnifeNPC.dialogueLines.Add("\"What do you mean \'it was found next to the victim?\'\"");
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
