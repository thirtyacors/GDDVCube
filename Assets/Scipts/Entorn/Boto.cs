using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boto : MonoBehaviour
{
    public DoorTest[] portes; 
    [SerializeField] bool mantenir; //True si s'ha de mantenir el boto per obrir la porta
    private int colisions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Grabable")
        {
            colisions++;
            if (colisions == 1)
            {
                for (int i = 0; i < portes.Length; i++)
                {
                    portes[i].OpenDoor();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (mantenir)
        {
            if (other.tag == "Player" || other.tag == "Grabable")
            {
                colisions--;
                if (colisions == 0)
                {
                    for (int i = 0; i < portes.Length; i++)
                    {
                        portes[i].CloseDoor();
                    }
                }
            }
        }
    }
}
