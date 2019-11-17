using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_GoHomeBtn : MonoBehaviour
{
    RPS_CamController RPS_CamController;

    private static RPS_GoHomeBtn _instance;
    public static RPS_GoHomeBtn Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_GoHomeBtn)) as RPS_GoHomeBtn;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_GoHomeBtn";
                    _instance = container.AddComponent(typeof(RPS_GoHomeBtn)) as RPS_GoHomeBtn;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        this.gameObject.SetActive(false);

        RPS_CamController = RPS_CamController.Instance;
    }

    //go home button clicker
    public void OnClickGoHomeBtn()
    {
        Debug.Log("go home");
    }
}
