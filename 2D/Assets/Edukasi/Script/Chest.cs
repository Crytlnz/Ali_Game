using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [Header("Quiz Data")]
    public QuizData quizData; // drag QuizData unik untuk chest ini

    [Header("UI Komponen")]
    public GameObject quizPanel; // UI panel quiz (aktif/nonaktif)
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    private bool isPlayerInRange = false;
    private int currentQuestionIndex = 0;

    public UnityEvent onSuccess;
    public UnityEvent onFailed;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartQuiz();
        }
    }

    private void StartQuiz()
    {
        if (quizData == null || quizData.questions.Length == 0) return;

        currentQuestionIndex = 0;
        quizPanel.SetActive(true);
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        if (currentQuestionIndex >= quizData.questions.Length)
        {
            FinishQuiz(true);
            return;
        }

        var question = quizData.questions[currentQuestionIndex];
        questionText.text = question.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < question.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.options[i];

                int capturedIndex = i; // penting agar closure aman
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => CheckAnswer(capturedIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void CheckAnswer(int index)
    {
        bool correct = index == quizData.questions[currentQuestionIndex].correctAnswerIndex;

        if (correct)
        {
            currentQuestionIndex++;
            DisplayQuestion();
        }
        else
        {
            FinishQuiz(false);
        }
    }

    private void FinishQuiz(bool success)
    {
        quizPanel.SetActive(false);

        if (success)
        {
            Debug.Log("Quiz berhasil!");
            onSuccess?.Invoke();
           
            // Tambahkan efek membuka chest atau reward
        }
        else
        {
            Debug.Log("Jawaban salah.");
            onFailed?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
