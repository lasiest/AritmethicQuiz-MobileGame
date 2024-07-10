using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    public int answerValue;
    
    //Setup Answer value
    public void Setup(int answer){
        answerValue = answer;
        answerText.text = answer.ToString();
    }
}
