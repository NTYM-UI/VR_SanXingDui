using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMain : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void LoginPanelShow()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    public void RegisterPanelShow()
    {
        RegisterPanel.SetActive(true);
        LoginPanel.SetActive(false);
    }
}
