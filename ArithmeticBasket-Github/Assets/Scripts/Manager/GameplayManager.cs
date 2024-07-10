using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    //This segment used to store this stage player data
    [Header("Player Data")]
    public int playerCurrentHealth;
    public int playerCorrectAnswer;

    //This segment used to hold necesarry variable to process the stage 
    [Header("Requirement")]
    [SerializeField]private List<Question> questionsList = new();
    private Question currentQuestion;
    [SerializeField]private GameObject answerPrefab;
    [SerializeField]private int stageBonusPoint;

    //This segment used to store all necessary UI
    [Header("UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public GameObject gameOverUI;
    public GameObject FinisihedUI;
    public Button gameOverMenuButton;
    public Button finishedMenuButton;

    //This segment used to store temporary data and data will be destroyed when its not needed anymore
    [Header("Temporary")]
    public List<GameObject> tempGameObjects = new();

    //This segment used to store and manage sfx
    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip correctAnswerAudioClip;
    public AudioClip wrongAnserAudioClip;

    private void Start() {
        playerCurrentHealth = 3;
        playerCorrectAnswer = 0;
        scoreText.text = playerCorrectAnswer.ToString();
        lifeText.text = playerCurrentHealth.ToString();
        gameOverMenuButton.onClick.AddListener(() => BackToMenuButton(0));
        finishedMenuButton.onClick.AddListener(() => BackToMenuButton(stageBonusPoint));
        SetupQuiz();
    }

    //Setup quiz for each question
    //Pick the question and remove question from list so player didnt get same question twice in one play
    //Start coroutine to make delay each answer instantiate
    public void SetupQuiz(){
        int randomIndexQuestion = Random.Range(0, questionsList.Count);
        Debug.Log(questionsList[randomIndexQuestion].quetsionDesc);
        questionText.text = questionsList[randomIndexQuestion].quetsionDesc;
        currentQuestion = questionsList[randomIndexQuestion];
        questionsList.RemoveAt(randomIndexQuestion);
        StartCoroutine(DelayForAnswerToSpawn(0));
    }

    //Function to check player answer, process answer, and check game is finished
    //After get player answer we check if the player answer correct, then update text, and setup quiz
    public void PlayerAnswer(int answerValue){
        Debug.Log("Player Choose " + answerValue + " as thier answer");
        if(currentQuestion.questionAnswer == answerValue){
            Debug.Log("Correct Answer");
            audioSource.clip = correctAnswerAudioClip;
            audioSource.Play();
            playerCorrectAnswer++;
        }else{
            Debug.Log("Wrong Answer");
            audioSource.clip = wrongAnserAudioClip;
            audioSource.Play();
            playerCurrentHealth--;
        }

        StopAllCoroutines();
        foreach (GameObject tempGameObject in tempGameObjects)
        {
            Destroy(tempGameObject);
        }

        tempGameObjects.RemoveAll(s => s == null);
        tempGameObjects.RemoveAt(0);

        scoreText.text = playerCorrectAnswer.ToString();
        lifeText.text = playerCurrentHealth.ToString();

        if(playerCorrectAnswer >= 5){
            Debug.Log("Game Finished");
            FinisihedUI.SetActive(true);
        }
        else if(playerCurrentHealth > 0){
            SetupQuiz();
        }
        else{
            Debug.Log("Game Over");
            Time.timeScale = 0;
            gameOverUI.SetActive(true);
        }
    }

    //Instantiate The answer
    public void InstantiateAnswer(int index){
        GameObject answerPrefabObject = Instantiate(answerPrefab, new Vector3(Random.Range(-2f, 2f), 4.5f,0f), Quaternion.identity);
        answerPrefabObject.GetComponent<Answer>().Setup(currentQuestion.questionOption[index]);
        tempGameObjects.Add(answerPrefabObject);
    }

    //Delay the instantiate answer
    private IEnumerator DelayForAnswerToSpawn(int index){
        yield return new WaitForSeconds(1f);
        InstantiateAnswer(index);
        yield return new WaitForSeconds(2f);
        if(index + 1 < currentQuestion.questionOption.Count){
            StartCoroutine(DelayForAnswerToSpawn(index + 1));
        }
    }

    //Function used for button to back to menu
    public void BackToMenuButton(int bonusPoint){
        GameManager.Instance.AddPoint(playerCorrectAnswer + bonusPoint);
        SceneManager.LoadScene("MainMenu");
    }
}
