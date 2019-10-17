using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private bool child;
    private GameObject collided;
    // Start is called before the first frame update
    void Start()
    {
        child = false;
        collided = null;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (collided != null && !child) // si té un fill el deixa anar
            {
                collided.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                collided.GetComponent<Rigidbody>().useGravity = false;
                collided.transform.parent = this.transform;
                collided.transform.localPosition = Vector3.zero;

                child = true;
            }
            else // si no te fill agafa l'objecte
            {
                collided = this.gameObject.transform.GetChild(0).gameObject;
                collided.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                collided.GetComponent<Rigidbody>().useGravity = true;
                collided.transform.parent = null;

                child = false;
            }
        }
    }
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Grabable")
        {
            collided = collider.gameObject;
        }
    }

    
    private void OnTriggerExit(Collider collider)
    {
        collided = null;
    }
    
}
