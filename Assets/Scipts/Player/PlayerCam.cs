using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 courrentLookingPos;
    
    private void Start() 
    {   
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
        CheckForShooting();
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
                IDamageable damageable = whatIHit.collider.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.AccioCaixa(1, Direccio(whatIHit.transform.InverseTransformDirection(whatIHit.normal))); // el numero depen de la posicio que porta l'arma
                }
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
}
