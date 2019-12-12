using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private bool child;
    private GameObject collided;
    private BoxCollider colliderCubAgafat;
    private SphereCollider checkCub;
    // Start is called before the first frame update
    void Start()
    {
        child = false;
        collided = null;
        colliderCubAgafat = transform.parent.parent.gameObject.GetComponent<BoxCollider>();
        checkCub = GetComponent<SphereCollider>();
    }

    /*
     * PER EL PROBLEMA DE COLISIONS PROVAR AIXO:
     * EN comptes de posarho com a fill del grab que segueixi al grab sense passar a ser fill
        public Transform toFollow;
        private Vector3 offset;
 
        void Start()
        {
            offset = toFollow.position - transform.position;
        }
        void Update()
        {
            transform.position = toFollow.position - offset;
            transform.rotation = toFollow.rotation;
        }
     */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (!child) 
            {
                if (collided != null  && collided.GetComponent<BoxActions>().EsNormal())
                {
                    colliderCubAgafat.enabled = true;
                    Physics.IgnoreCollision(colliderCubAgafat, collided.GetComponent<BoxCollider>(), true);
                    Physics.IgnoreCollision(colliderCubAgafat, collided.transform.GetChild(0).GetComponent<BoxCollider>(), true);

                    collided.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    collided.GetComponent<Rigidbody>().useGravity = false;
                    collided.GetComponent<Rigidbody>().isKinematic = true;
                    collided.transform.parent = this.transform;
                    collided.transform.localPosition = Vector3.zero;
                    collided.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    checkCub.radius = 0;

                    collided.GetComponent<BoxActions>().Agafar(true);
                    child = true;
                }
            }
            else // si té un fill el deixa anar
            {
                print("DEIXAR");
                colliderCubAgafat.enabled = false;
                Physics.IgnoreCollision(colliderCubAgafat, collided.GetComponent<BoxCollider>(), false);
                Physics.IgnoreCollision(colliderCubAgafat, collided.transform.GetChild(0).GetComponent<BoxCollider>(), false);

                collided = this.gameObject.transform.GetChild(0).gameObject;
                collided.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                collided.GetComponent<Rigidbody>().useGravity = true;
                collided.GetComponent<Rigidbody>().isKinematic = false;
                collided.transform.parent = null;
                collided.transform.localRotation = Quaternion.Euler(0, 0, 0);

                checkCub.radius = 0.5f;

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
        if (collider.gameObject == collided)
        {
            colliderCubAgafat.enabled = false;
            Physics.IgnoreCollision(colliderCubAgafat, collided.GetComponent<BoxCollider>(), false);
            Physics.IgnoreCollision(colliderCubAgafat, collided.transform.GetChild(0).GetComponent<BoxCollider>(), false);

            collided = null;
            collider.GetComponent<BoxActions>().Agafar(false);
            child = false;
        }
    }
    
}
