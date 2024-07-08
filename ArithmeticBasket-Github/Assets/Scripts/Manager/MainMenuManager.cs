using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Page")]
    public GameObject mainPage;
    public GameObject levelSelectionPage;

    [Header("Locked Level")]
    public GameObject additionLockedGameObject;
    public GameObject subtractionLockedGameObject;
    public GameObject multiplicationLockedGameObject;
    public GameObject divisionLockedGameObject;

    [Header("Main Page Button")]
    public Button startButton;
    public Button quitButton;

    [Header("Level Selection Button")]
    public Button backButton;

    public Button resetDataButton;

    public Button additionButton;
    public Button subtractionButton;
    public Button multiplicationButton;
    public Button divisionButton;

    public Button unlockAdditionButton;
    public Button unlockSubtractionButton;
    public Button unlockMultiplicationButton;
    public Button unlockDivisionButton;

    [Header("Text UI")]
    public TextMeshProUGUI pointText;

    private void Start() {
        startButton.onClick.AddListener(OpenLevelSelectionPage);
        quitButton.onClick.AddListener(QuitApplication);
        backButton.onClick.AddListener(OpenMainPage);
        additionButton.onClick.AddListener(()=> ChangeScene("AdditionGameplay"));
        subtractionButton.onClick.AddListener(() => ChangeScene("SubtractionGameplay"));
        multiplicationButton.onClick.AddListener(() => ChangeScene("MultiplicationGameplay"));
        divisionButton.onClick.AddListener(()=> ChangeScene("DivisionGameplay"));
        unlockAdditionButton.onClick.AddListener(() => UnlockStage(0, "Addition"));
        unlockSubtractionButton.onClick.AddListener(() => UnlockStage(10, "Subtraction"));
        unlockMultiplicationButton.onClick.AddListener(() => UnlockStage(25, "Multiplication"));
        unlockDivisionButton.onClick.AddListener(()=> UnlockStage(25, "Division"));
        resetDataButton.onClick.AddListener(ResetData);
        SetupLockedLevel();
    }

    private void SetupLockedLevel(){
        pointText.text = GameManager.Instance.CheckPoint().ToString();
        additionLockedGameObject.SetActive(true);
        subtractionLockedGameObject.SetActive(true);
        multiplicationLockedGameObject.SetActive(true);
        divisionLockedGameObject.SetActive(true);
        if(GameManager.Instance.CheckUnlockedLevel("Addition")){
            additionLockedGameObject.SetActive(false);
        }
        if(GameManager.Instance.CheckUnlockedLevel("Subtraction")){
            subtractionLockedGameObject.SetActive(false);
        }
        if(GameManager.Instance.CheckUnlockedLevel("Multiplication")){
            multiplicationLockedGameObject.SetActive(false);
        }
        if(GameManager.Instance.CheckUnlockedLevel("Division")){
            divisionLockedGameObject.SetActive(false);
        }
    }

    public void OpenLevelSelectionPage(){
        mainPage.SetActive(false);
        levelSelectionPage.SetActive(true);
    }

    public void OpenMainPage(){
        mainPage.SetActive(true);
        levelSelectionPage.SetActive(false);
    }

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void UnlockStage(int pointRequired, string levelName){
        if(GameManager.Instance.CheckPoint() >= pointRequired){
            GameManager.Instance.UnlockLevel(levelName, pointRequired);
            SetupLockedLevel();
        }
    }

    public void ResetData(){
        GameManager.Instance.ResetData();
        SetupLockedLevel();
    }

    public void QuitApplication(){
        Application.Quit();
    }

}
