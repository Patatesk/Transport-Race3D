using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiControl : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] ParticleSystem confetti;
    [SerializeField] Settings settings;


    [SerializeField] GameObject canvas;
    [SerializeField] GameObject whichvehicle;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject atvText;
    [SerializeField] GameObject carText;
    [SerializeField] GameObject busText;
    [SerializeField] GameObject taxiText;
    [SerializeField] GameObject scoterText;
    [SerializeField] GameObject airplaneText;
    [SerializeField] GameObject atvTextAnim;
    [SerializeField] GameObject carTextAnim;
    [SerializeField] GameObject busTextAnim;
    [SerializeField] GameObject taxiTextAnim;
    [SerializeField] GameObject airplaneTextAnim;
    [SerializeField] GameObject scooterTextAnim;
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;
    [SerializeField] GameObject level4;
    [SerializeField] GameObject level5;
    [SerializeField] Text levelText;


    [SerializeField] public Slider slider;
    [SerializeField] public TextMeshProUGUI crowdText;



    GameObject mainMenu;
    GameObject winPanel;
    GameObject losePanel;
    GameObject inGame;
    Player player;



    public float targetProgress;
    private float fillSpeed = 0.05f;







    void Start()
    {
        crowdText = crowdText.GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
        mainMenu = canvas.transform.GetChild(2).gameObject;
        inGame = canvas.transform.GetChild(3).gameObject;
        winPanel = canvas.transform.GetChild(4).gameObject;
        losePanel = canvas.transform.GetChild(5).gameObject;
    }

    void Update()
    {
        if (settings.level == 1)
        {
            level3.SetActive(false);
            level2.SetActive(false);
            level1.SetActive(true);
            level4.SetActive(false);
            level5.SetActive(false);
            levelText.text = "Level"+" "+"1";
        }

        if (settings.level == 2)
        {
            level1.SetActive(false);
            level3.SetActive(false);
            level2.SetActive(true);
            level4.SetActive(false);
            level5.SetActive(false);
            levelText.text = "Level" + " " + "2";

        }

        if (settings.level == 3)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
            level4.SetActive(false);
            level5.SetActive(false);
            levelText.text = "Level" + " " + "3";

        }

        if (settings.level == 4)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level5.SetActive(false);
            level4.SetActive(true);
            levelText.text = "Level" + " " + "4";

        }

        if (settings.level == 5)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(true);
            levelText.text = "Level" + " " + "5";


        }

        if (settings.level ==6)
        {
            settings.level = 1;
        }

        if (settings.isPlaying && !settings.GameOver)
        {
            mainMenu.SetActive(false);
            inGame.SetActive(true);
            winPanel.SetActive(false);
            losePanel.SetActive(false);

        }

        else if (settings.GameOver && !settings.isPlaying)
        {
            winPanel.SetActive(true);
            mainMenu.SetActive(false);
            inGame.SetActive(false);
            losePanel.SetActive(false);
        }

        else if (settings.GameStarted && !settings.isPlaying)
        {
            winPanel.SetActive(false);
            mainMenu.SetActive(false);
            inGame.SetActive(false);
            losePanel.SetActive(true);
        }

        if (slider.value < targetProgress)
        {
            slider.value = targetProgress;

        }

        else if (slider.value > targetProgress)
        {
            slider.value = targetProgress;

        }
    }
    public void ProgressBar(float increase)
    {

        targetProgress = slider.value + increase;
    }

    public void StartButton()
    {
        settings.GameOver = false;
        startButton.SetActive(false);
        settings.isPlaying = true;
        settings.GameStarted = true;
    }

    public void NextLevel()
    {
        settings.level++;
        levelText.text = "Level " + (settings.level + 1).ToString();
        SceneManager.LoadScene("SampleScene");
    }

    public void TryAgain()
    {
        levelText.text = "Level " + (settings.level + 1).ToString();
        SceneManager.LoadScene("SampleScene");
    }

    private void AtvText()
    {
        if (player.whichVehicle == 0)
        {
            atvText.SetActive(true);
            atvTextAnim.SetActive(false);
        }
    }

    private void CarText()
    {
        if (player.whichVehicle == 1)
        {
            carText.SetActive(true);
            carTextAnim.SetActive(false);
        }
    }

    private void TaxiText()
    {
        if (player.whichVehicle == 4)
        {
            taxiText.SetActive(true);
            taxiTextAnim.SetActive(false);
        }
    }

    private void BusText()
    {
        if (player.whichVehicle == 2)
        {
            busText.SetActive(true);
            busTextAnim.SetActive(false);
        }
    }

    private void AirplaneText()
    {
        if (player.whichVehicle == 3)
        {
            airplaneText.SetActive(true);
            airplaneTextAnim.SetActive(false);
        }
    }

    private void ScooterText()
    {
        scoterText.SetActive(true);
        scooterTextAnim.SetActive(false);

    }

    public void CarTextEffect()
    {
        if (player.whichVehicle == 0)
        {
            if (settings.isPlaying)
            {
                atvTextAnim.SetActive(true);
                Invoke("AtvText", 0.3f);
                carText.SetActive(false);
                busText.SetActive(false);
                airplaneText.SetActive(false);
                taxiText.SetActive(false);
                scoterText.SetActive(false);

            }
        }

        if (player.whichVehicle == 1)
        {
            carTextAnim.SetActive(true);
            taxiText.SetActive(false);
            atvText.SetActive(false);
            Invoke("CarText", 0.3f);
            busText.SetActive(false);
            airplaneText.SetActive(false);
            scoterText.SetActive(false);

        }

        if (player.whichVehicle == 2)
        {
            busTextAnim.SetActive(true);
            atvText.SetActive(false);
            carText.SetActive(false);
            Invoke("BusText", 0.3f);
            airplaneText.SetActive(false);
            taxiText.SetActive(false);
            scoterText.SetActive(false);


        }

        if (player.whichVehicle == 3)
        {
            airplaneTextAnim.SetActive(true);
            atvText.SetActive(false);
            carText.SetActive(false);
            busText.SetActive(false);
            Invoke("AirplaneText", 0.3f);
            taxiText.SetActive(false);
            scoterText.SetActive(false);

            //slider.gameObject.SetActive(false);

            slider.transform.localPosition = new Vector3(10, 450, 315);
        }

        if (player.whichVehicle == 4)
        {
            taxiTextAnim.SetActive(true);
            atvText.SetActive(false);
            carText.SetActive(false);
            busText.SetActive(false);
            airplaneText.SetActive(false);
            Invoke("TaxiText", 0.3f);
            scoterText.SetActive(false);


        }

        if (player.whichVehicle == 5)
        {
            scooterTextAnim.SetActive(true);
            atvText.SetActive(false);
            carText.SetActive(false);
            busText.SetActive(false);
            airplaneText.SetActive(false);
            taxiText.SetActive(false);
            Invoke("scoterText", 0.3f);

        }



    }




}
