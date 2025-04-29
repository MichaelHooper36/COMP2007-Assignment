using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject solve_menu;
    public GameObject menu_reminder;

    public static bool gun_obtained = false;
    public static bool knife_obtained = false;
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
        switch(button_name)
            case string
    }

    public void ConfirmSuspicion()
    {

    }

    public void BackFromSuspect()
    {
        solve_menu.SetActive(false);
        main_menu.SetActive(true);
    }
}
