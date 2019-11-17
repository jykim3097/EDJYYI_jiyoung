using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class RPS_GameManager : MonoBehaviour
{
    public enum Level { init, lv1, lv2, lv3, lv4, lv5, party};
    Level level;

    enum State { init, success, fail};
    State state;

    public GameObject bar1, bar2, bar3, bar4, bar5;
    public GameObject Fail;
    public Button GoHomeBtn;
    
    public Text TextInTimerBox;

    private int[] intDownTime = new int[25]; // 흐른 시간
    private int dt_idx;

    private int[] intState = new int[25]; //성공실패 여부 넣음
    private int s_idx;

    private int[] intSucState = { 0, 0, 0, 0, 0 }; //레벨별 성공 횟수
    private int[] intFailState = { 0, 0, 0, 0, 0 }; //레벨별 실패 횟수

    private bool alertFlag, comHandsFlag, cntFlag, scoreFlag, timerFlag, calcFlag;
    private string strAlert;
    private int intComHand, intUserHand, intCompareHand;
    private int cnt;

    private float stdTime = 11f; //standard time
    private int downTime; //흐른 시간
    private float limitedTime;
    float score;

    int level_idx;

    private float floatTimer;
    private float[] floatArrTimer = new float[5];

    private int intSucCnt_box;
    private int intFailCnt_box;
    private int intScore_box;
    private int intMaxTime;
    private int intMinTime;

    Stopwatch sw = new Stopwatch();

    RPS_Discription RPS_Discription;
    RPS_CharacterManager RPS_CharacterManager;
    RPS_CamController RPS_CamController;
    RPS_AlertController RPS_AlertController;
    RPS_ComputerHands RPS_ComputerHands;
    RPS_UserHands RPS_UserHands;
    RPS_InputManager RPS_InputManager;
    RPS_ChickenController RPS_ChickenController;
    RPS_LevelTextChange RPS_LevelTextChange;
    RPS_ScoreManager RPS_ScoreManager;

    private static RPS_GameManager _instance;
    public static RPS_GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(RPS_GameManager)) as RPS_GameManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "RPS_GameManager";
                    _instance = container.AddComponent(typeof(RPS_GameManager)) as RPS_GameManager;
                }
            }
            return _instance;
        }
    }

    struct StrangeRPS_Box
    {
        int score;
        int maxTime; //가장 오래 걸린 시간
        int minTime; //가장 적게 걸린 시간

        public StrangeRPS_Box(int score, int maxTime, int minTime)
        {
            this.score = score;
            this.maxTime = maxTime;
            this.minTime = minTime;
        }

        public StrangeRPS_Box(string s)
        {
            string[] arr;
            arr = s.Split(':');
            int a = int.Parse(arr[2]);
            int b = int.Parse(arr[4]);
            int c = int.Parse(arr[6]);
            
            score = a;
            maxTime = b;
            minTime = c;
        }

        override public string ToString()
        {
            return "StrangeRPS:score:" + score + ":maxTime:" + maxTime + ":minTime:" + minTime;
        }
    }

    void Start()
    {
        level = Level.init;
        state = State.init;

        alertFlag = true;
        comHandsFlag = true;
        cntFlag = true;
        scoreFlag = true;
        timerFlag = true;
        calcFlag = true;

        cnt = 0;
        score = 0;
        downTime = 0;
        level_idx = 0;
        limitedTime = 0;

        dt_idx = 0;
        s_idx = 0;

        //progress bar
        bar1.GetComponent<SpriteRenderer>().enabled = false;
        bar2.GetComponent<SpriteRenderer>().enabled = false;
        bar3.GetComponent<SpriteRenderer>().enabled = false;
        bar4.GetComponent<SpriteRenderer>().enabled = false;
        bar5.GetComponent<SpriteRenderer>().enabled = false;
        Fail.GetComponent<SpriteRenderer>().enabled = false;

        //레벨 별 제한시간 초기 설정 7 6 5 4 3s
        for (int i = 0; i < floatArrTimer.Length; i++)
        {
            floatArrTimer[i] = 7 - i;
        }

        //제한 시간
        TextInTimerBox.GetComponent<Text>().enabled = false;

        RPS_Discription = RPS_Discription.Instance;
        RPS_CharacterManager = RPS_CharacterManager.Instance;
        RPS_CamController = RPS_CamController.Instance;
        RPS_AlertController = RPS_AlertController.Instance;
        RPS_ComputerHands = RPS_ComputerHands.Instance;
        RPS_UserHands = RPS_UserHands.Instance;
        RPS_InputManager = RPS_InputManager.Instance;
        RPS_ChickenController = RPS_ChickenController.Instance;
        RPS_LevelTextChange = RPS_LevelTextChange.Instance;
        RPS_ScoreManager = RPS_ScoreManager.Instance;
    }
    
    public void Update()
    {
        switch (level)
        {
            case Level.init:
                RPS_Discription.TouchBubble();
                RPS_CharacterManager.DissapearLargeChar(); //큰 캐릭터가 없어지면
                RPS_CharacterManager.MoveSmallChar(); //작은 캐릭터가 움직인다
                RPS_CamController.MoveToGame(); //작은 캐릭터가 식당1에 도착하면 카메라를 전환한다.

                //식당1에 도착하면 레벨을 1로 바꾼다
                if (RPS_CamController.getOnGame() == 1)
                {
                    RPS_LevelTextChange.IncreaseLv();
                    level = Level.lv1;
                }
                break;

            case Level.lv1:
                
                rpsGame();

                //게임이 끝나면 캐릭터를 식당2로 이동시킨다
                if (RPS_CamController.getOnGame() == 0)
                {
                    RPS_LevelTextChange.InvisibleLv();
                    RPS_CharacterManager.MoveSmallChar2();
                }
                RPS_CamController.MoveToGame(); //작은 캐릭터가 식당2에 도착하면 카메라를 전환한다.

                //식당2에 도착하면 레벨을 2로 바꾼다
                if (RPS_CamController.getOnGame() == 2)
                {
                    cnt = 0;
                    RPS_LevelTextChange.IncreaseLv();
                    level = Level.lv2;
                }
                break;

            case Level.lv2:
                rpsGame();

                //게임이 끝나면 캐릭터를 식당3으로 이동시킨다
                if (RPS_CamController.getOnGame() == 0)
                {
                    RPS_LevelTextChange.InvisibleLv();
                    RPS_CharacterManager.MoveSmallChar3();
                }
                RPS_CamController.MoveToGame(); //작은 캐릭터가 식당2에 도착하면 카메라를 전환한다.

                //식당3에 도착하면 레벨을 3로 바꾼다
                if (RPS_CamController.getOnGame() == 3)
                {
                    cnt = 0;
                    RPS_LevelTextChange.IncreaseLv();
                    level = Level.lv3;
                }
                break;

            case Level.lv3:
                rpsGame();

                //게임이 끝나면 캐릭터를 식당4로 이동시킨다
                if (RPS_CamController.getOnGame() == 0)
                {
                    RPS_LevelTextChange.InvisibleLv();
                    RPS_CharacterManager.MoveSmallChar4();
                }
                RPS_CamController.MoveToGame(); //작은 캐릭터가 식당2에 도착하면 카메라를 전환한다.

                //식당4에 도착하면 레벨을 4로 바꾼다
                if (RPS_CamController.getOnGame() == 4)
                {
                    cnt = 0;
                    RPS_LevelTextChange.IncreaseLv();
                    level = Level.lv4;
                }
                break;

            case Level.lv4:
                rpsGame();

                //게임이 끝나면 캐릭터를 식당5로 이동시킨다
                if (RPS_CamController.getOnGame() == 0)
                {
                    RPS_LevelTextChange.InvisibleLv();
                    RPS_CharacterManager.MoveSmallChar5();
                }
                RPS_CamController.MoveToGame(); //작은 캐릭터가 식당에 도착하면 카메라를 전환한다.

                //식당5에 도착하면 레벨을 5로 바꾼다
                if (RPS_CamController.getOnGame() == 5)
                {
                    cnt = 0;
                    RPS_LevelTextChange.IncreaseLv();
                    level = Level.lv5;
                }
                break;

            case Level.lv5:
                rpsGame();

                //게임이 끝나면 캐릭터를 파티장으로 이동시킨다
                if (RPS_CamController.getOnGame() == 0)
                {
                    RPS_LevelTextChange.InvisibleLv();
                    RPS_CharacterManager.MoveSmallChar6();
                }
                RPS_CamController.MoveToGame();

                //파티장에 도착하면 레벨을 party으로 바꾼다
                if (RPS_CamController.getOnGame() == 6)
                {
                    level = Level.party;
                }
                break;

            case Level.party:
                if (scoreFlag)
                {
                    RPS_ScoreManager.showScore();
                    GoHomeBtn.gameObject.SetActive(true);

                    StrangeRPS_Box StrangeRPS_Box = new StrangeRPS_Box((int)GetScore(), getMaxTime(), getMinTime());
                    Debug.Log("StrangeRPS_Box.ToString():"+StrangeRPS_Box.ToString());

                    scoreFlag = false;
                }
                break;
        }

    }

    void rpsGame()
    {
        //제한 시간 보여주기
        int getLevel = RPS_CamController.getOnGame() - 1;

        //시간 초과 시 실패로 상태 바꾸기
        if (cnt < 5 && floatArrTimer[getLevel] <= 0)
        {
            if (calcFlag)
            {
                state = State.fail;
                calculateScore();
            }
            calcFlag = false;
        }
        else
        {
            if (cnt < 5)
            {
                if (timerFlag)
                    setTimer();

                if (RPS_InputManager.GetIsCatched())
                {
                    timerFlag = false;
                }

                if (cnt == 0)
                {
                    cntFlag = true;
                    timerFlag = true;
                    randomAlert(); //지시어 팝업
                    randomComHands(); //컴퓨터 손 팝업
                }

                RPS_UserHands.SelectedHand(); //유저가 선택한 손 팝업

                //count가 4일 때까지만 유저가 손 선택할 수 있음(레벨 넘어갈 때 터치되면 안됨)
                //뭔가 이상한데..
                if (cnt < 4)
                    RPS_InputManager.IsNotCatched();

                //컴퓨터 손과 유저 손을 비교해서 성공여부 계산
                intUserHand = RPS_UserHands.GetIntUserHand();
                intCompareHand = intComHand - intUserHand;

                if (intComHand != -1 && intUserHand != -1)
                {
                    if (strAlert == "win")
                    {
                        if (intCompareHand == -1 || intCompareHand == 2)
                        {
                            state = State.success;
                        }
                        else
                        {
                            state = State.fail;
                        }
                    }
                    else if (strAlert == "draw")
                    {
                        if (intCompareHand == 0)
                        {
                            state = State.success;
                        }
                        else
                        {
                            state = State.fail;
                        }
                    }
                    else if (strAlert == "lose")
                    {
                        if (intCompareHand == 1 || intCompareHand == -2)
                        {
                            state = State.success;
                        }
                        else
                        {
                            state = State.fail;
                        }
                    }

                    sw.Stop();
                    downTime = (int)sw.ElapsedMilliseconds / 1000;
                    sw.Reset();

                    //수행 시간 배열에 넣기
                    intDownTime[dt_idx] = downTime;
                    dt_idx++;

                    calculateScore();
                }

            }
            else
            {
                //게임 5판을 완료하면 마을로 돌아감
                RPS_CamController.MoveToTown();
            }

        }
    }

    void randomAlert()
    {
        if (alertFlag)
        {
            RPS_AlertController.RandomPopUp(RPS_AlertController.RandomState()); //지시어 alert
            strAlert = RPS_AlertController.GetStrAlert();
            alertFlag = false;
        }
    }

    void randomComHands()
    {
        if (comHandsFlag)
        {
            RPS_ComputerHands.RandomPopUp(RPS_ComputerHands.RandomState()); //컴퓨터 손 alert
            sw.Start();
            intComHand = RPS_ComputerHands.GetIntComHand();
            comHandsFlag = false;
        }
    }

    //성공 여부에 따라 이미지 띄워주기, 점수 계산
    void calculateScore()
    {
        if (state == State.success)
        {
            //이미지 띄우기
            RPS_ChickenController.EnableSprite();

            //점수 계산
            level_idx = level.GetHashCode();
            limitedTime = stdTime - level_idx;
            score += 5f + level_idx * (10 - downTime);

            intState[s_idx] = 1;
            s_idx++;
        }

        if (state == State.fail)
        {
            Fail.GetComponent<SpriteRenderer>().enabled = true;

            score += 5f;

            intState[s_idx] = 0;
            s_idx++;
        }

        StartCoroutine(RestartCoroutine());
    }

    public float GetScore()
    {
        return score;
    }

    IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(1f);
        cleanBoard();
        increaseCnt();
        setBoard();
        timerFlag = true;
        calcFlag = true;

        for (int i = 0; i < floatArrTimer.Length; i++)
        {
            floatArrTimer[i] = 7 - i;
        }
        yield return null;
    }

    void cleanBoard()
    {
        RPS_AlertController.DisableSprite();
        RPS_ComputerHands.DisableSprite();
        RPS_UserHands.DisableSprite();
        RPS_ChickenController.DisableSprite();
        Fail.GetComponent<SpriteRenderer>().enabled = false;
        TextInTimerBox.GetComponent<Text>().enabled = false;
    }

    int increaseCnt()
    {
        if (cntFlag)
        {
            cnt++;
        }
        cntFlag = false;

        setBar();

        return cnt;
    }

    void set5to0Cnt()
    {
        cnt = 0;
    }

    public int GetCnt()
    {
        return cnt;
    }

    void setBoard()
    {
        alertFlag = true;
        comHandsFlag = true;
        cntFlag = true;
        
        randomAlert();
        randomComHands();
        RPS_UserHands.SelectedHand();
    }

    void setTimer()
    {
        int getLevel = RPS_CamController.getOnGame() - 1;
        
        TextInTimerBox.GetComponent<Text>().enabled = true;
        floatArrTimer[getLevel] -= Time.deltaTime;
        updateTimerBox();

        //시간이 0이되면 멈춰
        if (floatArrTimer[getLevel] <= 0)
        {
            timerFlag = false;
        }
    }

    void updateTimerBox()
    {
        int getLevel = RPS_CamController.getOnGame() - 1;

        int intTimer = (int)floatArrTimer[getLevel];
        TextInTimerBox.text = intTimer.ToString();
    }

    //가장 오래 걸린 시간
    int getMaxTime()
    {
        int tmp = 0;

        for(int i = 0; i < intDownTime.Length; i++)
        {
            if(tmp <= intDownTime[i])
            {
                tmp = intDownTime[i];
            }
        }

        return tmp;
    }
    
    //가장 적게 걸린 시간
    int getMinTime()
    {
        int tmp = 8;

        for(int i=0; i< intDownTime.Length; i++)
        {
            if( tmp >= intDownTime[i])
            {
                tmp = intDownTime[i];
            }
        }

        return tmp;
    }

    public void ArrangeIntState()
    {
        for(int i=0; i < 5; i++)
        {
            if(intState[i] == 1)
            {
                intSucState[0]++;
            }
            else if(intState[i] == 0)
            {
                intFailState[0]++;
            }
        }

        for (int i = 5; i < 10; i++)
        {
            if (intState[i] == 1)
            {
                intSucState[1]++;
            }
            else if (intState[i] == 0)
            {
                intFailState[1]++;
            }
        }

        for (int i = 10; i < 15; i++)
        {
            if (intState[i] == 1)
            {
                intSucState[2]++;
            }
            else if (intState[i] == 0)
            {
                intFailState[2]++;
            }
        }

        for (int i = 15; i < 20; i++)
        {
            if (intState[i] == 1)
            {
                intSucState[3]++;
            }
            else if (intState[i] == 0)
            {
                intFailState[3]++;
            }
        }

        for (int i = 20; i < 25; i++)
        {
            if (intState[i] == 1)
            {
                intSucState[4]++;
            }
            else if (intState[i] == 0)
            {
                intFailState[4]++;
            }
        }
    }
    
    public int[] GetIntSucState()
    {
        return intSucState;
    }

    public int[] GetIntFailState()
    {
        return intFailState;
    }

    void setBar()
    {
        if (GetCnt() == 1)
            bar1.GetComponent<SpriteRenderer>().enabled = true;
        if (GetCnt() == 2)
            bar2.GetComponent<SpriteRenderer>().enabled = true;
        if (GetCnt() == 3)
            bar3.GetComponent<SpriteRenderer>().enabled = true;
        if (GetCnt() == 4)
            bar4.GetComponent<SpriteRenderer>().enabled = true;
        if (GetCnt() == 5)
        {
            bar5.GetComponent<SpriteRenderer>().enabled = true;

            bar1.GetComponent<SpriteRenderer>().enabled = false;
            bar2.GetComponent<SpriteRenderer>().enabled = false;
            bar3.GetComponent<SpriteRenderer>().enabled = false;
            bar4.GetComponent<SpriteRenderer>().enabled = false;
            bar5.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}