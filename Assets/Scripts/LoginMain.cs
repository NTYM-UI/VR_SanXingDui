using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;

public class LoginMain : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    protected static string ip = "127.0.0.1";
    protected static string port = "3306";
    protected static string database = "teachdb";
    protected static string user = "root";
    protected static string password = "123456";

    public static MySqlConnection mysql;

    public InputField NameInputField;
    public InputField PasswordInputField;
    public Text LoginPanelTileLable;

    private void Awake()
    {
        string connectionString = string.Format("server={0};port={1};database={2};user={3};password={4}",ip, port, database, user, password);
        mysql = new MySqlConnection(connectionString);
        Debug.Log("数据库连接成功！");
    }
    void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    //验证登录
    public void Login()
    {
        try
        {
            mysql.Open();
            string name1 = NameInputField.text;
            string password1 = PasswordInputField.text;
            MySqlCommand command = new MySqlCommand("select * from user where username=@usernsme and password=@password",mysql);
            command.Parameters.AddWithValue("username", name1);
            command.Parameters.AddWithValue("password", password1);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                LoginPanelTileLable.text = "欢迎你：" + name1.ToString() + "即将进入游戏...";
                StartCoroutine(ChangeToMainScene());
            }
            else
            {
                LoginPanelTileLable.text = "用户不存在，请检查用户名或密码...";
            }
        }
        finally
        {
            mysql.Close();
        }
    }
    //切换场景
    IEnumerator ChangeToMainScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneLoader.Instance.ChangeScene("Game");
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
