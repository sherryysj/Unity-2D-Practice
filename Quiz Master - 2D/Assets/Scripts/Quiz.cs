using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class Quiz : MonoBehaviour {

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSo question;
    [SerializeField] List<QuestionSo> questions;
    int currentQuestionIndex = 0;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly;

    [Header("Button colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress bar")]
    [SerializeField] Slider progressBar;
    public bool isQuizFinished = false;

    // Start is called before the first frame update
    void Start() {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        question = questions[currentQuestionIndex];
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        DisplayQuestion();
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion) {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            DisplayCorrectAnswer(-1); // input a wrong index
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        DisplayCorrectAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    public void DisplayCorrectAnswer(int index) {
        int correctAnswerIndex = question.GetCorrectAnswerIndex();
        Image buttonImage;
        if (index == correctAnswerIndex) {
            scoreKeeper.IncrementCorrectAnswers();
            questionText.text = "Correct Answer!";
        } else {
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was:\n" + correctAnswer;
        }
        scoreKeeper.IncrementQuestionsSeen();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

    void GetNextQuestion() {
        currentQuestionIndex++;
        progressBar.value++;
        if (currentQuestionIndex < questions.Count) {
            question = questions[currentQuestionIndex];
            DisplayQuestion();
            SetButtonState(true);
        } else {
            timer.stopTimer = true;
            isQuizFinished = true;
        }
    }

    void DisplayQuestion() {
        questionText.text = question.GetQuestion();
        for (int i = 0; i < 4; i++) {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetAnswer(i);
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
            answerButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    void SetButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }
}
