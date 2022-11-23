using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FinishManager : MonoBehaviour
{
    public Button btnClose;
    public Button btnReplay;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("gameclear");
        btnReplay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Title ¾À ·Îµå");
        });
        btnClose.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
