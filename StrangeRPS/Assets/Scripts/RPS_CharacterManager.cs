using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_CharacterManager : MonoBehaviour
{
    public GameObject LargeCharacter;
    public GameObject SmallCharacter;

    public GameObject Restaurant1;
    public GameObject Restaurant2;
    public GameObject Restaurant3;
    public GameObject Restaurant4;
    public GameObject Restaurant5;
    public GameObject Party;

    private Vector2 currPosition;
    private Vector2 res0, res1, res2, res3, res4, res5, party; //EndPositions

    private float speed = 2f;

    private bool isDissapear;

    RPS_Discription RPS_Discription;
    RPS_CamController RPS_CamController;
    RPS_GameManager RPS_GameManager;

    private static RPS_CharacterManager _instance;
    public static RPS_CharacterManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_CharacterManager)) as RPS_CharacterManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_CharacterManager";
                    _instance = container.AddComponent(typeof(RPS_CharacterManager)) as RPS_CharacterManager;
                }
            }
            return _instance;
        }
    }
    
    void Start()
    {
        RPS_Discription = RPS_Discription.Instance;
        RPS_CamController = RPS_CamController.Instance;
        RPS_GameManager = RPS_GameManager.Instance;

        isDissapear = true;

        res1 = Restaurant1.transform.position;
        res2 = Restaurant2.transform.position;
        res3 = Restaurant3.transform.position;
        res4 = Restaurant4.transform.position;
        res5 = Restaurant5.transform.position;
        party = Party.transform.position;
    }

    //배열이 다 읽혀지면 큰 캐릭터를 없애고 작은 캐릭터를 이동시킨다.
    public void DissapearLargeChar()
    {
        if(RPS_Discription.GetBubbleState() == false)
        {
            isDissapear = false;
            LargeCharacter.SetActive(isDissapear);
        }
    }

    //큰 캐릭터가 없어지면 작은 캐릭터가 움직인다.
    public void MoveSmallChar()
    {
        //res0 -> res1
        if(isDissapear == false)
        {
            res0 = SmallCharacter.transform.position;
            float step = speed * Time.deltaTime;
            SmallCharacter.transform.position = Vector2.MoveTowards(res0, res1, step);
            currPosition = SmallCharacter.transform.position;
        }
    }

    public void MoveSmallChar2()
    {
        //res1 -> res2
        if (RPS_GameManager.GetCnt() == 5)
        {
            res1 = SmallCharacter.transform.position;
            float step = speed * Time.deltaTime;
            SmallCharacter.transform.position = Vector2.MoveTowards(res1, res2, step);
            currPosition = SmallCharacter.transform.position;
        }
    }

    public void MoveSmallChar3()
    {
        //res2 -> res3
        if (RPS_GameManager.GetCnt() == 5 )
        {
            res2 = SmallCharacter.transform.position;
            float step = speed * Time.deltaTime;
            SmallCharacter.transform.position = Vector2.MoveTowards(res2, res3, step);
            currPosition = SmallCharacter.transform.position;
        }
    }

    public void MoveSmallChar4()
    {
        //res3 -> res4
        if (RPS_GameManager.GetCnt() == 5)
        {
            res3 = SmallCharacter.transform.position;
            float step = speed * Time.deltaTime;
            SmallCharacter.transform.position = Vector2.MoveTowards(res3, res4, step);
            currPosition = SmallCharacter.transform.position;
        }
    }

    public void MoveSmallChar5()
    {
        //res4 -> res5
        if (RPS_GameManager.GetCnt() == 5)
        {
            res4 = SmallCharacter.transform.position;
            float step = speed * Time.deltaTime;
            SmallCharacter.transform.position = Vector2.MoveTowards(res4, res5, step);
            currPosition = SmallCharacter.transform.position;
        }
    }

    public void MoveSmallChar6()
    {
        //res5 -> res6
        if (RPS_GameManager.GetCnt() == 5)
        {
            res5 = SmallCharacter.transform.position;
            float step = speed * Time.deltaTime;
            SmallCharacter.transform.position = Vector2.MoveTowards(res5, party, step);
            currPosition = SmallCharacter.transform.position;
        }
    }

    public Vector2 getCharPostion()
    {
        return currPosition;
    }
    
    public Vector2 GetRes1Position()
    {
        return res1;
    }

    public Vector2 GetRes2Position()
    {
        return res2;
    }

    public Vector2 GetRes3Position()
    {
        return res3;
    }

    public Vector2 GetRes4Position()
    {
        return res4;
    }

    public Vector2 GetRes5Position()
    {
        return res5;
    }

    public Vector2 GetPartyPosition()
    {
        return party;
    }

    public bool IsDissapear()
    {
        return isDissapear;
    }
}
