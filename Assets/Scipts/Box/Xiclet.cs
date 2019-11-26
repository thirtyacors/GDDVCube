using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiclet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chiclet" && this.transform.parent.GetComponent<BoxActions>().EstatActual() == 1)
        {
            other.transform.parent.transform.parent = this.transform;
            other.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chiclet" && !other.transform.parent.GetComponent<BoxActions>().EstaAgafat())
        {
            other.transform.parent.transform.parent = null;
            other.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.transform.parent.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.parent.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
