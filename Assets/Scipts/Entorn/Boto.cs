using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boto : MonoBehaviour
{
    public DoorTest[] portes; 
    [SerializeField] bool mantenir; //True si s'ha de mantenir el boto per obrir la porta


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Grabable")
            for (int i = 0; i < portes.Length; i++)
            {
                portes[i].OpenDoor();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Grabable")
            for (int i = 0; i < portes.Length; i++)
            {
                 if (mantenir) portes[i].CloseDoor();
            }
    }
}
