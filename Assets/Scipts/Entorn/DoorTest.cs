using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorTest : MonoBehaviour
{

    Transform door;


    Vector3 closedPosition;
    Vector3 openedPosition;
    AudioSource so;

    public bool Pont;
    [SerializeField] float alcadaObert = 7;
    [SerializeField] float openSpeed = 5;
    private bool open = false;

    private void Start()
    {
        if(!Pont)so = GetComponent<AudioSource>();
        door = this.transform;
        closedPosition = door.position;
        if(Pont)openedPosition = door.position + new Vector3(alcadaObert,0,0);
        else openedPosition = door.position + new Vector3(0,alcadaObert,0);
    }
    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            door.position = Vector3.Lerp(door.position,
                openedPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            door.position = Vector3.Lerp(door.position,
                closedPosition, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.tag == "Player")
        {
            CloseDoor();
        }
    }
    public void CloseDoor()
    {
        open = false;
        if (!Pont) so.Play();
    }

    public void OpenDoor()
    {
        open = true;
        if (!Pont) so.Play();

    }
}
