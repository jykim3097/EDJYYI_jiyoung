using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_UserHands : MonoBehaviour
{
    private int intUserHand;

    RPS_ComputerHands RPS_ComputerHands;
    RPS_InputManager RPS_InputManager;

    private static RPS_UserHands _instance;
    public static RPS_UserHands Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_UserHands)) as RPS_UserHands;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_UserHands";
                    _instance = container.AddComponent(typeof(RPS_UserHands)) as RPS_UserHands;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        intUserHand = -1;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        RPS_ComputerHands = RPS_ComputerHands.Instance;
        RPS_InputManager = RPS_InputManager.Instance;
    }

    public void SelectedHand()
    {
         string whatHand = RPS_InputManager.GetName();

        if(whatHand != null && intUserHand == -1)
        {
            if (whatHand == "Rock")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = RPS_ComputerHands.HandSprite[0];
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                intUserHand = 0;
            }
            else if (whatHand == "Paper")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = RPS_ComputerHands.HandSprite[1];
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                intUserHand = 1;
            }
            else if (whatHand == "Scissors")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = RPS_ComputerHands.HandSprite[2];
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                intUserHand = 2;

            }

            //RPS_InputManager.IsNotCatched();
        }
        
    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public int GetIntUserHand()
    {
        int hand = intUserHand;
        intUserHand = -1;
        return hand;
    }
}
