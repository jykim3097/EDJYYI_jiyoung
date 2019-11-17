using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPS_Discription : MonoBehaviour
{
    public Text saidChar;
    public Button SkipBtn;
    public Image BubbleImage;

    private string[] strArrDiscription;

    private int s_idx;
    private bool isFinished; //배열 끝까지 가면 false

    RPS_InputManager RPS_InputManager;

    private static RPS_Discription _instance;
    public static RPS_Discription Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_Discription)) as RPS_Discription;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_Discription";
                    _instance = container.AddComponent(typeof(RPS_Discription)) as RPS_Discription;
                }
            }
            return _instance;
        }
    }

    private void Start()
    {
        isFinished = true;
        s_idx = 0;

        strArrDiscription = new string[5];
        saidChar.text = "안녕? 내일은 우리 마을에 큰 파티가 있어.";
        
        strArrDiscription[0] = "파티에 가기 위해서는 음식을 가져가야 해";
        strArrDiscription[1] = "음식을 모으기 위해서는 식당에 가서 주인장과 가위바위보를 해야 하는데";
        strArrDiscription[2] = "한 식당에서는 최대 5개의 음식을 모을 수 있어";
        strArrDiscription[3] = "음식을 최대한 많이 빨리 모아봐~!";

        RPS_InputManager = RPS_InputManager.Instance;
    }
    
    public void TouchBubble()
    {
        string name = RPS_InputManager.GetName();

        if (name == "Bubble")
        {
            sayChar();
            s_idx++;

            RPS_InputManager.IsNotCatched();
        }

        //배열이 없어지면 클릭 막기
        if (s_idx > strArrDiscription.Length - 1)
        {
            isFinished = false;
            this.gameObject.SetActive(isFinished);
            BubbleImage.gameObject.SetActive(false);
        }
    }

    private void sayChar()
    {
        if(s_idx > 3) //배열을 다 읽었으면
        {
            isFinished = false;
            this.gameObject.SetActive(isFinished);
            BubbleImage.gameObject.SetActive(false);

            saidChar.GetComponent<Text>().enabled = false;
            SkipBtn.gameObject.SetActive(false);
        }
        else // 그렇지 않으면
        {
            saidChar.text = strArrDiscription[s_idx];
        }
    }
    
    //배열 다 읽었는지 여부
    public bool GetBubbleState()
    {
        return isFinished;
    }

    public bool DisableBubble()
    {
        isFinished = false;
        return isFinished;
    }

}
