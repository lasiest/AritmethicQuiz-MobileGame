using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Addition
    //Subtraction
    //Multiplication
    //Division
    //Point

    //0 = Locked
    //1 = Unlocked

    public static GameManager Instance { get; private set; }

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

    public void UnlockLevel(string KeyName, int Value){
        ReducePoint(Value);
        PlayerPrefs.SetInt(KeyName, 1);
    }

    public bool CheckUnlockedLevel(string KeyName){
        int temp = PlayerPrefs.GetInt(KeyName, 0);
        if(temp == 1){
            return true;
        }else{
            return false;
        }
    }

    public void AddPoint(int pointValue){
        int temp = PlayerPrefs.GetInt("Point", 0);
        temp += pointValue;
        PlayerPrefs.SetInt("Point", temp);
    }

    public int CheckPoint(){
        return PlayerPrefs.GetInt("Point", 0);
    }

    public void ReducePoint(int pointValue){
        int temp = PlayerPrefs.GetInt("Point", 0);
        temp -= pointValue;
        PlayerPrefs.SetInt("Point", temp);
    }

    public void ResetData(){
        PlayerPrefs.SetInt("Addition", 0);
        PlayerPrefs.SetInt("Subtraction", 0);
        PlayerPrefs.SetInt("Multiplication", 0);
        PlayerPrefs.SetInt("Division", 0);
        PlayerPrefs.SetInt("Point", 0);
    }

}
