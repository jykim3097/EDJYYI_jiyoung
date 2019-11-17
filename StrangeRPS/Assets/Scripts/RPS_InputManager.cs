using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_InputManager : MonoBehaviour
{
    private string hand;
    private string objectName;
    private bool isCatched;

    RPS_Discription RPS_Discription;
    RPS_UserHands RPS_UserHands;

    private static RPS_InputManager _instance;
    public static RPS_InputManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_InputManager)) as RPS_InputManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_InputManager";
                    _instance = container.AddComponent(typeof(RPS_InputManager)) as RPS_InputManager;
                }
            }
            return _instance;
        }
    }
    
    void Start()
    {
        isCatched = false;

        RPS_Discription = RPS_Discription.Instance;
        RPS_UserHands = RPS_UserHands.Instance;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCatched == false)
        {
            CastRay();
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //Application.Quit();
        //}

    }

    void CastRay()
    {
        Vector2 worldPorint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPorint, Vector2.zero);

        if (hit)
        {
            if (hit.collider.tag == "tagName")
            {
                objectName = hit.collider.name;
                isCatched = true;
            }

        }
    }

    public string GetName()
    {
        string name = objectName;
        objectName = null;
        return name;
    }

    public bool GetIsCatched()
    {
        return isCatched;
    }

    public void IsNotCatched()
    {
        isCatched = false;
    }

    public void SetIsCatched()
    {
        isCatched = true;
    }
}
