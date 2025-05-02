using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuizManager : MonoBehaviour
{
    //[Header("UI References")]
    //public TextMeshProUGUI questionTextUI;
    //public Button[] optionButtons;

    //[Header("Quiz Event")]
    //public UnityEvent<bool> onQuizComplete;

    //private QuizData currentQuizData;
    //private int currentQuestionIndex = 0;

    //private void Start()
    //{
    //    if (questionTextUI == null)
    //    {
    //        Debug.LogError("questionTextUI belum disambungkan!");
    //    }

    //    if (currentQuizData.questions == null || currentQuizData.questions.Length == 0)
    //    {
    //        Debug.LogError("QuizData tidak punya pertanyaan!");
    //    }

    //}

    //public void ShowQuiz(QuizData quizData)
    //{
    //    currentQuizData = quizData;
    //    currentQuestionIndex = 0;
    //    gameObject.SetActive(true);
    //    DisplayQuestion();
    //}

    //void DisplayQuestion()
    //{
    //    if (currentQuestionIndex >= currentQuizData.questions.Length)
    //    {
    //        FinishQuiz(true);
    //        return;
    //    }

    //    var question = currentQuizData.questions[currentQuestionIndex];
    //    questionTextUI.text = question.questionText;

    //    for (int i = 0; i < optionButtons.Length; i++)
    //    {
    //        if (i < question.options.Length)
    //        {
    //            optionButtons[i].gameObject.SetActive(true);
    //            optionButtons[i].GetComponentInChildren<Text>().text = question.options[i];

    //            int capturedIndex = i; // Hindari closure
    //            optionButtons[i].onClick.RemoveAllListeners();
    //            optionButtons[i].onClick.AddListener(() => CheckAnswer(capturedIndex));
    //        }
    //        else
    //        {
    //            optionButtons[i].gameObject.SetActive(false);
    //        }
    //    }
    //}

    //public void CheckAnswer(int index)
    //{
    //    if (index == currentQuizData.questions[currentQuestionIndex].correctAnswerIndex)
    //    {
    //        currentQuestionIndex++;
    //        DisplayQuestion();
    //    }
    //    else
    //    {
    //        FinishQuiz(false);
    //    }
    //}

    //void FinishQuiz(bool success)
    //{
    //    gameObject.SetActive(false);
    //    onQuizComplete?.Invoke(success);
    //}
}
