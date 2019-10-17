using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;

    void Start()
    {
        speed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Grabable")
        {
            //obj.transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            obj.transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
    }
}
