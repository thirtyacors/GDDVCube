using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public Vector3 direccioVent = new Vector3(0, 0, 0);
    public float forca = 2;

    private void OnTriggerStay(Collider obj)
    {
        if (obj.tag == "Grabable")
        {
            obj.transform.Translate(direccioVent * forca * Time.deltaTime);
        }
        if (obj.tag == "Player")
        {
            obj.transform.Translate(direccioVent * forca * Time.deltaTime);
            obj.transform.position += direccioVent * forca * Time.deltaTime;
        }
    }
}
