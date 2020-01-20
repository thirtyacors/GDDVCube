using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxActions : MonoBehaviour
{

    const int NORMAL = -1, TERRA = 0, CHICLET = 1, VENT=2; 

    public int estatActual;

    public bool activarTerra = true;
    public bool activarChiclet = true;
    public bool activarVent = true;

    private bool enTerra = true;

    private Vector3 posActual, novaPos, scaleActual, nouScale;
    [SerializeField] Material[] material;
    [SerializeField] AudioClip [] sons;
    Renderer rend;
    Rigidbody rb;

    //Collider que detectera si s'ha d enganchar un cub
    [SerializeField] BoxCollider colliderChiclet;
    //Collider per empuchar
    [SerializeField] BoxCollider colliderVent;

    [SerializeField] float midaCreixer = 1;
    [SerializeField] float velocitatCreixer = 8;
    [SerializeField] float velocitatDecreixer = 8;

    [SerializeField] float min_pos = -20;
    [SerializeField] Vector3 reaparicio = new Vector3(0,0,0);

    private bool agafat, creixent, decreixent, estatic;

    private AudioSource so;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        so = GetComponent<AudioSource>();

        rend.sharedMaterial = material[0];
        estatActual = NORMAL;
        rb = GetComponent<Rigidbody>();
        agafat = false;
        creixent = decreixent = false;

        if (rb.isKinematic == true)  estatic = true;
    }

    private void Update()
    {
        posActual = transform.localPosition;
        if (creixent)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, novaPos, Time.deltaTime* velocitatCreixer);
            transform.localScale = Vector3.Lerp(transform.localScale, nouScale, Time.deltaTime* velocitatCreixer);
            if (transform.localScale == nouScale) creixent = false;
        }
        else if (decreixent)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posActual, Time.deltaTime* velocitatDecreixer);
            transform.localScale = Vector3.Lerp(transform.localScale, scaleActual, Time.deltaTime* velocitatDecreixer);
            if (transform.localScale == scaleActual)
            {
                decreixent = false;
                CanviarEstat(NORMAL);
            }
        }

        if (estatActual == CHICLET && enTerra) rb.isKinematic = true;
        else if (estatActual == CHICLET && !enTerra) rb.isKinematic = false;


        if (transform.position.y <= min_pos)
        {
            rb.velocity = new Vector3(0,0,0);
            transform.position = reaparicio;
        }
    }

    public bool EsNormal(){return estatActual == NORMAL;}

    public int EstatActual(){return estatActual;}

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
    public void AplicarVent(string costat, Vector3 dir)
    {
        if(activarVent)
        {
            //Si esta transformar en vent, treu el collider i el posa en estat normal
            if (estatActual != NORMAL)
            {
                so.Stop();
                so.loop = false;
                colliderVent.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                colliderVent.gameObject.SetActive(false);
                CanviarEstat(NORMAL);
            }
            else //Si esta en estat normal, activa el collider de vent i li passa la direccio per parametre. Mou al collider al costat corresponent
            {
                
                CanviarEstat(VENT);

                so.clip = sons[VENT];
                so.loop = true;
                so.Play();

                colliderVent.gameObject.SetActive(true);
                colliderVent.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
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
        
    }
    //------------------------------------------------------CHICLET-------------------------------------------------
    public void TransformarChiclet(string costat)
    {
        if(activarChiclet)
        {
            so.clip = sons[CHICLET];
            so.Play();
            //Si esta transformar en chiclet, treu el collider i el posa en estat normal
            if (estatActual != NORMAL)
            {
                colliderChiclet.size = new Vector3(0, 0, 0);
                colliderChiclet.isTrigger = false;
                CanviarEstat(NORMAL);
                if(!estatic) rb.isKinematic = false;
            }
            else // Si esta en estat normal, activa el collider d'enganchar i el posa en estat chiclet
            {
                colliderChiclet.size = new Vector3(1, 1, 1);
                colliderChiclet.isTrigger = true;
                CanviarEstat(CHICLET);
                if(!estatic) rb.isKinematic = true;
            }
        }
    }

    //------------------------------------------------------TERRA-------------------------------------------------
    public void AugmentarMida(string costat)
    {
        if(activarTerra)
        {
            so.clip = sons[TERRA];
            so.Play();

            if (estatActual != NORMAL)
            {
                //CanviarEstat(NORMAL);
                creixent = false;
                decreixent = true;
            }
            else
            {
                CanviarEstat(TERRA);
                scaleActual = transform.localScale;

                decreixent = false;

                if (costat == "OEST")
                {
                    float newPos = transform.localPosition.x - (midaCreixer/2);
                    novaPos = new Vector3(newPos, transform.localPosition.y, transform.localPosition.z);
                    nouScale = new Vector3(transform.localScale.x + midaCreixer, transform.localScale.y, transform.localScale.z);
                }
                else if (costat == "EST")
                {
                    float newPos = transform.localPosition.x + (midaCreixer / 2);
                    novaPos = new Vector3(newPos, transform.localPosition.y, transform.localPosition.z);
                    nouScale = new Vector3(transform.localScale.x + midaCreixer, transform.localScale.y, transform.localScale.z);
                }
                else if (costat == "SUD")
                {
                    float newPos = transform.localPosition.z - (midaCreixer / 2);
                    novaPos = new Vector3(transform.localPosition.x, transform.localPosition.y, newPos);
                    nouScale = new Vector3(transform.localScale.x , transform.localScale.y, transform.localScale.z + midaCreixer);
                }
                else if (costat == "NORD")
                {
                    float newPos = transform.localPosition.z + (midaCreixer / 2);
                    novaPos = new Vector3(transform.localPosition.x, transform.localPosition.y, newPos);
                    nouScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + midaCreixer);
                }
                else if (costat == "AMUNT")
                {
                    float newPos = transform.localPosition.y + (midaCreixer / 2);
                    novaPos = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
                    nouScale = new Vector3(transform.localScale.x, transform.localScale.y + midaCreixer, transform.localScale.z);
                }
                else if (costat == "ABAIX")
                {
                    float newPos = transform.localPosition.y + (midaCreixer / 2);
                    novaPos = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
                    nouScale = new Vector3(transform.localScale.x, transform.localScale.y + midaCreixer, transform.localScale.z);
                }
                creixent = true;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Terra")
        {
            enTerra = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Terra")
        {
            enTerra = false; 
        }
    }


}
