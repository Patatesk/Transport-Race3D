using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject carEffect;
    [SerializeField] GameObject Particul;
    [SerializeField] private Camera ortho;
    [SerializeField] Settings settings;
    [SerializeField] UiControl ui;
    [SerializeField] ParticleSystem confetti;
    [SerializeField] private Animation animantion;
    [SerializeField] private GameObject skidATV;
    [SerializeField] private GameObject skidTaxi;
    [SerializeField] private GameObject skidJeep;
    [SerializeField] private GameObject skidBus;
    [SerializeField] private GameObject skidMonster;
    [SerializeField] private GameObject spawnDoll;
    [SerializeField] private GameObject spawnTransform;
    [SerializeField] private GameObject spawnTransform2;
    Rigidbody rb;

    private Vector3 mousePos;
    private Vector3 firstPos;
    private bool changeCarUp;
    private bool changeCarDown;
    private int currentCar;
    private float turnAngle;
    private float turnSpeed = 4;
    private int numberOfCrowds = 10;
    private BoxCollider BoxCollider;
    private int currentCrowd;
    private Coroutine Stop;
    private Coroutine Stop2;
    private bool AþaðýB;
    private bool YukarýB;
    private bool dontChange = true;


    public Vector3 mouseDif;
    public int whichVehicle=1;

    int twoToOne;


    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        settings.GameOver = false;
        settings.isPlaying = false;
        anim = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider>();
        settings.GameStarted = false;
        ui.crowdText.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //settings.isPlaying = true;
        }

        turnAngle = mouseDif.x * 3;

        if (turnAngle < 0)
        {
            turnAngle = -1 * turnAngle;
        }

        if (settings.isPlaying)
        {

            Move();


            //if (whichVehicle == 3)
            //{
            //    MoveVertical();

            //}

            firstPos = Vector3.Lerp(firstPos, mousePos, 0.1f);

            if (Input.GetMouseButtonDown(0))
                MouseDown(Input.mousePosition);

            else if (Input.GetMouseButtonUp(0))
                MouseUp();

            else if (Input.GetMouseButton(0))
                MouseHold(Input.mousePosition);
        }

        if (mouseDif.x < 0)
        {


            TurnLeft();
        }

        if (mouseDif.x > 0)
        {


            TurnRight();
        }

        if (mouseDif.x == 0 || transform.position.x <= -4 || transform.position.x >= 4)
        {
            GetBackRotation();
        }

        if (mouseDif.x > 10f)
        {
            SkidSetActive(whichVehicle);

        }

        else if (mouseDif.x < -10f)
        {
            SkidSetActive(whichVehicle);

        }

        else
        {
            SkidSetActiveFalse();
        }

        GameOver();

        if (numberOfCrowds < 3 && dontChange)
        {
            for (int i = 0; i < gameObject.transform.GetChild(2).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(2).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(4).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }


            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);

            whichVehicle = 5;

            for (int i = 0; i < numberOfCrowds; i++)
            {
                gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(true);
            }



        }

        if (whichVehicle == 3)
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        else if (whichVehicle != 3)
        {
            GetComponent<BoxCollider>().enabled = true;

        }

    }

    void Move()
    {
        if (settings.isPlaying)
        {
            if (whichVehicle != 3)
            {
                float xPos = Mathf.Clamp(transform.position.x, -4.5f, 4.5f);
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
                rb.velocity = new Vector3(mouseDif.x, rb.velocity.y, 1 * settings.speed);
            }
            else if (whichVehicle == 3)
            {
                float xPos = Mathf.Clamp(transform.position.x, -3.5f, 3.5f);
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
                rb.velocity = new Vector3(mouseDif.x, rb.velocity.y, 1 * settings.speed);
            }
            
        }
    }

    private void MouseDown(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        firstPos = mousePos;
    }

    private void MouseHold(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        mouseDif = mousePos - firstPos;
        mouseDif *= settings.sensitivity;
    }

    private void MouseUp()
    {
        mouseDif = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_STICK))
        {
            currentCrowd++;

            other.gameObject.SetActive(false);

            SetActiveTrue();

            settings.points += 5;

            SetProgressBar();

            CrowdText();
        }

        if (other.CompareTag(StringClass.TAG_OPSTACLE))
        {
            //Dikenliyol
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            SetActiveFalse(1);

            transform.GetChild(whichVehicle).DOShakeRotation(1.5f, 5, 10, 90);

            SetProgressBarNegative(-1);

            InstantiateDoll(1);
        }

        if (other.CompareTag(StringClass.TAG_OPSTACLE1))
        {
            //kaygan yol
            GetBackRotation();

            SetActiveFalse(1);

            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            transform.GetChild(whichVehicle).DOShakeRotation(0.5f, new Vector3(0, 10, 0), 5, 90);

            SetProgressBarNegative(-1);

            InstantiateDoll(1);

        }

        if (other.CompareTag(StringClass.TAG_OPSTACLE2))
        {
            //huni
            GetBackRotation();

            SetActiveFalse(2);

            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            transform.GetChild(whichVehicle).DOShakeRotation(0.25f, new Vector3(0, 25, 5), 10);

            StartCoroutine(DestroyObj(other.gameObject));

            SetProgressBarNegative(-2);

            InstantiateDoll(2);

        }

        if (other.CompareTag(StringClass.TAG_OPSTACLE3))
        {
            //bariyer
            GetBackRotation();

            SetActiveFalse(3);
            other.gameObject.AddComponent<Rigidbody>();

            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2, 2), Random.Range(7.5f, 12.5f), Random.Range(10f, 17.5f)), ForceMode.Impulse);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            transform.GetChild(whichVehicle).DOShakeRotation(0.25f, new Vector3(0, 25, 5), 10);

            StartCoroutine(DestroyObj(other.gameObject));

            SetProgressBarNegative(-2);

            InstantiateDoll(3);

        }

        if (other.CompareTag(StringClass.TAG_GAMEOVER))
        {
            settings.isPlaying = false;

            rb.isKinematic = true;

            settings.GameOver = true;

            settings.GameStarted = false;

            confetti.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            confetti.gameObject.SetActive(true);
        }

        if (other.CompareTag(StringClass.TAG_COIN))
        {

            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            other.gameObject.transform.GetChild(0).parent = null;

            Destroy(other.gameObject);

            settings.points += 10;

        }

        if (other.CompareTag(StringClass.TAG_HUNI))
        {
            other.gameObject.AddComponent<Rigidbody>();

            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2, 2), Random.Range(7.5f, 12.5f), Random.Range(10f, 17.5f)), ForceMode.Impulse);
        }

        if (other.CompareTag(StringClass.TAG_BARIYER))
        {
            //bariyer
            GetBackRotation();

            SetActiveFalse(3);

            transform.GetChild(whichVehicle).DOShakeRotation(0.25f, new Vector3(0, 25, 5), 10);

            StartCoroutine(DestroyObj(other.gameObject));

            SetProgressBarNegative(-2);

            InstantiateDoll(3);

        }

        if (other.CompareTag(StringClass.TAG_FINISH1))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            dontChange = false;
            //SetActiveFalse(1);
            SetActiveFinishLine(1, 0, 2);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            ui.slider.gameObject.SetActive(false);
        }

        if (other.CompareTag(StringClass.TAG_FINISH2))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            //SetActiveFalse(2);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Finish");
            SetActiveFinishLine(3, 1, 3);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }

        if (other.CompareTag(StringClass.TAG_FINISH3))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            //SetActiveFalse(2);
            SetActiveFinishLine(6, 3, 4);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (other.CompareTag(StringClass.TAG_FINISH4))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            //SetActiveFalse(3);
            SetActiveFinishLine(10, 6, 5);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }

        if (other.CompareTag(StringClass.TAG_FINISH5))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            SetActiveFinishLine(15, 10, 6);
            //SetActiveFalse(4);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }

        if (other.CompareTag(StringClass.TAG_FINISH6))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            SetActiveFinishLine(21, 15, 7);
            //SetActiveFalse(6);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }

        if (other.CompareTag(StringClass.TAG_FINISH7))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            SetActiveFinishLine(28, 21, 8);
            //SetActiveFalse(7);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }

        if (other.CompareTag(StringClass.TAG_FINISH8))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            SetActiveFalse(7);
            SetActiveFinishLine(36, 28, 9);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (other.CompareTag(StringClass.TAG_FINISH9))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            //SetActiveFalse(8);
            SetActiveFinishLine(45, 36, 10);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (other.CompareTag(StringClass.TAG_FINISH10))
        {
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }
            //SetActiveFalse(9);
            SetActiveFinishLine(55, 45, 0);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (numberOfCrowds <= 0)
            {
                settings.GameOver = true;
                settings.isPlaying = false;
            }

        }
    }

    private IEnumerator DestroyObj(GameObject other)
    {
        yield return new WaitForSeconds(3);
        Destroy(other.gameObject);
    }

    private void SetActiveFalse(int decrease)
    {
        if (dontChange)
        {
            ChangeVehicle();

        }

        int holdNumberOfCrowds = numberOfCrowds;
        numberOfCrowds -= decrease;

        for (int i = 1; i <= holdNumberOfCrowds - numberOfCrowds; i++)
        {
            gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetActiveTrue()
    {
        if (dontChange)
        {
            ChangeVehicle();
        }

        twoToOne++;

        if (twoToOne == 2)
        {
            twoToOne = 0;
            numberOfCrowds++;
        }

        if (dontChange)
        {
            ChangeVehicle();

        }

        if (transform.GetChild(whichVehicle).GetChild(0).childCount >= numberOfCrowds)
        {
            gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(numberOfCrowds).gameObject.SetActive(true);
        }
    }

    private void ChangeVehicle()
    {
        currentCar = whichVehicle;

        if (numberOfCrowds <= 2)
        {
            for (int i = 0; i < gameObject.transform.GetChild(2).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(2).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(4).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }


            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);

            whichVehicle = 5;

            for (int i = 0; i < numberOfCrowds; i++)
            {
                gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(true);
            }



        }

        else if (numberOfCrowds < 5)
        {
            for (int i = 0; i < gameObject.transform.GetChild(2).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(2).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(4).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }


            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);

            whichVehicle = 0;

            for (int i = 0; i < numberOfCrowds; i++)
            {
                gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(true);
            }



        }

        else if (numberOfCrowds >10 && numberOfCrowds < 14)
        {
            whichVehicle = 1;

            for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(2).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(2).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(4).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);




        }

        else if (numberOfCrowds > 6 && numberOfCrowds < 10)
        {
            whichVehicle = 4;

            for (int i = 0; i < gameObject.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(2).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(2).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }


            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);


        }

        else if (numberOfCrowds > 13 && numberOfCrowds < 21)
        {
            whichVehicle = 2;

            for (int i = 0; i < gameObject.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(4).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(3).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }




            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);




        }

        else if (numberOfCrowds > 21 && numberOfCrowds < 100)
        {
            whichVehicle = 3;

            //transform.position = new Vector3(0, transform.position.y, transform.position.z);

            for (int i = 0; i < gameObject.transform.GetChild(2).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(2).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }

        UpgradeOrDownGrade();
    }

    private void GameOver()
    {
        if (numberOfCrowds < 0)
        {
            numberOfCrowds = 0;
        }
        if (numberOfCrowds == 0)
        {
            settings.isPlaying = false;
            rb.isKinematic = true;
        }
        if (settings.GameOver && !settings.isPlaying)
        {
            rb.isKinematic = true;
        }
    }

    private void CarChangeEffect()
    {
        Vector3 insPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z - 3);
        Vector3 insPos2 = new Vector3(transform.position.x + 1, transform.position.y + 1.5f, transform.position.z - 3);
        Instantiate(carEffect, insPos, Quaternion.identity).transform.parent = transform;
        //Instantiate(carEffect, insPos2, Quaternion.identity).transform.parent = transform;
    }

    private void UpgradeOrDownGrade()
    {
        if (currentCar < whichVehicle)
        {
            for (int i = 0; i < numberOfCrowds; i++)
            {
                if (this.gameObject.transform.GetChild(whichVehicle).GetChild(0).childCount >= numberOfCrowds)
                {
                    gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
            }
            changeCarDown = true;
            CarChangeEffect();
            changeCarDown = false;
            ui.targetProgress = 0f;
            ui.slider.value = 0;
            ui.CarTextEffect();
        }

        if (currentCar > whichVehicle)
        {
            for (int i = 0; i < numberOfCrowds; i++)
            {
                if (this.gameObject.transform.GetChild(whichVehicle).GetChild(0).childCount >= numberOfCrowds)
                {
                    gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
            }
            changeCarUp = true;
            CarChangeEffect();
            changeCarUp = false;
            ui.targetProgress = 0f;
            ui.slider.value = 0;
            ui.CarTextEffect();



        }

        else return;
    }

    private void TurnLeft()
    {
        if (whichVehicle == 0)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, -5);
            Quaternion tarRor3 = Quaternion.Euler(0, -turnAngle, 0);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, 0.5f);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }

        else if (whichVehicle == 1)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, 5);
            Quaternion tarRor3 = Quaternion.Euler(0, -turnAngle, 0);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, 0.5f);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }

        else if (whichVehicle == 2)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, 5);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, 0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, -turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }

        else if (whichVehicle == 3)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, 5);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, -0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, -turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }

        else if (whichVehicle == 4)
        {
            Quaternion tarRor = Quaternion.Euler(0, 1, 0);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, 0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, -turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }

        else if (whichVehicle == 5)
        {
            Quaternion tarRor = Quaternion.Euler(0, 1, 0);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, 0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, -turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            //transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 5 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 5 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }


    }

    private void TurnRight()
    {
        if (whichVehicle == 0)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, 5);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, -0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);

        }

        else if (whichVehicle == 1)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, -5);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, -0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, turnSpeed * Time.deltaTime);
        }

        else if (whichVehicle == 2)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, -5);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, -0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, turnSpeed * Time.deltaTime);

        }

        else if (whichVehicle == 3)
        {
            Quaternion tarRor = Quaternion.Euler(0, 0, -5);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, 0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, turnSpeed * Time.deltaTime);

        }

        else if (whichVehicle == 4)
        {
            Quaternion tarRor = Quaternion.Euler(0, -1, 0);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, -0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, turnSpeed * Time.deltaTime);

        }

        else if (whichVehicle == 5)
        {
            Quaternion tarRor = Quaternion.Euler(0, -1, 0);
            Quaternion tarRor2 = Quaternion.Euler(0, 0, -0.5f);
            Quaternion tarRor3 = Quaternion.Euler(0, turnAngle, 0);
            Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
            //transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 5 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor3, 5 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, turnSpeed * Time.deltaTime);

        }


    }

    private void GetBackRotation()
    {
        Quaternion tarRor = Quaternion.Euler(0, 0, 0);
        Quaternion tarRor2 = Quaternion.Euler(0, 0, 0);
        Quaternion objRot2 = transform.GetChild(whichVehicle).GetChild(0).transform.localRotation;
        Quaternion vehicleRot = transform.GetChild(whichVehicle).transform.localRotation;

        if (whichVehicle != 5)
        {
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor, 10 * Time.deltaTime);
        }

        if (whichVehicle == 5)
        {
            Quaternion objRot = transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation;
            //transform.GetChild(whichVehicle).GetChild(1).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot, tarRor, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).GetChild(0).transform.localRotation = Quaternion.Slerp(objRot2, tarRor2, 10 * Time.deltaTime);
            transform.GetChild(whichVehicle).transform.localRotation = Quaternion.Slerp(vehicleRot, tarRor, 10 * Time.deltaTime);
        }

    }

    private void SetProgressBar()
    {
        if (whichVehicle == 0)
        {
            ui.ProgressBar(0.079f);
        }

        else if (whichVehicle == 1)
        {
            ui.ProgressBar(0.115f);
        }

        else if (whichVehicle == 2)
        {
            ui.ProgressBar(0.048f);
        }

        else if (whichVehicle == 4)
        {
            ui.ProgressBar(0.086f);
        }

        else if (whichVehicle == 5)
        {
            ui.ProgressBar(0.24f);
        }

        else if (whichVehicle == 3)
        {
            ui.ProgressBar(0.05f);
        }

    }

    private void SetProgressBarNegative(int x)
    {
        if (whichVehicle == 0)
        {
            ui.ProgressBar(0.1f * x);
        }
        else if (whichVehicle == 1)
        {
            ui.ProgressBar(0.130f * x);
        }
        else if (whichVehicle == 2)
        {
            ui.ProgressBar(0.05f * x);
        }
        else if (whichVehicle == 4)
        {
            ui.ProgressBar(0.100f*x);
        }

        else if (whichVehicle == 5)
        {
            ui.ProgressBar(0.25f*x);
        }

    }

    private void CrowdText()
    {
        ui.crowdText.GetComponent<Animator>().SetBool("Timeout", false);
        ui.crowdText.GetComponent<Animator>().SetBool("Hit", false);
        ui.crowdText.GetComponent<Animator>().SetBool("Hit", true);
        ui.crowdText.GetComponent<TextMeshProUGUI>().enabled = false;
        if (Stop2 != null)
        {
            StopCoroutine(Stop2);
        }
        if (Stop != null)
        {
            StopCoroutine(Stop);
        }
        ui.crowdText.GetComponent<TextMeshProUGUI>().enabled = true;
        ui.crowdText.text = "+" + currentCrowd + " " + "Crowd";
        Stop = StartCoroutine(CrowdTextControl());
        Stop2 = StartCoroutine(AnimCont());
    }

    private IEnumerator CrowdTextControl()
    {
        yield return new WaitForSeconds(0.65f);
        if (currentCrowd != 0)
        {
            ui.crowdText.GetComponent<Animator>().SetBool("Hit", false);
            ui.crowdText.GetComponent<Animator>().SetBool("Timeout", true);

        }
        currentCrowd = 0;




        yield return new WaitForSeconds(0.25f);
        ui.crowdText.GetComponent<Animator>().SetBool("Timeout", false);
        //StartCoroutine(AnimCont2());
        ui.crowdText.GetComponent<TextMeshProUGUI>().enabled = false;

    }

    private IEnumerator AnimCont()
    {
        yield return new WaitForSeconds(0.2f);
        ui.crowdText.GetComponent<Animator>().SetBool("Hit", false);

    }

    private IEnumerator AnimCont2()
    {
        yield return new WaitForSeconds(0.3f);
        ui.crowdText.GetComponent<Animator>().SetBool("Timeout", false);

    }

    private void SkidSetActive(int Vehicle)
    {
        if (Vehicle == 0)
        {
            skidATV.SetActive(true);
            skidJeep.SetActive(false);
            skidBus.SetActive(false);
            skidMonster.SetActive(false);
            skidTaxi.SetActive(false);

        }

        else if (Vehicle == 1)
        {
            skidATV.SetActive(false);
            skidJeep.SetActive(true);
            skidBus.SetActive(false);
            skidMonster.SetActive(false);
            skidTaxi.SetActive(false);
        }

        else if (Vehicle == 2)
        {
            skidATV.SetActive(false);
            skidJeep.SetActive(false);
            skidBus.SetActive(true);
            skidMonster.SetActive(false);
            skidTaxi.SetActive(false);
        }

        else if (Vehicle == 3)
        {
            skidATV.SetActive(false);
            skidJeep.SetActive(false);
            skidBus.SetActive(false);
            skidMonster.SetActive(true);
            skidTaxi.SetActive(false);
        }

        else if (Vehicle == 4)
        {
            skidATV.SetActive(false);
            skidJeep.SetActive(false);
            skidBus.SetActive(false);
            skidMonster.SetActive(false);
            skidTaxi.SetActive(true);
        }

    }

    private void SkidSetActiveFalse()
    {
        skidATV.SetActive(false);
        skidJeep.SetActive(false);
        skidBus.SetActive(false);
        skidMonster.SetActive(false);
        skidTaxi.SetActive(false);
    }

    private void SetActiveFinishLine(int X, int Y, int Z)
    {
        numberOfCrowds -= (Z - 1);
        for (int i = Y; i <= X; i++)
        {
            gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        if (numberOfCrowds < Z)
        {
            settings.GameOver = true;
            settings.isPlaying = false;
        }
        if (settings.GameOver && !settings.isPlaying)
        {
            for (int i = 0; i < gameObject.transform.GetChild(whichVehicle).GetChild(0).childCount; i++)
            {
                gameObject.transform.GetChild(whichVehicle).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    private void InstantiateDoll(int X)
    {
        GameObject doll;
        int Y;
        
        Vector3 spawn1 = spawnTransform.transform.position;
        Vector3 spawn2 = spawnTransform2.transform.position;
        for (int i = 0; i <= X; i++)
        {
            Y = Random.Range(1, 3);
            Debug.Log(Y);
            if (Y == 1)
            {
                doll = Instantiate(spawnDoll, spawn1, Quaternion.identity);
                doll.AddComponent<Rigidbody>();
                doll.GetComponent<Rigidbody>().AddForce(3, 3, 3, ForceMode.Impulse);
            }
            else if (Y == 2)
            {
                doll = Instantiate(spawnDoll, spawn2, Quaternion.identity);
                doll.AddComponent<Rigidbody>();
                doll.GetComponent<Rigidbody>().AddForce(3, 3, 3, ForceMode.Impulse);
            }

        }
        
    }


}
