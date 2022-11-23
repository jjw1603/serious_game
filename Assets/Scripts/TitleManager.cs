using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    public GameObject objMenuPopupSet;
    public Button btnGameMode;
    public Button btnStoryMode;
    public Button btnMenuClose;

    public GameObject objSynopPopupSet;
    public Button btnNext;

    public GameObject objExplainPopupSet;
    public Button btnClose;

    public void InitAll()
    {
        objMenuPopupSet.SetActive(true);
        objSynopPopupSet.SetActive(false);
        objExplainPopupSet.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAll();
        btnGameMode.onClick.AddListener(() => // 게임모드 버튼 이벤트 설정
        {
            objExplainPopupSet.SetActive(true);
        });
        btnStoryMode.onClick.AddListener(() => // 스토리모드 버튼 이벤트 설정
        {
            objSynopPopupSet.SetActive(true);
        });
        btnMenuClose.onClick.AddListener(() => // 스토리모드 버튼 이벤트 설정
        {
            SceneManager.LoadScene("Main");
            Debug.Log("Main 씬 로드");
        });

        btnNext.onClick.AddListener(() => // 넥스트 버튼 이벤트 설정
        {
            objExplainPopupSet.SetActive(true);
        });
        btnClose.onClick.AddListener(() => // 클로즈 버튼 이벤트 설정
        {
            InitAll();
        });

        InitAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
