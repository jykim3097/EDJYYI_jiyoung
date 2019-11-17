using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_ComputerHands : MonoBehaviour
{
    private enum Hands { init, rock, paper, scissors };
    Hands hand = Hands.init;

    public Sprite[] HandSprite = new Sprite[3]; //win, draw, lose
    private Sprite target;

    private int num;
    private int intComHand;

    private static RPS_ComputerHands _instance;
    public static RPS_ComputerHands Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_ComputerHands)) as RPS_ComputerHands;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_ComputerHands";
                    _instance = container.AddComponent(typeof(RPS_ComputerHands)) as RPS_ComputerHands;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        intComHand = -1;
    }

    //숫자 랜덤으로 뽑아내서 가위바위보 번호 설정
    public int RandomState()
    {
        num = Random.Range(0, 3);
        if (num == 0)
        {
            hand = Hands.rock;
        }

        else if (num == 1)
        {
            hand = Hands.paper;
        }
        else if (num == 2)
        {
            hand = Hands.scissors;
        }

        return num;
    }

    //랜덤으로 뽑은 숫자로 가위바위보 sprite 출력
    public void RandomPopUp(int num)
    {
        if (hand == Hands.rock)
        {
            target = HandSprite[num];
            gameObject.GetComponent<SpriteRenderer>().sprite = target;
            transform.gameObject.tag = "rock";
        }
        else if (hand == Hands.paper)
        {
            target = HandSprite[num];
            gameObject.GetComponent<SpriteRenderer>().sprite = target;
            transform.gameObject.tag = "paper";
        }
        else if (hand == Hands.scissors)
        {
            target = HandSprite[num];
            gameObject.GetComponent<SpriteRenderer>().sprite = target;
            transform.gameObject.tag = "scissors";
        }
        
        intComHand = num;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public int GetIntComHand()
    {
        return intComHand;
    }
}
