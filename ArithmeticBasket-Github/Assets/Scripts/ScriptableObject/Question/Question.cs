using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Object/Question")]
public class Question : ScriptableObject
{
    [Header("Requirement")]
    public EnumBundle.QuestionType questionType;
    public string quetsionDesc;

    public int questionAnswer;
    public List<int> questionOption = new();
}
