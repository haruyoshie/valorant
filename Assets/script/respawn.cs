using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
   public Transform ResetPosition;
    Vector3 finalPos;
    public CharacterController controller;
    public GameObject player;

    private void Start()
    {
        finalPos = ResetPosition.position;
    }
   
    private void OnTriggerEnter(Collider other)
    {
       player = other.gameObject;
       controller = other.GetComponent<CharacterController>();
       controller.enabled = false;
       PlayerPosFinal();
    }
    public void PlayerPosFinal()
    {
        player.transform.position = finalPos;
        controller.enabled = true;
    }
}
