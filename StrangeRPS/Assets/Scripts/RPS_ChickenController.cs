using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_ChickenController : MonoBehaviour
{
    public Sprite[] Foods = new Sprite[5];
    private Sprite target;
    private int num;

    private string[] strBread = new string[5];
    private int b_idx;

    RPS_CamController RPS_CamController;

    private static RPS_ChickenController _instance;
    public static RPS_ChickenController Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_ChickenController)) as RPS_ChickenController;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_ChickenController";
                    _instance = container.AddComponent(typeof(RPS_ChickenController)) as RPS_ChickenController;
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        b_idx = 0;

        RPS_CamController = RPS_CamController.Instance;
    }

    public void EnableSprite()
    {
        num = RPS_CamController.getOnGame();
        target = Foods[num - 1];
        gameObject.GetComponent<SpriteRenderer>().sprite = target;
        //Debug.Log("Target: " + target.GetInstanceID());
        

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
