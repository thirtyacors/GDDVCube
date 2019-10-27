using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{

    private bool endevant;
    private int size;
    private int posAct = 0;
    public Vector3[] positions; 
    public int stopSec; 
    
    [SerializeField] private bool stop;

private void Start() {
    size = positions.Length;
}

void Update(){

    if (transform.position == positions[size-1]) { 

        endevant = false; 
        if (stop)  {
            StartCoroutine("wait");
        }

    }
    else if (transform.position == positions[0])
    {
        endevant = true;
        if (stop)  {
            StartCoroutine("wait");
        }
        
    }
    if(endevant)
    {
        if(transform.position == positions[posAct]) posAct++;
        transform.position = Vector3.MoveTowards(transform.position, positions[posAct], 5 * Time.deltaTime);  
    } 
    else 
    {
        if(transform.position == positions[posAct]) posAct--;
        transform.position = Vector3.MoveTowards(transform.position, positions[posAct], 5 * Time.deltaTime);
    }

}

IEnumerator wait(){
    this.enabled = false;
    yield return new WaitForSeconds(stopSec);
    this.enabled = true;
}

}
