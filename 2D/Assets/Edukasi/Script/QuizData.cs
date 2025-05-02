using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/QuizData")]
public class QuizData : ScriptableObject
{
    public Question[] questions;
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] options;
    public int correctAnswerIndex;
}
