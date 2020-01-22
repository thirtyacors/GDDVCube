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
        SceneManager.LoadScene(PlayerPrefs.GetInt("level",1));
    }

    public void Sortir()
    {
        Application.Quit();
    }
}
