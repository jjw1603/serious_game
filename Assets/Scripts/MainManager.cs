using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [Header("����, ��Ÿ")] 
    public Button btnBackTitle;
    public Text txtQuizScript;
    public int nQuizTotalCount;
    public int nQuizNowStep;
    public bool isQuizLoad;
    public Text txtStage;

    //[Header("��� ���� - ���")]
    //public GameObject objAnswerType0set;
    //public Button L_btnUp;
    //public Button L_btnDown;
    //public Button R_btnUp;
    //public Button R_btnDown;

    //[Header("��� ���� - 1")]
    //public GameObject objAnswerType1Set;
    //public Button btnO;
    //public Button btnX;
    //public bool isChooseO;
    //public bool isChooseX;

    [Header("��� ���� - 2")]
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

    //[Header("��� ���� - 3")]
    //public GameObject objAnswerType3Set;
    //public InputField ipfAnswer;
    //public Button btnAnswerIpf;
    //public bool isSubmitIpf;

    [Header("����˾�â")]
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

    public ArrayList RandomWeight = new ArrayList(); // ����ġ �迭

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

        Debug.Log("�ʱ�ȭ ����");
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 8; i++)
        {
            RandomWeight.Add(i); // ó���� ������ ��ȣ 8���� �־��ش�.
        }

        for (int i = 0; i < 8; i++)
        {
            Debug.Log("RandomWeight : " + RandomWeight[i]); // Ȯ�ο�
        }

        gameDataManager = GameObject.Find("GameDataManager_DontDestroy").GetComponent<GameDataManager>();  

        this.ImgHP = GameObject.Find("ImgHP");

        btnBackTitle.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Title �� �ε�");
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
                SceneManager.LoadScene("Title");
                Debug.Log("���� �� Title �� �ε�");
            }


            else
            {
                NextQuizLoad();
            }
        });

        

        InitAll();

        Debug.Log(gameDataManager.data_Quiz.Count);

        // ������ �� ������ default������ �൵��.
        // �ƴϸ� ��忡�� ������� �����ϰ� �ؼ� ���� ������ ���� �ɵ�.
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

            txtAnswerDivTotal.text = "����� : " + (a / b) * 100 + "%";
            txtTotalResult.text = nTotalCorrectAnswerCount.ToString() + "/" + nQuizTotalCount.ToString() + " ����";
            objTotalResult.SetActive(true);
            objResultPopupSet.SetActive(true);

            Debug.Log("RandomMax : " + RandomMax);
            Debug.Log("RandomWeight[8,9] : " + RandomWeight[8] + "/" + RandomWeight[9]);
            for (int k = 0; k < RandomMax; k++)
            {
                Debug.Log("Last Check : " + RandomWeight[k]); // Ȯ�ο�
            }
        }

        // cnt = ���
        if (cnt > 2)
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Game End");

            Debug.Log("RandomMax : " + RandomMax);
            //Debug.Log("RandomWeight[8,9] : " + RandomWeight[8] + "/" + RandomWeight[9]);
            for (int k = 0; k < RandomMax; k++)
            {
                Debug.Log("Last Check : " + RandomWeight[k]); // Ȯ�ο�
            }
        }
    }

    
    void Update()
    {
        if (nQuizNowStep < nQuizTotalCount)
        {
            
            if (isQuizLoad == false) // ���� ���� ��
            {

                // �����̱� �Լ�
                //RandomPick = Random.Range(0, 8);
                
                RandomPick_ = Random.Range(0, RandomMax);
                RandomPick = (int)RandomWeight[RandomPick_];

                Debug.Log("RandomPick : " + RandomPick);

                txtQuizScript.text = gameDataManager.data_Quiz[RandomPick]["����"].ToString();
                txtStage.text = "Now Stage : " + nQuizNowStep.ToString() + "/" + nQuizTotalCount.ToString();
                //switch (gameDataManager.data_Quiz[nQuizNowStep]["Ÿ��"].ToString())
                //{

                //case "2": // Ÿ��2 - ������ ����
                objAnswerType2Set.SetActive(true);

                        //RandomPickControl += 1;
                        //Debug.Log("UnloadRandomPick : " + RandomPick);                        
                        //Debug.Log("isQuizLoad : " + isQuizLoad.ToString());
                        //break;
                //}

                // �������� �̱� ����
                RandomPickControl = RandomPick;
                //Debug.Log("RandomPickControl : " + RandomPickControl);

                isQuizLoad = true;

            }
            else  // ���� ���� ��
            {
                        if (isChoose1 || isChoose2 || isChoose3 || isChoose4)
                        {
                            objResultPopupSet.SetActive(true); // �̰� �˾�â �߰����ִ°�

                            if ((gameDataManager.data_Quiz[RandomPickControl]["����"].ToString() == "1" && isChoose1 == true) ||
                                (gameDataManager.data_Quiz[RandomPickControl]["����"].ToString() == "2" && isChoose2 == true) ||
                                (gameDataManager.data_Quiz[RandomPickControl]["����"].ToString() == "3" && isChoose3 == true) ||
                                (gameDataManager.data_Quiz[RandomPickControl]["����"].ToString() == "4" && isChoose4 == true))
                            {
                                nTotalCorrectAnswerCount += 1;
                                objCorrectAnswer.SetActive(true);
                                isCorrect = true;

                                //Debug.Log("LoadRandomPick : " + RandomPick);
                                
                                // �̺κп� ����� �� �־��൵ �ɵ�.
                                //txtAnswerDivTotal.text="����� : "+nTotalCorrectAnswerCount / nQuizTotalCount;
                                txtTotalResult.text = nTotalCorrectAnswerCount.ToString() + "/" + nQuizTotalCount.ToString() + " ����";
                                if (nQuizNowStep >= nQuizTotalCount)
                                {
                                    SceneManager.LoadScene("Title");
                                    Debug.Log("���� �� Title �� �ε�");
                                }

                                NextQuizLoad();
                            }
                            else
                            {
                                
                                objWrongAnswer.SetActive(true);
                                //txtAnswerDivTotal.text="����� : " + nTotalCorrectAnswerCount / nQuizTotalCount;
                                txtTotalResult.text = nTotalCorrectAnswerCount.ToString() + "/" + nQuizTotalCount.ToString() + " ����";
                                
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