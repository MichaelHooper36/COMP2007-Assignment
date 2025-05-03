using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject solve_menu;
    public GameObject settings_menu;
    public GameObject tutorial_menu;
    public GameObject confirm_menu;
    public GameObject verdict_menu;

    public GameObject menu_reminder;
    public GameObject current_suspect;
    public GameObject suspect_text;

    public static bool tutorial_on = true;
    public static bool menu_on = false;
    public static bool gun_obtained = false;
    public static bool knife_obtained = false;
    public static int ending = 0;

    public string final_verdict = "";
    // Start is called before the first frame update
    void Start()
    {
        FPSController.canMove = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tutorial_menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial_on && Input.GetKey(KeyCode.Return))
        {
            tutorial_on = false;
            tutorial_menu.SetActive(false);
            FPSController.canMove = true;
        }

        if (!menu_on && Input.GetKey(KeyCode.Escape))
        {
            menu_on = true;
            FPSController.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menu_reminder.SetActive(false);
            main_menu.SetActive(true);
        }
    }

    public void SolveButton()
    {
        main_menu.SetActive(false);
        solve_menu.SetActive(true);
    }

    public void ResumeButton()
    {
        if (main_menu != null)
        {
            menu_on = false;
            main_menu.SetActive(false);
            menu_reminder.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FPSController.canMove = true;
        }
    }

    public void OptionsButton()
    {
        main_menu.SetActive(false);
        settings_menu.SetActive(true);
    }

    public void QuitButton()
    {
        if (main_menu != null)
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }   
    }

    public void SuspectButton()
    {
        string button_name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (button_name == "Suspect Button 1")
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: Ryan Gosling";
            ending = 1;
        }
        else if (button_name == "Suspect Button 2")
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: Jack D. Ripper";
            ending = 2;
        }
        else if (button_name == "Suspect Button 3")
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: Harry Houdini";
            ending = 3;
        }
        else if (button_name == "Suspect Button 4")
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: You";
            ending = 4;
        }
    }

    public void AccuseSuspect()
    {
        confirm_menu.SetActive(true);
    }

    public void BackFromSuspect()
    {
        if (confirm_menu.activeInHierarchy)
        {
            confirm_menu.SetActive(false);
        }
        solve_menu.SetActive(false);
        main_menu.SetActive(true);
    }

    public void BackFromSettings()
    {
        settings_menu.SetActive(false);
        main_menu.SetActive(true);
    }

    public void ConfirmButton()
    {
        switch(ending)
        {
            case 1:
                final_verdict = "Ryan Gosling was not the killer";
                break;
            case 2:
                final_verdict = "Jack D. Ripper was not the killer";
                break;
            case 3:
                final_verdict = "Harry Houdini was the killer";
                break;
            case 4:
                final_verdict = "You were not the killer";
                break;
        }

        suspect_text.GetComponent<TextMeshProUGUI>().text = final_verdict;

        confirm_menu.SetActive(false);
        solve_menu.SetActive(false);
        verdict_menu.SetActive(true);
    }

    public void CancelButton()
    {
        confirm_menu.SetActive(false);
    }
}
