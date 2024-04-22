using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent ActionTriggerEnter;
    public UnityEvent ActionTriggerExit;
    public UnityEvent ActionTriggerStay;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter");
        ActionTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exit");
        ActionTriggerExit.Invoke();
    }

    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Stay");
        ActionTriggerStay.Invoke();
    }
}
