using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour {

    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToshowCorrectAnswer = 10f;

    public float fillFraction;
    public bool isAnsweringQuestion;
    public bool loadNextQuestion;
    public bool stopTimer;

    float timerValue;

    void Start() {
        timerValue = timeToCompleteQuestion;
        isAnsweringQuestion = true;
        loadNextQuestion = false;
        stopTimer = false;
    }

    // Update is called once per frame
    void Update() {
        if (!stopTimer) {
            UpdateTimer();
        }

    }

    void UpdateTimer() {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion) {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToCompleteQuestion;
            } else {
                isAnsweringQuestion = !isAnsweringQuestion;
                timerValue = timeToshowCorrectAnswer;
            }
        } else {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToshowCorrectAnswer;
            } else {
                isAnsweringQuestion = !isAnsweringQuestion;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }

    public void CancelTimer() {
        isAnsweringQuestion = false;
        timerValue = timeToshowCorrectAnswer;
    }
}
