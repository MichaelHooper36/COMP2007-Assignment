using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#else
using System.Diagnostics;
using System.IO;
#endif

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip button_sound;

    public GameObject the_menus;
    public GameObject main_menu;
    public GameObject solve_menu;
    public GameObject settings_menu;
    public GameObject tutorial_menu;
    public GameObject confirm_menu;
    public GameObject verdict_menu;
    public GameObject eviction_notice;

    public GameObject menu_reminder;
    public GameObject current_suspect;
    public GameObject suspect_text;

    public GameObject suspect_button_1;
    public GameObject suspect_button_2;
    public GameObject suspect_button_3;
    public GameObject suspect_button_4;

    public static bool tutorial_on = true;
    public static bool menu_on = false;
    public static bool notice_on = false;

    public static bool gun_obtained = false;
    public static bool knife_obtained = false;
    public static bool letter_obtained = false;
    public static int ending = 0;

    public string final_verdict = "";
    List<Transform> menu_list = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        menu_list = new List<Transform>(the_menus.GetComponentsInChildren<Transform>(true).Where(t => t != the_menus.transform));

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

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (menu_on)
            {
                ResumeButton();
            }
            else
            {
                menu_on = true;
                FPSController.dialogue = true;
                FPSController.canMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                menu_reminder.SetActive(false);
                main_menu.SetActive(true);
            }
        }

        if(notice_on && letter_obtained && Input.GetKey(KeyCode.Return))
        {
            eviction_notice.SetActive(false);
            notice_on = false;
        }
    }

    public void OnClick()
    {
        float sound_volume = Mathf.Clamp01(PlayerPrefs.GetFloat("soundVolume", 0.5f));
        FPSController fps_controller = FindObjectOfType<FPSController>();
        AudioSource.PlayClipAtPoint(button_sound, fps_controller.transform.position, sound_volume);
    }

    public void SolveButton()
    {
        main_menu.SetActive(false);
        solve_menu.SetActive(true);
    }

    public void ResumeButton()
    {
        if (menu_on)
        {

            bool a_child_is_alive = false;
            foreach (Transform child in menu_list)
            {
                if (child.gameObject.activeInHierarchy && !main_menu.GetComponentsInChildren<Transform>(true).Contains(child))
                {
                    a_child_is_alive = true;
                    break;
                }
            }
            print(a_child_is_alive);

            if (!a_child_is_alive)
            {
                menu_on = false;
                FPSController.dialogue = false;
                main_menu.SetActive(false);
                menu_reminder.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            FPSController.canMove = !a_child_is_alive;
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
            UnityEngine.Application.Quit();
            #endif
        }   
    }

    public void SuspectButton()
    {
        GameObject selected_suspect = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (selected_suspect == suspect_button_1)
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: Ryan Gosling";
            ending = 1;
        }
        else if (selected_suspect == suspect_button_2)
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: Jack D. Ripper";
            ending = 2;
        }
        else if (selected_suspect == suspect_button_3)
        {
            if (letter_obtained == true)
            {
                current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: Harry Houdini";
            }
            else
            {
                current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: ???";
            }
                ending = 3;
        }
        else if (selected_suspect == suspect_button_4)
        {
            current_suspect.GetComponent<TextMeshProUGUI>().text = "Current suspect: You";
            ending = 4;
        }
    }

    public void AccuseSuspect()
    {
        suspect_button_1.GetComponent<Button>().interactable = false;
        suspect_button_2.GetComponent<Button>().interactable = false;
        suspect_button_3.GetComponent<Button>().interactable = false;
        suspect_button_4.GetComponent<Button>().interactable = false;
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
                final_verdict = "While Ryan Gosling was correct in his statement that he crashed his car after the incident had occured, the fact that his discarded pistol had not been used to kill anyone was a lie! " +
                    "\n Upon being taken to the police station, Ryan confessed out of guilt to the murder of a separate person on the other side of the city, confirming his alibi in relation to Harry Houdini's murder." +
                    "\n You may have failed to apprehend to the murderer attached to your case, but you succeeded in catching a killer nonetheless." +
                    "\n\n Thank you for playing!";
                break;
            case 2:
                final_verdict = "Despite what the legends may tell you, Jack D. Ripper is a truly innocent man!" +
                    "\n It seems that if you end up in the wrong place at the wrong time on enough occasions, stories can spread like wildfire." +
                    "\n If only we knew how he managed to misplace his knife..." +
                    "\n\n Thank you for playing!";
                break;
            case 3:
                final_verdict = "Following his monumental career as an escape artist, Harry Houdini ran into a series of unfortunate events." +
                    "\n After an accident on the stage, an extreme leg injury placed Harry into early retirement. Without even a cent to his name, Harry Houdini was evicted from his childhood home and was soon to lose everything." +
                    "\n In a final act of desperation, he stole Jack D. Ripper's lucky dagger and stabbed himself, attempting to frame the theater technician in the process. If it wasn't for the foolish actions of Ryan Gosling, he would have gotten away with it, too." +
                    "\n It seemed that even with only one working leg, he was still able to escape from his greatest cage of all: " +
                    "\n This big ball of dirt that we call Earth..." +
                    "\n\n Thank you for playing!";
                break;
            case 4:
                final_verdict = "Despite having a sufficient alibi, the ability to determine the actual killer, and all of the necessary information at your disposal, you decide that only one person could perform such a heinous crime... yourself!" +
                    "\n Upon returning to the police station, you confess to the crime you most certainly did not commit and choose to rot away in a cell for the rest of your days." +
                    "\n Was this to repent for the crimes of your past, pure stupidity, or something else entirely? Only you will know the answer, while onlookers tell the tale of the detective that failed to solve the murder... of Harry Houdini." +
                    "\n\n Thanks for playing.";
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

    public void EvictionButton()
    {
        notice_on = true;
        eviction_notice.SetActive(true);
    }
}
