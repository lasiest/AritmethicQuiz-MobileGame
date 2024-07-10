using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    //Store GameObject parent each page
    [Header("Page")]
    public GameObject mainPage;
    public GameObject levelSelectionPage;
    public GameObject settingPage;

    //Store GameObject of locked level
    [Header("Locked Level")]
    public GameObject additionLockedGameObject;
    public GameObject subtractionLockedGameObject;
    public GameObject multiplicationLockedGameObject;
    public GameObject divisionLockedGameObject;

    //Store general button that is exist in more than 1 page
    [Header("General Button")]
    public Button[] backButtons;

    //Store button that only exist in Main Page
    [Header("Main Page Button")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;

    //Store button that only exist in Level Selection
    [Header("Level Selection Button")]
    public Button resetDataButton;
    public Button additionButton;
    public Button subtractionButton;
    public Button multiplicationButton;
    public Button divisionButton;

    public Button unlockAdditionButton;
    public Button unlockSubtractionButton;
    public Button unlockMultiplicationButton;
    public Button unlockDivisionButton;

    //Store slider for settings
    [Header("Setting Slider")]
    public Slider sfxSlider;
    public Slider musicSlider;

    //Store variable to manage point text
    [Header("Text UI")]
    public TextMeshProUGUI pointText;

    private void Start() {
        startButton.onClick.AddListener(OpenLevelSelectionPage);
        settingButton.onClick.AddListener(OpenSettingsPage);
        quitButton.onClick.AddListener(QuitApplication);
        foreach (Button backButton in backButtons)
        {
            backButton.onClick.AddListener(OpenMainPage);
        }
        additionButton.onClick.AddListener(()=> ChangeScene("AdditionGameplay"));
        subtractionButton.onClick.AddListener(() => ChangeScene("SubtractionGameplay"));
        multiplicationButton.onClick.AddListener(() => ChangeScene("MultiplicationGameplay"));
        divisionButton.onClick.AddListener(()=> ChangeScene("DivisionGameplay"));
        unlockAdditionButton.onClick.AddListener(() => UnlockStage(0, "Addition"));
        unlockSubtractionButton.onClick.AddListener(() => UnlockStage(10, "Subtraction"));
        unlockMultiplicationButton.onClick.AddListener(() => UnlockStage(25, "Multiplication"));
        unlockDivisionButton.onClick.AddListener(()=> UnlockStage(25, "Division"));

        sfxSlider.onValueChanged.AddListener(delegate {ChangeVolumeValue("SFX", sfxSlider.value); });
        musicSlider.onValueChanged.AddListener(delegate {ChangeVolumeValue("Music", musicSlider.value); });

        resetDataButton.onClick.AddListener(ResetData);
        Setup();
    }

    //Setup Main menu
    private void Setup(){
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
        sfxSlider.value = GameManager.Instance.GetVolume("SFX");
        musicSlider.value = GameManager.Instance.GetVolume("Music");

    }

    //Open Level selection page
    public void OpenLevelSelectionPage(){
        mainPage.SetActive(false);
        levelSelectionPage.SetActive(true);
        settingPage.SetActive(false);
    }

    //Open Main page
    public void OpenMainPage(){
        mainPage.SetActive(true);
        levelSelectionPage.SetActive(false);
        settingPage.SetActive(false);
    }

    //Open Settings Page
    public void OpenSettingsPage(){
        mainPage.SetActive(false);
        levelSelectionPage.SetActive(false);
        settingPage.SetActive(true);
    }

    //Change scene to gameplay
    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    //Unlock locked stage
    public void UnlockStage(int pointRequired, string levelName){
        if(GameManager.Instance.CheckPoint() >= pointRequired){
            GameManager.Instance.UnlockLevel(levelName, pointRequired);
            Setup();
        }
    }

    //Change volume
    public void ChangeVolumeValue(string volumeName, float value){
        GameManager.Instance.SetVolume(volumeName, value);
    }

    //Reset data for testing
    public void ResetData(){
        GameManager.Instance.ResetData();
        Setup();
    }

    //Quit Application
    public void QuitApplication(){
        Application.Quit();
    }
}
