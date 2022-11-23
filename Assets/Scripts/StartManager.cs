using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button btnGameStart;
    public Button btnGameClose;

    // Start is called before the first frame update
    void Start()
    {
        btnGameStart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Title ¾À ·Îµå");
        });
        btnGameClose.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
