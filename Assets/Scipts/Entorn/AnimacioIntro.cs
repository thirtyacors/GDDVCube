using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacioIntro : MonoBehaviour
{
    BoxActions box;
    [SerializeField] float tempsEntreTransformacio = 2;
    int ultimaTransformacio = 0;
    float temps = 0;
    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.GetComponent<BoxActions>();
    }

    // Update is called once per frame
    void Update()
    {
        temps += Time.deltaTime;

        if(temps >= tempsEntreTransformacio)
        {
            ultimaTransformacio++;
            if (ultimaTransformacio > 4) ultimaTransformacio = 1;   
            switch (ultimaTransformacio)
            {
                case 1:
                    box.TransformarChiclet("NORD");
                    break;
                case 2:
                    box.TransformarChiclet("NORD");
                    box.AplicarVent("AMUNT", new Vector3(0, 0, 0));
                    break;
                case 3:
                    box.AplicarVent("NORD", new Vector3(0, 0, 0));
                    box.AugmentarMida("AMUNT");
                    break;
                case 4:
                    box.AugmentarMida("AMUNT");
                    break;
            }
            temps = 0;
        }
    }
}
