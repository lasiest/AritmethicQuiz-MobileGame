using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler
{
    GameObject gameManagerObject;

    private void Start() {
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
    }

    //onclick event
    public void OnPointerClick(PointerEventData eventData)
    {
        gameManagerObject.GetComponent<GameplayManager>().PlayerAnswer(GetComponent<Answer>().answerValue);
        Destroy(gameObject);
    }
}
