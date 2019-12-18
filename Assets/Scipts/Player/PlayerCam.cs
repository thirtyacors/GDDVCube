using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    //[SerializeField] Image imatgePoder;
    [SerializeField] Color[] colorsPoders;

    private GameObject grabCollider;

    GameObject player;
    Vector2 smoothedVelocity;
    Vector2 courrentLookingPos;

    int poderActual;

    private void Start() 
    {
        grabCollider = transform.Find("Grab Collider").gameObject;

        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CanviarPoder(0);
    }

    private void Update()
    {
        RotateCamera();
        CheckForShooting();
        ComprovarCanviPoder();
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        courrentLookingPos += smoothedVelocity;

        // myObject.GetComponent<MyScript>().MyFunction();
        print(grabCollider);
        if (grabCollider.GetComponent<PlayerGrab>().SomethingGrabbed() && courrentLookingPos.y < -20) courrentLookingPos.y = -20;
        else if (courrentLookingPos.y > 90) courrentLookingPos.y = 90;
        else if (courrentLookingPos.y < -90) courrentLookingPos.y = -90;

        transform.localRotation = Quaternion.AngleAxis(-courrentLookingPos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(courrentLookingPos.x, player.transform.up);

    }
    //Comprova si dispara a un cub
    private void CheckForShooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit whatIHit;
            if(Physics.Raycast(transform.position, transform.forward, out whatIHit,  Mathf.Infinity))
            {
                BoxActions damageable = whatIHit.collider.GetComponent<BoxActions>();
                if(damageable != null)
                {
                    if(!damageable.EstaAgafat())
                        damageable.AccioCaixa(poderActual, whatIHit.transform.InverseTransformDirection(whatIHit.normal)); // el numero depen de la posicio que porta l'arma
                }
            }
        }
    }

    private void ComprovarCanviPoder()
    {
        //Comprova cada tecla num per cada poder a la llista
        for(int i = 0;i<colorsPoders.Length;i++)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1+i)) CanviarPoder(i);
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            AugmentarPoderRoda();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            DisminuirPoderRoda();
        }
    }
    
    //Roda del ratoli
    private void AugmentarPoderRoda()
    {
        poderActual++;
        if (poderActual > colorsPoders.Length - 1)
        {
            poderActual = 0;
        }
        CanviarPoder(poderActual);
    }
    private void DisminuirPoderRoda()
    {
        poderActual--;
        if (poderActual < 0)
        {
            poderActual = colorsPoders.Length - 1;
        }
        CanviarPoder(poderActual);

    }

    //Canvia de poder actual
    private void CanviarPoder(int poder)
    {
        poderActual = poder;
        //imatgePoder.color = colorsPoders[poder];
    }
}
