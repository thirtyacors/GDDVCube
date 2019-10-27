using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{

    Transform door;


    Vector3 closedPosition;
    Vector3 openedPosition;

    [SerializeField] float alcadaObert = 7;
    [SerializeField] float openSpeed = 5;
    private bool open = false;

    private void Start()
    {
        door = this.transform;
        closedPosition = door.position;
        openedPosition = door.position + new Vector3(0,alcadaObert,0);
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
    }

    public void OpenDoor()
    {
        open = true;

    }
}
