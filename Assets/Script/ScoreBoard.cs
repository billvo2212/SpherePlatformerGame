using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //public GameOverScript gameOverScript;
    public TMP_Text pointText;
    public static ScoreManager instance;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text jumpText;
    private int score = 0;
    private int superJump = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        jumpText.text = "Super Jump: " + superJump.ToString();
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }

    public void updateSuperJump(int value)
    {   
        superJump  = value;
        jumpText.text = "Super Jump: " + superJump.ToString();
    }

    public int getScore()
    {
        return this.score;
    }
}
