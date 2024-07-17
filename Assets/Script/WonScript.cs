using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class WonScript : MonoBehaviour
{
    public GameObject winScreen;
    public TMP_Text winScore;
    public void SetupWon(int score)
    {
        //Debug.Log("You won SETUP");
        winScreen.gameObject.SetActive(true);
        winScore.text = "Score: " + score.ToString();
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}