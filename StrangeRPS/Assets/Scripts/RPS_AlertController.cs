using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_AlertController : MonoBehaviour
{
    private enum Alert { init, win, draw, lose};
    Alert alert = Alert.init;

    public Sprite[] commandSprite = new Sprite[3]; //win, draw, lose
    private Sprite alertTarget;

    private int num;
    private string strAlert;

    RPS_Discription RPS_Discription;
    RPS_CamController RPS_CamController;

    private static RPS_AlertController _instance;
    public static RPS_AlertController Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_AlertController)) as RPS_AlertController;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_AlertController";
                    _instance = container.AddComponent(typeof(RPS_AlertController)) as RPS_AlertController;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        num = -1;
        strAlert = "init";
    }
    
    //숫자 랜덤으로 뽑아내서 alert 번호 설정
    public int RandomState()
    {
        num = Random.Range(0, 3);
        if (num == 0)
        {
            alert = Alert.win;
        }

        else if (num == 1)
        {
            alert = Alert.draw;
        }
        else if (num == 2)
        {
            alert = Alert.lose;
        }

        return num;
    }

    //랜덤으로 뽑은 숫자로 지시어 sprite 출력
    public void RandomPopUp(int num)
    {
        if (alert == Alert.win)
        {
            alertTarget = commandSprite[num];
            transform.gameObject.tag = "win";
        }
        else if (alert == Alert.draw)
        {
            alertTarget = commandSprite[num];
            transform.gameObject.tag = "draw";
        }
        else if (alert == Alert.lose)
        {
            alertTarget = commandSprite[num];
            transform.gameObject.tag = "lose";
        }

        strAlert = transform.gameObject.tag;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = alertTarget;

    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public string GetStrAlert()
    {
        return strAlert;
    }
}
