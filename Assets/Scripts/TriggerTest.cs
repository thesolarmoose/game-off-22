using UnityEngine;
using UnityEngine.Events;


public class TriggerTest : MonoBehaviour
{
    public UnityEvent _onEnter;
    public UnityEvent _onExit;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        _onEnter.Invoke();
    }
    
    public void OnTriggerExit2D(Collider2D collider)
    {
        _onExit.Invoke();
    }
}