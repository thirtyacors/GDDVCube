using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{

    [SerializeField] Material[] material;

    private int municio;
    private bool municioChanged;
    private GameObject carregador;
    private GameObject cablesCarregador;

    // Start is called before the first frame update
    void Start()
    {
        municio = 0;
        municioChanged = false;
        carregador = transform.Find("Carregador").gameObject;
        cablesCarregador = transform.Find("CablesCarregador").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            municio = 0;
            municioChanged = true;
        }
        else if (Input.GetKeyDown("2"))
        {
            municio = 1;
            municioChanged = true;
        }
        else if (Input.GetKeyDown("3"))
        {
            municio = 2;
            municioChanged = true;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            municio++;
            if (municio > 2) municio = 0;
            municioChanged = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            municio--;
            if (municio < 0) municio = 2;
            municioChanged = true;
        }

        if (municioChanged)
        {
            carregador.GetComponent<MeshRenderer>().material = material[municio];
            cablesCarregador.GetComponent<MeshRenderer>().material = material[municio];
            municioChanged = false;
        }
    }
}
