using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxActions : MonoBehaviour
{

    private int estatActual;
    private Vector3 posActual;
    public Material[] material;
    Renderer rend;
    Rigidbody rb;
    //Collider que detectera si s'ha d enganchar un cub
    public BoxCollider colliderChiclet;
    public BoxCollider colliderVent;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        estatActual = -1;
        rb = GetComponent<Rigidbody>();
    }

    public bool EsNormal()
    {
        return estatActual == -1;
    }

    public void AccioCaixa(int accio, Vector3 direccio)
    {
        string costat = Direccio(direccio);
        if (estatActual == accio || estatActual == -1)
        { 
            switch (accio)
            {
                case 0:
                    augmentarMida(costat);
                    break;
                case 1:
                    transformarChiclet(costat);
                    break;
                case 2:
                    aplicarVent(costat, direccio);
                    break;

            }
        }
    }

    void aplicarVent(string costat, Vector3 dir)
    {
        //Si esta transformar en vent, treu el collider i el posa en estat normal
        if (estatActual == 2)
        {
            estatActual = -1;
            rend.sharedMaterial = material[0];
            colliderVent.gameObject.SetActive(false);
        }
        else//Si esta en estat normal, activa el collider de vent i li passa la direccio per parametre. Mou al collider al costat corresponent
        {
            estatActual = 2;
            rend.sharedMaterial = material[3];
            colliderVent.gameObject.SetActive(true);
            colliderVent.gameObject.GetComponent<OnStay>().direccioVent = dir;

            Debug.Log(costat);
            if (costat == "OEST")
            {
                colliderVent.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (costat == "EST")
            {
                colliderVent.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (costat == "SUD")
            {
                colliderVent.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);

            }
            else if (costat == "NORD")
            {
                colliderVent.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if (costat == "AMUNT")
            {
                colliderVent.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (costat == "ABAIX")
            {
                colliderVent.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }

    void transformarChiclet(string costat)
    {
        //Si esta transformar en chiclet, treu el collider i el posa en estat normal
        if (estatActual == 1)
        {
            estatActual = -1;
            rend.sharedMaterial = material[0];
            colliderChiclet.size = new Vector3(0, 0, 0);
        }
        else//Si esta en estat normal, activa el collider d'enganchar i el posa en estat chiclet
        {
            estatActual = 1;
            rend.sharedMaterial = material[2];
            colliderChiclet.size = new Vector3(1, 1, 1);
        }
    }
    public void augmentarMida(string costat)
    {
        if (estatActual == 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            estatActual = -1;
            rend.sharedMaterial = material[0];
            transform.localPosition = posActual;

        }
        else
        {
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
            estatActual = 0;
            rend.sharedMaterial = material[1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chiclet")
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chiclet")
        {
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
