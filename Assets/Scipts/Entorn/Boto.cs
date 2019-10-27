using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boto : MonoBehaviour
{
    [SerializeField]DoorTest porta;
    [SerializeField] bool mantenir; //True si s'ha de mantenir el boto per obrir la porta


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Grabable")
            porta.OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Grabable")
            if (mantenir) porta.CloseDoor();
    }
}
