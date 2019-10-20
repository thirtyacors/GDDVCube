using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;
     
    public Image imatgePoder; 


    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 courrentLookingPos;

    public Color[] colorsPoders;
    int poderActual;


    private void Start() 
    {   
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

        transform.localRotation = Quaternion.AngleAxis(-courrentLookingPos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(courrentLookingPos.x, player.transform.up);

    }
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
                    damageable.AccioCaixa(poderActual, whatIHit.transform.InverseTransformDirection(whatIHit.normal)); // el numero depen de la posicio que porta l'arma
                }
            }
        }
    }

    private void ComprovarCanviPoder()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1)) CanviarPoder(0);
        if (Input.GetKeyUp(KeyCode.Alpha2)) CanviarPoder(1);
        if (Input.GetKeyUp(KeyCode.Alpha3)) CanviarPoder(2);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            AugmentarPoderRoda();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            DisminuirPoderRoda();
        }
    }

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
    private void CanviarPoder(int poder)
    {
        poderActual = poder;
        imatgePoder.color = colorsPoders[poder];
    }
}
