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
            if (!child) 
            {
                if (collided != null  && collided.GetComponent<BoxActions>().EsNormal())
                {
                    collided.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    collided.GetComponent<Rigidbody>().useGravity = false;
                    collided.GetComponent<Rigidbody>().isKinematic = true;
                    collided.transform.parent = this.transform;
                    collided.transform.localPosition = Vector3.zero;
                    collided.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    collided.GetComponent<BoxActions>().Agafar(true);
                    child = true;
                }
            }
            else // si té un fill el deixa anar
            {
                collided = this.gameObject.transform.GetChild(0).gameObject;
                collided.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                collided.GetComponent<Rigidbody>().useGravity = true;
                collided.GetComponent<Rigidbody>().isKinematic = false;
                collided.transform.parent = null;
                collided.transform.localRotation = Quaternion.Euler(0, 0, 0);

                collided.GetComponent<BoxActions>().Agafar(false);

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
        if (collider.gameObject.tag == "Grabable" || collider.gameObject.tag == "CubParet")
        {
            collided = null;
            collider.GetComponent<BoxActions>().Agafar(false);
            if(collider.gameObject.GetComponent<BoxActions>().estatActual == 1)child = false;
        }
    }
    
}
