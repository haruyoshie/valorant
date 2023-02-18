using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPositionPlayer : MonoBehaviour
{
    public bool StayActive, deleteOptions;
    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCapsuleChamber")
        {
            Debug.Log("entro chamber");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "PlayerCapsuleChamber")
        {
            StayActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerCapsuleChamber")
        {
            StayActive = false;
        }
    }
    private void Update()
    {
        if(deleteOptions == true)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
