using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{

    public TMP_Text pointText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointText.text = "Score: " + score.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
