using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxActions : MonoBehaviour
{

    const int NORMAL = -1, TERRA = 0, CHICLET = 1, VENT=2; 

    private int estatActual;

    private Vector3 posActual;
    [SerializeField] Material[] material;
    Renderer rend;
    Rigidbody rb;

    //Collider que detectera si s'ha d enganchar un cub
    [SerializeField] BoxCollider colliderChiclet;
    //Collider per empuchar
    [SerializeField] BoxCollider colliderVent;

    private bool agafat;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        rend.sharedMaterial = material[0];
        estatActual = NORMAL;
        rb = GetComponent<Rigidbody>();
        agafat = false;
    }

    public bool EsNormal(){return estatActual == NORMAL;}

    public void Agafar(bool agafar) {agafat = agafar;}

    public bool EstaAgafat() { return agafat;}

    public void AccioCaixa(int accio, Vector3 direccio)
    {
        string costat = Direccio(direccio);
        if (estatActual == accio || estatActual == NORMAL)
        {
            switch (accio)
            {
                case TERRA:
                    AugmentarMida(costat);
                    break;
                case CHICLET:
                    TransformarChiclet(costat);
                    break;
                case VENT:
                    AplicarVent(costat, direccio);
                    break;
            }
        }
    }

    void CanviarEstat(int estat)
    {
        estatActual = estat;
        rend.sharedMaterial = material[estat+1];
    }
    
    //------------------------------------------------------VENT-------------------------------------------------
    void AplicarVent(string costat, Vector3 dir)
    {
        //Si esta transformar en vent, treu el collider i el posa en estat normal
        if (estatActual != NORMAL)
        {
            colliderVent.gameObject.SetActive(false);
            CanviarEstat(NORMAL);
        }
        else //Si esta en estat normal, activa el collider de vent i li passa la direccio per parametre. Mou al collider al costat corresponent
        {
            CanviarEstat(VENT);
            colliderVent.gameObject.SetActive(true);
            colliderVent.gameObject.GetComponent<Vent>().direccioVent = dir;

            if (costat == "OEST")
            {
                colliderVent.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (costat == "EST")
            {
                colliderVent.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (costat == "SUD")
            {
                colliderVent.gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);

            }
            else if (costat == "NORD")
            {
                colliderVent.gameObject.transform.localRotation = Quaternion.Euler(0, 270, 0);
            }
            else if (costat == "AMUNT")
            {
                colliderVent.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (costat == "ABAIX")
            {
                colliderVent.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }
    //------------------------------------------------------CHICLET-------------------------------------------------
    void TransformarChiclet(string costat)
    {
        //Si esta transformar en chiclet, treu el collider i el posa en estat normal
        if (estatActual != NORMAL)
        {
            colliderChiclet.size = new Vector3(0, 0, 0);
            CanviarEstat(NORMAL);
        }
        else//Si esta en estat normal, activa el collider d'enganchar i el posa en estat chiclet
        {
            colliderChiclet.size = new Vector3(1, 1, 1);
            CanviarEstat(CHICLET);
        }
    }

    //------------------------------------------------------TERRA-------------------------------------------------
    public void AugmentarMida(string costat)
    {
        if (estatActual != NORMAL)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localPosition = posActual;
            CanviarEstat(NORMAL);
        }
        else
        {
            CanviarEstat(TERRA);
            posActual = transform.localPosition;
            if (costat == "OEST")
            {
                float newPos = transform.localPosition.x - (float)0.5;
                transform.localPosition = new Vector3(newPos, transform.localPosition.y, transform.localPosition.z);
                transform.localScale = new Vector3(2, 1, 1);
            }
            else if (costat == "EST")
            {
                float newPos = transform.localPosition.x + (float)0.5;
                transform.localPosition = new Vector3(newPos, transform.localPosition.y, transform.localPosition.z);
                transform.localScale = new Vector3(2, 1, 1);
            }
            else if (costat == "SUD")
            {
                float newPos = transform.localPosition.z - (float)0.5;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newPos);
                transform.localScale = new Vector3(1, 1, 2);
            }
            else if (costat == "NORD")
            {
                float newPos = transform.localPosition.z + (float)0.5;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newPos);
                transform.localScale = new Vector3(1, 1, 2);
            }
            else if (costat == "AMUNT")
            {
                float newPos = transform.localPosition.y - (float)0.5;
                transform.localPosition = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
                transform.localScale = new Vector3(1, 2, 1);
            }
            else if (costat == "ABAIX")
            {
                float newPos = transform.localPosition.y + (float)0.5;
                transform.localPosition = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
                transform.localScale = new Vector3(1, 2, 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chiclet" && estatActual == CHICLET)
        {
            other.transform.parent.transform.parent = this.transform;
            other.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.transform.parent.GetComponent<Rigidbody>().useGravity = false;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chiclet" && !other.transform.parent.GetComponent<BoxActions>().EstaAgafat())
        {
            other.transform.parent.transform.parent = null;
            other.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.transform.parent.GetComponent<Rigidbody>().useGravity = true;
            rb.isKinematic = false;
        }
    }

    public string Direccio(Vector3 dir)
    {
        string direccio = "ERROR?";
        if ((int)dir.x == 1) direccio = "EST";

        else if ((int)dir.x == -1) direccio = "OEST";

        else if ((int)dir.y == 1) direccio = "AMUNT";

        else if ((int)dir.y == -1) direccio = "ABAIX";

        else if ((int)dir.z == 1) direccio = "NORD";

        else if ((int)dir.z == -1) direccio = "SUD";

        return direccio;
    }
}
