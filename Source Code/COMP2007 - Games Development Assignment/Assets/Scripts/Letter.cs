using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

public class Letter : MonoBehaviour
{
    public GameObject letter;

    public GameObject int_template;
    public GameObject tool_bar;
    public GameObject eviction_button;
    public GameObject game_holder;
    public GameObject eviction_notice;

    public GameObject suspect_name;

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
            FPSController.canMove = false;
            FPSController.dialogue = true;

            int_template.SetActive(false);
            Menu.letter_obtained = true;
            eviction_notice.SetActive(true);
        }

        if (Menu.letter_obtained == true && Input.GetKey(KeyCode.Return))
        {
            foreach (Transform child in item_list)
            {
                TextMeshProUGUI textComp = child.GetComponent<TextMeshProUGUI>();
                if (textComp != null && string.IsNullOrEmpty(textComp.text))
                {
                    textComp.text = "Eviction Notice";
                    break;
                }
            }
            if (!tool_bar.activeInHierarchy)
            {
                tool_bar.SetActive(true);
                Menu.tool_bar_active = true;
            }

            eviction_button.SetActive(true);
            suspect_name.GetComponent<TextMeshProUGUI>().text = "Harry Houdini";
            eviction_notice.SetActive(false);
            FPSController.dialogue = false;
            FPSController.canMove = true;
            letter.SetActive(false);
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
