using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPS_SkipClicker : MonoBehaviour
{
    public Text saidChar;
    public GameObject Bubble;

    private bool isFinished;

    RPS_Discription RPS_Discription;
    
    void Start()
    {
        RPS_Discription = RPS_Discription.Instance;
    }

    //skip
    public void OnClickSkip()
    {
        isFinished = RPS_Discription.DisableBubble();
        Bubble.gameObject.SetActive(isFinished);
        saidChar.GetComponent<Text>().enabled = false;
        //SkipButton.GetComponent<Button>().enabled = false;
        this.gameObject.SetActive(isFinished);
    }
}
