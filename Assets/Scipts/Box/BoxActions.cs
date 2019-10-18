using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxActions : MonoBehaviour, IDamageable
{

    private string estatActual;
    private Vector3 posActual;
    public Material[] material;
    Renderer rend;
    Rigidbody rb;
    //Collider que detectera si s'ha d enganchar un cub
    public BoxCollider colliderChiclet;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        estatActual = "normal";
        rb = GetComponent<Rigidbody>();
    }

    public bool EsNormal()
    {
        return estatActual == "normal";
    }

    public void AccioCaixa(int accio, string costat)
    {
        switch (accio)
        {
            case 0:
                augmentarMida(costat);
                break;
            case 1:
                transformarChiclet(costat);
                break;

        }
    }

    void transformarChiclet(string costat)
    {
        //Si esta transformar en chiclet, treu el collider i el posa en estat normal
        if (estatActual == "transformarChiclet")
        {
            estatActual = "normal";
            rend.sharedMaterial = material[0];
            colliderChiclet.size = new Vector3(0, 0, 0);
        }
        else//Si esta en estat normal, activa el collider d'enganchar i el posa en estat chiclet
        {
            estatActual = "transformarChiclet";
            rend.sharedMaterial = material[2];
            colliderChiclet.size = new Vector3(1, 1, 1);
        }
    }
    public void augmentarMida(string costat)
    {
        if (estatActual == "augmentarMida")
        {
            transform.localScale = new Vector3(1, 1, 1);
            estatActual = "normal";
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
            estatActual = "augmentarMida";
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
}
