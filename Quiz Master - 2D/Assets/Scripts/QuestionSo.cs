using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Qestion", fileName = "New Question")]
public class QuestionSo : ScriptableObject {

    [TextArea(2, 6)]
    [SerializeField] String question = "Enter new question here";
    [SerializeField] String[] answers = new String[4];
    [SerializeField] int correctAnswerIndex;

    public String GetQuestion() {
        return question;
    }

    public int GetCorrectAnswerIndex() {
        return correctAnswerIndex;
    }

    public String GetAnswer(int index) {
        return answers[index];
    }
}
