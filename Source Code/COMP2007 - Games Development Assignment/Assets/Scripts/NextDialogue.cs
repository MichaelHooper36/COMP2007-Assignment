using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    int index = CarManNPC.dialogueLines.Count;
    GameObject currentLine = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (transform.childCount > 2)
        {
            currentLine = transform.GetChild(2).gameObject;
            currentLine.SetActive(true);
            index = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && transform.childCount > 1)
        {
            if (FPSController.dialogue)
            {
                transform.GetChild(index).gameObject.SetActive(true);
                index++;
                if(transform.childCount == index)
                {
                    index = CarManNPC.dialogueLines.Count;
                    FPSController.dialogue = false;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
