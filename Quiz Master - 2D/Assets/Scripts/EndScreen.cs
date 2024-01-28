using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        ShowFinalScore();
    }

    public void ShowFinalScore() {
        finalScoreText.text = "Congratulations!\nYou got a score of " +
                                scoreKeeper.CalculateScore() + "%";
    }
}
