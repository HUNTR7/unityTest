using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static string userInput;
    public TMP_Text Input;

    

    public void Next()
    {
        SceneManager.LoadScene("OutputScene");
        userInput = Input.text;

    }
}
