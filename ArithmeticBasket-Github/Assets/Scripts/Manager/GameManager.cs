using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    //Addition
    //Subtraction
    //Multiplication
    //Division
    //Point
    //SFX
    //Music

    //0 = Locked
    //1 = Unlocked

    public static GameManager Instance { get; private set; }
    [SerializeField]private AudioMixer audioMixer;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        DontDestroyOnLoad(gameObject);
    }

    //Set volume based on paramater
    public void SetVolume(string volumeName, float value){
        audioMixer.SetFloat(volumeName, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(volumeName, value);
    }

    //Get volume based on paramater
    //return volume value
    public float GetVolume(string volumeName){
        return PlayerPrefs.GetFloat(volumeName, 0.5f);
    }

    //Unlock level based on paramater
    public void UnlockLevel(string KeyName, int Value){
        ReducePoint(Value);
        PlayerPrefs.SetInt(KeyName, 1);
    }

    //Check unlocked level based on paramater
    //return boolean value
    public bool CheckUnlockedLevel(string KeyName){
        int temp = PlayerPrefs.GetInt(KeyName, 0);
        if(temp == 1){
            return true;
        }else{
            return false;
        }
    }

    //Add point based on value
    public void AddPoint(int pointValue){
        int temp = PlayerPrefs.GetInt("Point", 0);
        temp += pointValue;
        PlayerPrefs.SetInt("Point", temp);
    }

    //Check point
    //return int
    public int CheckPoint(){
        return PlayerPrefs.GetInt("Point", 0);
    }

    //Reduce point
    public void ReducePoint(int pointValue){
        int temp = PlayerPrefs.GetInt("Point", 0);
        temp -= pointValue;
        PlayerPrefs.SetInt("Point", temp);
    }

    //Reset all data
    public void ResetData(){
        PlayerPrefs.SetInt("Addition", 0);
        PlayerPrefs.SetInt("Subtraction", 0);
        PlayerPrefs.SetInt("Multiplication", 0);
        PlayerPrefs.SetInt("Division", 0);
        PlayerPrefs.SetInt("Point", 0);
        PlayerPrefs.SetFloat("SFX", 0.5f);
        PlayerPrefs.SetFloat("Music", 0.5f);
    }

}
