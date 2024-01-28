using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Quiz quiz;
    EndScreen endScreen;

    // Start is called before the first frame update
    void Awake() {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (quiz.isQuizFinished) {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
