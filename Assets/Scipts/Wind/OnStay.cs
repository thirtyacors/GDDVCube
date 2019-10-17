using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStay : MonoBehaviour
{
    private void OnTriggerStay(Collider obj)
    {
        if (obj.tag == "Grabable")
        {
            obj.transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
        if (obj.tag == "Player")
        {
            obj.transform.Translate(Vector3.forward * 5 * Time.deltaTime);
            obj.transform.position += Vector3.forward * 5 * Time.deltaTime;
        }
    }
}
