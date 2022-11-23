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
        btnGameMode.onClick.AddListener(() => // ���Ӹ�� ��ư �̺�Ʈ ����
        {
            objExplainPopupSet.SetActive(true);
        });
        btnStoryMode.onClick.AddListener(() => // ���丮��� ��ư �̺�Ʈ ����
        {
            objSynopPopupSet.SetActive(true);
        });
        btnMenuClose.onClick.AddListener(() => // ���丮��� ��ư �̺�Ʈ ����
        {
            SceneManager.LoadScene("Main");
            Debug.Log("Main �� �ε�");
        });

        btnNext.onClick.AddListener(() => // �ؽ�Ʈ ��ư �̺�Ʈ ����
        {
            objExplainPopupSet.SetActive(true);
        });
        btnClose.onClick.AddListener(() => // Ŭ���� ��ư �̺�Ʈ ����
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
