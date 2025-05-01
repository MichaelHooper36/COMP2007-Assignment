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

    public GameObject menu_reminder;
    public GameObject current_suspect;

    public static bool gun_obtained = false;
    public static bool knife_obtained = false;
    public static int ending = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
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

    public void ConfirmSuspicion()
    {

    }

    public void BackFromSuspect()
    {
        solve_menu.SetActive(false);
        main_menu.SetActive(true);
    }

    public void BackFromSettings()
    {
        settings_menu.SetActive(false);
        main_menu.SetActive(true);
    }
}
