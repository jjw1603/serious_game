using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastStageManager : MonoBehaviour
{
    [Header("공용, 기타")] 
    public Button btnBackTitle;
    public Text txtQuizScript;
    public int nQuizTotalCount;
    public int nQuizNowStep;
    public bool isQuizLoad;
    public Text txtStage;

    //[Header("답안 형식 - 깃발")]
    //public GameObject objAnswerType0set;
    //public Button L_btnUp;
    //public Button L_btnDown;
    //public Button R_btnUp;
    //public Button R_btnDown;

    //[Header("답안 형식 - 1")]
    //public GameObject objAnswerType1Set;
    //public Button btnO;
    //public Button btnX;
    //public bool isChooseO;
    //public bool isChooseX;

    [Header("답안 형식 - 2")]
    public GameObject objAnswerType2Set;
    public Button btnRightUp;
    public Button btnRightDown;
    public Button btnLeftUp;
    public Button btnLeftDown;
    public Text txtAnswer1;
    public Text txtAnswer2;
    public Text txtAnswer3;
    public Text txtAnswer4;
    public bool isChoose1;
    public bool isChoose2;
    public bool isChoose3;
    public bool isChoose4;

    //[Header("답안 형식 - 3")]
    //public GameObject objAnswerType3Set;
    //public InputField ipfAnswer;
    //public Button btnAnswerIpf;
    //public bool isSubmitIpf;

    [Header("결과팝업창")]
    public GameObject objResultPopupSet;
    public GameObject objCorrectAnswer;
    public GameObject objWrongAnswer;
    public Button btnResultConfirm;

    GameDataManager gameDataManager;

    public bool isCorrect;
    public GameObject objTotalResult;
    public Text txtTotalResult;
    public Text txtAnswerDivTotal;
    public int nTotalCorrectAnswerCount;

    public int cnt;
    public bool cnt_check;
    public int RandomPick;
    public int RandomPick_;
    public int RandomPickControl;
    public int RandomMax;

    public string nTotalCorrectAnswerCountToInt;
    public string nQuizTotalCountToInt;

    public ArrayList RandomWeight = new ArrayList(); // 가중치 배열

    GameObject ImgHP;

    public void DecreaseHP()
    {
        ImgHP.GetComponent<Image>().fillAmount -= 0.33f;
    }

    public void InitAll()
    {
        txtQuizScript.text = "";
        txtStage.text = "";
        isQuizLoad = false;

        //objAnswerType1Set.SetActive(false);
        objAnswerType2Set.SetActive(false);
        //objAnswerType3Set.SetActive(false);

        //isChooseO = false;
        //isChooseX = false;

        isChoose1 = false;
        isChoose2 = false;
        isChoose3 = false;
        isChoose4 = false;

        //isSubmitIpf = false;

        //ipfAnswer.text = "";

        isCorrect = false;

        objResultPopupSet.SetActive(false);
        objCorrectAnswer.SetActive(false);
        objWrongAnswer.SetActive(false);
        objTotalResult.SetActive(false);

        cnt_check = false;

        Debug.Log("초기화 진행");
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 8; i++)
        {
            RandomWeight.Add(i); // 처음에 문제의 번호 8개를 넣어준다.
        }

        for (int i = 0; i < 8; i++)
        {
            Debug.Log("RandomWeight : " + RandomWeight[i]); // 확인용
        }

        gameDataManager = GameObject.Find("GameDataManager_DontDestroy").GetComponent<GameDataManager>();  

        this.ImgHP = GameObject.Find("ImgHP");

        btnBackTitle.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Title 씬 로드");
        }); // Back to Menu

        btnRightUp.onClick.AddListener(() => 
        {
            isChoose1 = true;
        });
        btnRightDown.onClick.AddListener(() => 
        {
            isChoose2 = true;
        });
        btnLeftUp.onClick.AddListener(() => 
        {
            isChoose3 = true;
        });
        btnLeftDown.onClick.AddListener(() => 
        {
            isChoose4 = true;
        });

        btnResultConfirm.onClick.AddListener(() => 
        {
            // If isCorrect, isCorrect Count++;
            if (isCorrect)
            {
                nTotalCorrectAnswerCount += 1;
                isCorrect = false;
            }

            // If Game End, Go to Menu
            if (nQuizNowStep >= nQuizTotalCount)
            {
                SceneManager.LoadScene("Finish");
                Debug.Log("게임 끝 Finish 씬 로드");
            }


            else
            {
                NextQuizLoad();
            }
        });

        

        InitAll();

        Debug.Log(gameDataManager.data_Quiz.Count);

        // 퀴즈의 총 갯수는 default값으로 줘도됌.
        // 아니면 모드에서 몇문제인지 선택하게 해서 각각 씬으로 만들어도 될듯.
        nQuizTotalCount = gameDataManager.data_Quiz.Count;
        RandomMax = nQuizTotalCount;

        nQuizNowStep = 0;
        RandomPickControl = 0;

        txtAnswerDivTotal.text = "";
        txtTotalResult.text = "";
        nTotalCorrectAnswerCount = 0;
    }

    public void NextQuizLoad()
    {
        InitAll();

        nQuizNowStep += 1;

        if (nQuizNowStep >= nQuizTotalCount)
        {

            nTotalCorrectAnswerCountToInt = nTotalCorrectAnswerCount.ToString();
            nQuizTotalCountToInt = nQuizTotalCount.ToString();

            float a = float.Parse(nTotalCorrectAnswerCountToInt);
            float b = float.Parse(nQuizTotalCountToInt);

            txtAnswerDivTotal.text = "정답률 : " + (a / b) * 100 + "%";
            txtTotalResult.text = nTotalCorrectAnswerCount.ToString() + "/" + nQuizTotalCount.ToString() + " 정답";
            objTotalResult.SetActive(true);
            objResultPopupSet.SetActive(true);

            Debug.Log("RandomMax : " + RandomMax);
            Debug.Log("RandomWeight[8,9] : " + RandomWeight[8] + "/" + RandomWeight[9]);
            for (int k = 0; k < RandomMax; k++)
            {
                Debug.Log("Last Check : " + RandomWeight[k]); // 확인용
            }
        }

        // cnt = 목숨
        if (cnt > 2)
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Game End");

            Debug.Log("RandomMax : " + RandomMax);
            //Debug.Log("RandomWeight[8,9] : " + RandomWeight[8] + "/" + RandomWeight[9]);
            for (int k = 0; k < RandomMax; k++)
            {
                Debug.Log("Last Check : " + RandomWeight[k]); // 확인용
            }
        }
    }

    
    void Update()
    {
        if (nQuizNowStep < nQuizTotalCount)
        {
            
            if (isQuizLoad == false) // 퀴즈 셋팅 전
            {

                // 랜덤뽑기 함수
                //RandomPick = Random.Range(0, 8);
                
                RandomPick_ = Random.Range(0, RandomMax);
                RandomPick = (int)RandomWeight[RandomPick_];

                Debug.Log("RandomPick : " + RandomPick);

                txtQuizScript.text = gameDataManager.data_Quiz[RandomPick]["문제"].ToString();
                txtStage.text = "Now Stage : " + nQuizNowStep.ToString() + "/" + nQuizTotalCount.ToString();
                //switch (gameDataManager.data_Quiz[nQuizNowStep]["타입"].ToString())
                //{

                //case "2": // 타입2 - 객관식 퀴즈
                objAnswerType2Set.SetActive(true);

                        //RandomPickControl += 1;
                        //Debug.Log("UnloadRandomPick : " + RandomPick);                        
                        //Debug.Log("isQuizLoad : " + isQuizLoad.ToString());
                        //break;
                //}

                // 랜덤문제 뽑기 구현
                RandomPickControl = RandomPick;
                //Debug.Log("RandomPickControl : " + RandomPickControl);

                isQuizLoad = true;

            }
            else  // 퀴즈 셋팅 후
            {
                        if (isChoose1 || isChoose2 || isChoose3 || isChoose4)
                        {
                            objResultPopupSet.SetActive(true); // 이게 팝업창 뜨게해주는거

                            if ((gameDataManager.data_Quiz[RandomPickControl]["정답"].ToString() == "1" && isChoose1 == true) ||
                                (gameDataManager.data_Quiz[RandomPickControl]["정답"].ToString() == "2" && isChoose2 == true) ||
                                (gameDataManager.data_Quiz[RandomPickControl]["정답"].ToString() == "3" && isChoose3 == true) ||
                                (gameDataManager.data_Quiz[RandomPickControl]["정답"].ToString() == "4" && isChoose4 == true))
                            {
                                nTotalCorrectAnswerCount += 1;
                                objCorrectAnswer.SetActive(true);
                                isCorrect = true;

                                //Debug.Log("LoadRandomPick : " + RandomPick);
                                
                                // 이부분에 정답률 더 넣어줘도 될듯.
                                //txtAnswerDivTotal.text="정답률 : "+nTotalCorrectAnswerCount / nQuizTotalCount;
                                txtTotalResult.text = nTotalCorrectAnswerCount.ToString() + "/" + nQuizTotalCount.ToString() + " 정답";
                                if (nQuizNowStep >= nQuizTotalCount)
                                {
                                    SceneManager.LoadScene("Title");
                                    Debug.Log("게임 끝 Title 씬 로드");
                                }

                                NextQuizLoad();
                            }
                            else
                            {
                                
                                objWrongAnswer.SetActive(true);
                                //txtAnswerDivTotal.text="정답률 : " + nTotalCorrectAnswerCount / nQuizTotalCount;
                                txtTotalResult.text = nTotalCorrectAnswerCount.ToString() + "/" + nQuizTotalCount.ToString() + " 정답";
                                
                                if (cnt_check == false)
                                {
                                    cnt+=1;
                                    Debug.Log("cnt : " + cnt);
                                    DecreaseHP();
                                    cnt_check=true;

                                    RandomWeight.Add(RandomPickControl);
                                    Debug.Log("RandomWeight.Add : " + RandomPickControl);
                                    RandomMax += 1;
                                    Debug.Log("RandomMax : " + RandomMax);
                                }

                                

                                //NextQuizLoad();
                            }
                        }
            }
        }

    }
}