using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Jugar()
    {
        SceneManager.LoadScene("Nivell_Creixer");
    }

    public void Sortir()
    {
        Application.Quit();
    }
}
