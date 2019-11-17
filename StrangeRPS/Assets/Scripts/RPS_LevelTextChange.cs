using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPS_LevelTextChange : MonoBehaviour
{
    public GameObject LevelBox;
    public Text Level;

    public GameObject TimerBox;

    RPS_CamController RPS_CamController;

    private static RPS_LevelTextChange _instance;
    public static RPS_LevelTextChange Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_LevelTextChange)) as RPS_LevelTextChange;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_LevelTextChange";
                    _instance = container.AddComponent(typeof(RPS_LevelTextChange)) as RPS_LevelTextChange;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        LevelBox.gameObject.GetComponent<Image>().enabled = false;
        TimerBox.gameObject.GetComponent<Image>().enabled = false;

        RPS_CamController = RPS_CamController.Instance;
    }

    public void IncreaseLv()
    {
        int intLevelNum = RPS_CamController.getOnGame();
        string strLevelNum = "";

        if ( intLevelNum == 1)
        {
            strLevelNum = "Lv. " + intLevelNum.ToString();
        }
        else
        {
            Level.GetComponent<Text>().enabled = true;
            strLevelNum = "Lv. " + intLevelNum.ToString();
        }

        Level.GetComponent<Text>().text = strLevelNum;
        LevelBox.gameObject.GetComponent<Image>().enabled = true;
        TimerBox.gameObject.GetComponent<Image>().enabled = true;
    }

    public void InvisibleLv()
    {
        LevelBox.gameObject.GetComponent<Image>().enabled = false;
        Level.GetComponent<Text>().enabled = false;
        
        TimerBox.gameObject.GetComponent<Image>().enabled = false;
    }
}
