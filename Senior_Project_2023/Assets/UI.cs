using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

// TO UPDATE THE VALUES OF THE TEXT OBJECTS IN THE UI IN ANOTHER SCRIPT, CREATE A FUNCTION THAT CHANGES THE VALUES
// AND CALL THE FUNCTION IN THAT OTHER SCRIPT

public class UI : MonoBehaviour
{
    // Reference to the canvas
    GameObject canvas;

    // References the powerup and bomb tab, the pictures for the instakill and double points
    // Powerups, as well as the pictures for the 2 grenades and 2 yarns
    GameObject powerupTab;
    GameObject ik_powerup;
    GameObject dp_powerup;
    GameObject BombTab;
    GameObject grenade1;
    GameObject grenade2;
    GameObject yarn1;
    GameObject yarn2;

    // References to the round, zombie count, points, and bullets text objects
    public Text Intro_1;
    public Text Intro_2;
    public Text round;
    public Text zombCount;
    public Text points;
    public Text bulletInClip;
    public Text bulletTotal;
    public Text errorMessages;
    public GameObject reloading;

    // Initializes the round, points and bullets
    byte roundCount = 1;
    ushort pointsCount = 500;
    byte bulletClipCount = 0;
    ushort bulletTotalCount = 0;
    float messageTimer = 0;
    bool showMessage = false;
    bool intro = true;
    float introTimer = 3.0f;

    // Initializes pause menu variables
    public static bool isPaused = false;
    GameObject pauseMenuImage;
    GameObject resumeBtn;
    GameObject mainMenuBtn;
    GameObject instructions;
    GameObject popupCanvas;

    // References to the game controller and player
    public MainController controller;
    public Player playerInfo;

    void Start()
    {
        // Initialize variables
        canvas = GameObject.Find("Canvas Full UI");

        powerupTab = canvas.transform.Find("Perk Tab").gameObject;
        BombTab = canvas.transform.Find("Item Tab").gameObject;
        BombTab.SetActive(false);

        ik_powerup = powerupTab.transform.Find("ik_powerup").gameObject;
        dp_powerup = powerupTab.transform.Find("dp_powerup").gameObject;
        ik_powerup.SetActive(false);
        dp_powerup.SetActive(false);

        grenade1 = BombTab.transform.Find("Grenade1").gameObject;
        grenade2 = BombTab.transform.Find("Grenade2").gameObject;
        yarn1 = BombTab.transform.Find("Yarn1").gameObject;
        yarn2 = BombTab.transform.Find("Yarn2").gameObject;

        reloading = canvas.transform.Find("Reloading").gameObject;
        Intro_1 = canvas.transform.Find("Intro Text").GetComponent<Text>();
        Intro_2 = canvas.transform.Find("Intro Text 2").GetComponent<Text>();

        pauseMenuImage = canvas.transform.Find("Pause Menu").gameObject;
        resumeBtn = canvas.transform.Find("Resume Button").gameObject;
        mainMenuBtn = canvas.transform.Find("Main Menu Button").gameObject;
        instructions = canvas.transform.Find("Instructions").gameObject;
        popupCanvas = transform.Find("/Popup_Canvas").gameObject;

        round.text = roundCount.ToString();
        zombCount.text = controller.zombiesLeft.ToString();
        points.text = pointsCount.ToString();
        bulletInClip.text = bulletClipCount.ToString();
        bulletTotal.text = bulletTotalCount.ToString();
        errorMessages.text = "";

        pauseMenuImage.SetActive(false);
        resumeBtn.SetActive(false);
        mainMenuBtn.SetActive(false);
        instructions.SetActive(false);
        
    }

    void Update()
    {
        // Always check for a change in the zombiesLeft variable in the game controller
        zombCount.text = controller.zombiesLeft.ToString();
        // If there are no more zombies in the round, increase the round in the ui
        if (controller.zombiesLeft == 0)
        {
            roundCount++;
            round.text =  roundCount.ToString();
        }
        // Control for the tab to see the amount of grenades and yarns the player has left to use
        if (Input.GetKeyDown(KeyCode.Tab))
            BombTab.SetActive(true);

        if (Input.GetKeyUp(KeyCode.Tab))
            BombTab.SetActive(false);

        // Timers for time controlled error and intro messages
        if (showMessage)
            messageTimer -= Time.deltaTime;

        if (intro)
            introTimer -= Time.deltaTime;
        
        // Turn off error and intro messages
        if (showMessage && messageTimer <= 0)
        {
            showMessage = false;
            errorMessages.text = "";
        }

        if (intro && introTimer <= 0)
        {
            Intro_1.text = "";
            Intro_2.text = "";

            Intro_1.gameObject.SetActive(false);
            Intro_2.gameObject.SetActive(false);
        }
    }

    public void Reloading(int progress) {
        switch (progress) {
            case 1:
                reloading.gameObject.SetActive(true);
                break;
            case 2:
                reloading.GetComponent<Text>().text = "Reloading..";
                break;
            case 3:
                reloading.GetComponent<Text>().text = "Reloading.";
                break;
            case 4:
                reloading.GetComponent<Text>().text = "Reloading...";
                reloading.gameObject.SetActive(false);
                break;
        }
    }

    // USE THIS FUNCTION WHEN WANTING TO UPDATE THE BULLETS IN THE UI
    public void Change(byte clipSize, ushort totalBullets)
    {
        bulletClipCount = clipSize;
        bulletTotalCount = totalBullets;

        bulletInClip.text = clipSize.ToString();
        bulletTotal.text = totalBullets.ToString();
    }
    // USE THIS FUNCTION WHEN WANTING TO UPDATE THE INSTAKILL POWERUP VISUAL
    public void instaKillActive(bool set)
    {
        ik_powerup.SetActive(set);
    }
    // USE THIS FUNCTION WHEN WANTING TO UPDATE THE DOUBLE POINTS POWERUP VISUAL
    public void douPoiActive(bool set)
    {
        dp_powerup.SetActive(set);
    }

    // USE THIS FUNCTION WHEN WANTING TO UPDATE THE GRENADES
    public void grenadeUI(bool add = false)
    {
        if (add)
        {
            if (!grenade1.activeSelf)   grenade1.SetActive(true);
            else    grenade2.SetActive(true);
        }
        else
        {
            if (grenade2.activeSelf)    grenade2.SetActive(false);
            else    grenade1.SetActive(false);
        }
    }
    // USE THIS FUNCTION WHEN WANTING TO UPDATE THE YARNS
    public void yarnUI(bool add = false)
    {
        if (add)
        {
            if (!yarn1.activeSelf)  yarn1.SetActive(true);
            else    yarn2.SetActive(true);
        }
        else
        {
            if(yarn2.activeSelf)    yarn2.SetActive(false);
            else    yarn1.SetActive(false);
        }
    }
    // USE THIS FUNCTION WHEN WANTING TO CHANGE THE ERROR MESSAGE
    public void changeMessage(string message)
    {
        messageTimer = 2.0f;
        showMessage = true;
        errorMessages.text = message;
    }

    public void Resume()
    {
        pauseMenuImage.SetActive(false);
        resumeBtn.SetActive(false);
        mainMenuBtn.SetActive(false);
        instructions.SetActive(false);
        popupCanvas.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuImage.SetActive(true);
        resumeBtn.SetActive(true);
        mainMenuBtn.SetActive(true);
        instructions.SetActive(true);
        popupCanvas.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
