using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preassure : MonoBehaviour
{
    [SerializeField] GameObject porta;
    [SerializeField] GameObject pont;
    private bool activated = false;

    private void OnTriggerEnter(Collider box) {
        if(!activated){
            porta.transform.position += new Vector3(0, 4, 0);
            pont.transform.position += new Vector3(10, 0, 0);
            activated = true;
        }
            
    }
}
