using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_CamController : MonoBehaviour
{
    RPS_CharacterManager RPS_CharacterManager;
    RPS_GameManager RPS_GameManager;

    private int onGame;

    private static RPS_CamController _instance;
    public static RPS_CamController Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_CamController)) as RPS_CamController;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_CamController";
                    _instance = container.AddComponent(typeof(RPS_CamController)) as RPS_CamController;
                }
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RPS_CharacterManager = RPS_CharacterManager.Instance;
        RPS_GameManager = RPS_GameManager.Instance;
        
        onGame = 0;
    }

    public int MoveToGame()
    {
        //식당들 중에 하나에 도착하면 게임화면으로 전환하게 할 수 있으면 좋을 듯 -> 어려워..
        //식당1에 도착하면 게임화면으로 전환
        if (RPS_CharacterManager.getCharPostion() == RPS_CharacterManager.GetRes1Position())
        {
            this.gameObject.transform.position = new Vector3(7f, 0f, -10f);

            onGame = 1;
            return onGame;
        }
        //식당2에 도착하면 게임화면으로 전환
        else if (RPS_CharacterManager.getCharPostion() == RPS_CharacterManager.GetRes2Position())
        {
            this.gameObject.transform.position = new Vector3(7f, 0f, -10f);

            onGame = 2;
            return onGame;
        }
        //식당3에 도착하면 게임화면으로 전환
        else if (RPS_CharacterManager.getCharPostion() == RPS_CharacterManager.GetRes3Position())
        {
            this.gameObject.transform.position = new Vector3(7f, 0f, -10f);

            onGame = 3;
            return onGame;
        }
        //식당4에 도착하면 게임화면으로 전환
        else if (RPS_CharacterManager.getCharPostion() == RPS_CharacterManager.GetRes4Position())
        {
            this.gameObject.transform.position = new Vector3(7f, 0f, -10f);

            onGame = 4;
            return onGame;
        }
        //식당5에 도착하면 게임화면으로 전환
        else if (RPS_CharacterManager.getCharPostion() == RPS_CharacterManager.GetRes5Position())
        {
            this.gameObject.transform.position = new Vector3(7f, 0f, -10f);

            onGame = 5;
            return onGame;
        }
        //파티장에 도착하면 게임화면으로 결과화면
        else if (RPS_CharacterManager.getCharPostion() == RPS_CharacterManager.GetPartyPosition())
        {
            onGame = 6;
            return onGame;
        }
        else
        {
            onGame = 7;
            return onGame;
        }
    }

    public void MoveToTown()
    {
        //게임 5판을 완료하면 마을로 돌아감
        if (RPS_GameManager.GetCnt() == 5)
        {
            this.gameObject.transform.position = new Vector3(0f, 0f, -10f);
            onGame = 0;
        }
    }

    public int getOnGame()
    {
        return onGame;
    }
}
