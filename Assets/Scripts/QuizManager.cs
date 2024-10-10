using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Lumin;

[System.Serializable]
public class Question
{
    public string question;
    public List<string> options;
    public string correctAnswer;
}

[System.Serializable]
public class QuizData
{
    public List<Question> questions;
}

public class QuizManager : Singleton<QuizManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject quizMenu;
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private TMP_Text answerText;
    private QuizData quizData;
    private Question currentQuestion;
    private List<string> currentAsnwers;
    private int selectedAnswer = 0;

    void Start()
    {
        LoadQuizData();
    }

    void Update()
    {

    }

    void LoadQuizData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("quizData");

        if (jsonFile != null)
        {
            quizData = JsonUtility.FromJson<QuizData>(jsonFile.text);
        }
        else
        {
            Debug.LogError("Quiz data file not found!");
        }
    }
    public void LoadQuestion()
    {
        currentQuestion = quizData.questions[UnityEngine.Random.Range(0, quizData.questions.Count)];
        currentAsnwers = currentQuestion.options.OrderBy(x => UnityEngine.Random.value).ToList();
        selectedAnswer = 0;
        answerText.text = currentAsnwers.First();
        questionText.text = currentQuestion.question;
    }

    public void ChooseAnswer()
    {
        if (currentQuestion != null && currentAsnwers.ElementAt(selectedAnswer) == currentQuestion.correctAnswer)
        {
            GameManager.Instance.SubmitAnswer(true);
            SoundManager.Instance.PlayEffect("correct");
        }
        else
        {
            GameManager.Instance.SubmitAnswer(false);
            SoundManager.Instance.PlayEffect("error");
        }
    }

    public void NextAnswer()
    {
        selectedAnswer = (selectedAnswer + 1) % currentAsnwers.Count;
        answerText.text = currentAsnwers.ElementAt(selectedAnswer);
        SoundManager.Instance.PlayEffect("pencil");
    }
    public void PreviousAnswer()
    {
        selectedAnswer = Math.Abs((selectedAnswer - 1) % currentAsnwers.Count);
        answerText.text = currentAsnwers.ElementAt(selectedAnswer);
        SoundManager.Instance.PlayEffect("pencil");
    }
    public void ShowQuiz()
    {
        quizMenu.SetActive(true);
        LoadQuestion();
    }
    public void HideQuiz()
    {
        quizMenu.SetActive(false);
    }
}
