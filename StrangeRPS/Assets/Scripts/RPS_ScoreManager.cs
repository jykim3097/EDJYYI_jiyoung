using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RPS_ScoreManager : MonoBehaviour
{
    public GameObject Bread;
    public GameObject Fri;
    public GameObject Bacon;
    public GameObject Steak;
    public GameObject Dessert;
    public GameObject BreadBox;
    public GameObject FriBox;
    public GameObject BaconBox;
    public GameObject SteakBox;
    public GameObject DessertBox;
    
    public Text Score;
    public Text Text;

    public Text BreadBoxText;
    public Text FriBoxText;
    public Text BaconBoxText;
    public Text SteakBoxText;
    public Text DessertBoxText;

    int[] intArr = { 0, 0, 0, 0, 0 };

    float floatScore;

    RPS_GameManager RPS_GameManager;
    RPS_CamController RPS_CamController;

    private static RPS_ScoreManager _instance;
    public static RPS_ScoreManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_ScoreManager)) as RPS_ScoreManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_ScoreManager";
                    _instance = container.AddComponent(typeof(RPS_ScoreManager)) as RPS_ScoreManager;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        this.gameObject.GetComponent<Image>().enabled = false; //이것은 뭐시당가

        Bread.gameObject.GetComponent<Image>().enabled = false;
        Fri.gameObject.GetComponent<Image>().enabled = false;
        Bacon.gameObject.GetComponent<Image>().enabled = false;
        Steak.gameObject.GetComponent<Image>().enabled = false;
        Dessert.gameObject.GetComponent<Image>().enabled = false;

        BreadBox.gameObject.GetComponent<Image>().enabled = false;
        FriBox.gameObject.GetComponent<Image>().enabled = false;
        BaconBox.gameObject.GetComponent<Image>().enabled = false;
        SteakBox.gameObject.GetComponent<Image>().enabled = false;
        DessertBox.gameObject.GetComponent<Image>().enabled = false;
        
        Text.gameObject.GetComponent<Text>().enabled = false;
        Score.gameObject.GetComponent<Text>().enabled = false;

        BreadBoxText.gameObject.GetComponent<Text>().enabled = false;
        FriBoxText.gameObject.GetComponent<Text>().enabled = false;
        BaconBoxText.gameObject.GetComponent<Text>().enabled = false;
        SteakBoxText.gameObject.GetComponent<Text>().enabled = false;
        DessertBoxText.gameObject.GetComponent<Text>().enabled = false;

        RPS_GameManager = RPS_GameManager.Instance;
        RPS_CamController = RPS_CamController.Instance;
    }

    public void showScore()
    {
        this.gameObject.GetComponent<Image>().enabled = true;

        Bread.gameObject.GetComponent<Image>().enabled = true;
        Fri.gameObject.GetComponent<Image>().enabled = true;
        Bacon.gameObject.GetComponent<Image>().enabled = true;
        Steak.gameObject.GetComponent<Image>().enabled = true;
        Dessert.gameObject.GetComponent<Image>().enabled = true;

        BreadBox.gameObject.GetComponent<Image>().enabled = true;
        FriBox.gameObject.GetComponent<Image>().enabled = true;
        BaconBox.gameObject.GetComponent<Image>().enabled = true;
        SteakBox.gameObject.GetComponent<Image>().enabled = true;
        DessertBox.gameObject.GetComponent<Image>().enabled = true;

        Text.gameObject.GetComponent<Text>().enabled = true;
            
        floatScore = RPS_GameManager.GetScore();
        Score.GetComponent<Text>().text = floatScore.ToString();
        Score.gameObject.GetComponent<Text>().enabled = true;

        //음식별 카운트
        RPS_GameManager.ArrangeIntState();
        intArr = RPS_GameManager.GetIntSucState();

        BreadBoxText.GetComponent<Text>().text = intArr[0].ToString();
        BreadBoxText.gameObject.GetComponent<Text>().enabled = true;

        FriBoxText.GetComponent<Text>().text = intArr[1].ToString();
        FriBoxText.gameObject.GetComponent<Text>().enabled = true;

        BaconBoxText.GetComponent<Text>().text = intArr[2].ToString();
        BaconBoxText.gameObject.GetComponent<Text>().enabled = true;

        SteakBoxText.GetComponent<Text>().text = intArr[3].ToString();
        SteakBoxText.gameObject.GetComponent<Text>().enabled = true;

        DessertBoxText.GetComponent<Text>().text = intArr[4].ToString();
        DessertBoxText.gameObject.GetComponent<Text>().enabled = true;
    }

}
