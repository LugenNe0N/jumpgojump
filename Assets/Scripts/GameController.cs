using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int Life = 3;
    public int Point = 0;
    int Time = 0;
    int totalScore;

    int s1BestScore = 0;
    int s2BestScore = 0;
    int s3BestScore = 0;
    int s4BestScore = 0;
    int s5BestScore = 0;
    int s6BestScore = 0;

    int countKey;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Controller;
    
    [SerializeField] private GameObject StartArea;
    [SerializeField] private GameObject GameClearArea;
    [SerializeField] private GameObject GameOverArea;
    [SerializeField] private Transform CheckPoint;
    [SerializeField] private Transform ReturnPoint;
    [SerializeField] private GameObject Bridge1;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject clearMessage;
    [SerializeField] private GameObject UIObject;


    public Text pointText;
    public Text pointDisplay;
    public Text resultPointText;
    public Text totalScoreText;
    public Text lifeText;


    private Animator doorAnimator;
    public GameObject keyDoor;
    public Material green;
    public Material red;
    public GameObject checkLight;
    public GameObject checkLight2;

    private GameObject upStage = null;
    private Animator updown = null;

    bool C1firstcheck;
    bool C2firstcheck;
    static bool OnFirst;
    //ドア開閉音
    public AudioSource doorSound;
    public AudioClip soundA;
    //アイテム取得音
    public AudioClip soundB;
    //ダメージ音
    public AudioSource damegeSound;
    public AudioClip soundC;
    //チェックポイント音
    public AudioSource CheckSound;
    public AudioSource CheckS2;
    public AudioClip soundD;
    //キーカード取得
    public AudioClip soundE;

    // Use this for initialization
    void Start () {
        OnFirst = true;
        C1firstcheck = true;
        C2firstcheck = true;
        Player = GameObject.Find("Player");
        

        pointText.text = "Score : " + Point + "pt";
        pointDisplay.text = "Score : " + Point + "pt";
        lifeText.text = "LIFE : " + Life;

        StartArea = GameObject.Find("StartArea");
        GameClearArea = GameObject.Find("GameClearArea");
        GameOverArea = GameObject.Find("GameOverArea");

        ReturnPoint = StartArea.transform;
        
        FlagManager.Instance.flags[9] = false;
        FlagManager.Instance.flags[90] = false;
        clearMessage = GameObject.Find("Canvas4");
        clearMessage.SetActive(false);

        keyDoor = GameObject.Find("KeyDoor");
        keyDoor.GetComponent<BoxCollider>().enabled = false;

        checkLight = GameObject.Find("checkLight");
        checkLight.GetComponent<Renderer>().material = red;

        if (GameObject.Find("UpStage") == true)
        {
            upStage = GameObject.Find("UpStage");
            updown = upStage.GetComponent<Animator>();
        }

        s1BestScore = PlayerPrefs.GetInt("s1", 0);
        s2BestScore = PlayerPrefs.GetInt("s2", 0);
        s3BestScore = PlayerPrefs.GetInt("s3", 0);
        s4BestScore = PlayerPrefs.GetInt("s4", 0);
        s5BestScore = PlayerPrefs.GetInt("s5", 0);
        s6BestScore = PlayerPrefs.GetInt("s6", 0);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("s1", s1BestScore);
        PlayerPrefs.SetInt("s2", s2BestScore);
        PlayerPrefs.SetInt("s3", s3BestScore);
        PlayerPrefs.SetInt("s4", s4BestScore);
        PlayerPrefs.SetInt("s5", s5BestScore);
        PlayerPrefs.SetInt("s6", s6BestScore);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update () {
        countKey = GameObject.FindGameObjectsWithTag("Key").Length
            + GameObject.FindGameObjectsWithTag("Key2").Length;
        
        if (countKey == 0)
        {
            keyDoor.GetComponent<BoxCollider>().enabled = true;
        }
        lifeText.text = "LIFE : " + Life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalArea")
        {
            FlagManager.Instance.flags[9] = true;

            Controller.GetComponent<LaserController>().enabled = true;
            resultPointText.text = "Score : " + Point;
            Time = (int)TimerScript.timeDate;
            totalScore = Point + (300 - Time);
            totalScoreText.text = "TotalScore : " + totalScore;

            clearMessage.SetActive(true);

            if (SceneManager.GetActiveScene().name == "Stage01")
            {
                if (s1BestScore < totalScore)
                {
                    s1BestScore = totalScore;
                }

            }else if (SceneManager.GetActiveScene().name == "Stage02")
            {
                if (s2BestScore < totalScore)
                {
                    s2BestScore = totalScore;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage03")
            {
                if (s3BestScore < totalScore)
                {
                    s3BestScore = totalScore;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage04")
            {
                if (s4BestScore < totalScore)
                {
                    s4BestScore = totalScore;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage05")
            {
                if (s5BestScore < totalScore)
                {
                    s5BestScore = totalScore;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage06")
            {
                if (s6BestScore < totalScore)
                {
                    s6BestScore = totalScore;
                }
            }

            Debug.Log("GOAL !!");
        }

        if (other.gameObject.tag == "CheckPoint")
        {
            Debug.Log("CheckPoint !!");
            CheckPoint = other.gameObject.GetComponent<Transform>();
            ReturnPoint = CheckPoint;

            GameObject Light = other.gameObject.transform.Find("Light").gameObject;
            GameObject Light2 = other.gameObject.transform.Find("Light (1)").gameObject;
            Light.GetComponent<Renderer>().material = green;
            Light2.GetComponent<Renderer>().material = green;

            GameObject C2;
            GameObject c2light;
            GameObject c2light2;

            if (GameObject.FindGameObjectWithTag("CheckPoint2") != null)
            {
                C2 = GameObject.FindGameObjectWithTag("CheckPoint2");
                c2light = C2.gameObject.transform.Find("Light").gameObject;
                c2light2 = C2.gameObject.transform.Find("Light (1)").gameObject;
                c2light.GetComponent<Renderer>().material = red;
                c2light2.GetComponent<Renderer>().material = red;
                C2firstcheck = true;
            }

            if (C1firstcheck == true)
            {
                CheckSound = other.GetComponent<AudioSource>();
                CheckSound.PlayOneShot(soundD);
                C1firstcheck = false;
            }
        }

        if (other.gameObject.tag == "CheckPoint2")
        {
            Debug.Log("CheckPoint 2 !!");
            CheckPoint = other.gameObject.GetComponent<Transform>();
            ReturnPoint = CheckPoint;

            GameObject Light = other.gameObject.transform.Find("Light").gameObject;
            GameObject Light2 = other.gameObject.transform.Find("Light (1)").gameObject;
            Light.GetComponent<Renderer>().material = green;
            Light2.GetComponent<Renderer>().material = green;

            GameObject C1 = GameObject.FindGameObjectWithTag("CheckPoint");
            GameObject c1light = C1.gameObject.transform.Find("Light").gameObject;
            GameObject c1light2 = C1.gameObject.transform.Find("Light (1)").gameObject;
            c1light.GetComponent<Renderer>().material = red;
            c1light2.GetComponent<Renderer>().material = red;
            C1firstcheck = true;

            if (C2firstcheck == true)
            {
                CheckS2 = other.GetComponent<AudioSource>();
                CheckS2.PlayOneShot(soundD);
                C2firstcheck = false;
            }
        }

        if (other.gameObject.tag == "Item")
        {
            Point = Point + 100;
            pointText.text = "Score : " + Point.ToString() + "pt";
            pointDisplay.text = "Score : " + Point.ToString() + "pt";
            AudioSource.PlayClipAtPoint(soundB, transform.position);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AutoDoor1")
        {
            door = GameObject.Find("Door");
            door.GetComponent<Animator>().Play("Open");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "AutoDoor2")
        {
            door = GameObject.Find("Door2");
            door.GetComponent<Animator>().Play("Open");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "KeyDoor")
        {
            door = GameObject.Find("Door3");
            door.GetComponent<Animator>().Play("Open");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "AutoDoor3")
        {
            door = GameObject.Find("Door4");
            door.GetComponent<Animator>().Play("Open");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "Damege")
        {
            Life = Life - 1;
            if (Life <= 0)
            {
                Player.gameObject.transform.position = GameOverArea.transform.position;
                Controller.GetComponent<LaserController>().enabled = true;

                Debug.Log("GameOver !!");
            }
        }

        //もし接触したのがkeyタグだったら
        if (other.gameObject.tag == "Key")
        {
            AudioSource.PlayClipAtPoint(soundE, transform.position);
            //ゲームオブジェクトを排除
            Destroy(other.gameObject);

            checkLight.GetComponent<Renderer>().material = green;
        }

        if (other.gameObject.tag == "Key2")
        {
            AudioSource.PlayClipAtPoint(soundE, transform.position);
            //ゲームオブジェクトを排除
            Destroy(other.gameObject);

            checkLight2.GetComponent<Renderer>().material = green;

            updown.SetBool("Getkey", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AutoDoor1")
        {
            door = GameObject.Find("Door");
            door.GetComponent<Animator>().Play("Close");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "AutoDoor2")
        {
            door = GameObject.Find("Door2");
            door.GetComponent<Animator>().Play("Close");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "KeyDoor")
        {
            door = GameObject.Find("Door3");
            door.GetComponent<Animator>().Play("Close");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }

        if (other.gameObject.tag == "AutoDoor3")
        {
            door = GameObject.Find("Door4");
            door.GetComponent<Animator>().Play("Close");
            doorSound = door.GetComponent<AudioSource>();
            doorSound.PlayOneShot(soundA);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hari")
        {
            damegeSound = Player.GetComponent<AudioSource>();
            damegeSound.PlayOneShot(soundC);
            if (OnFirst == true)
            {
                Life = Life - 1;
                Debug.Log("Hari !!");
                OnFirst = false;
            }

            if (Life > 0)
            {
                Player.gameObject.transform.position = ReturnPoint.position;
                OnFirst = true;
            }
            else if (Life <= 0)
            {
                Player.gameObject.transform.position = GameOverArea.transform.position;
                UIObject.SetActive(false);
                Controller.GetComponent<LaserController>().enabled = true;
                OnFirst = true;
                FlagManager.Instance.flags[90] = true;
                Debug.Log("GameOver !!");
            }
        }

        if (collision.gameObject.tag == "DeathPoint")
        {
            Debug.Log("Damege !!");
            Life = Life - 1;
            if (Life <= 0)
            {
                Player.gameObject.transform.position = GameOverArea.transform.position;
                Controller.GetComponent<LaserController>().enabled = true;
                FlagManager.Instance.flags[90] = true;
                Debug.Log("GameOver !!");
            }
            else
            {
                Player.gameObject.transform.position = ReturnPoint.position;
            }
        }

        if (collision.gameObject.name == "MoveStage")
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MoveStage")
        {
            transform.SetParent(null);
        }
    }
}
