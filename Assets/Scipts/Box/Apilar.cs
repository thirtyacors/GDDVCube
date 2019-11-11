using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apilar : MonoBehaviour
{
    private void OnColliderEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Grabable" && !collider.transform.parent.GetComponent<BoxActions>().EstaAgafat())
        {
            collider.transform.localPosition = transform.parent.transform.localPosition + new Vector3(0,1,0);
        }
    }

    
}
