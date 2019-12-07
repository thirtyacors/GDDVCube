using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa: MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [SerializeField] private PlayerCam camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { ActivarMenuPausa(); }
    }

    public void SeguentEscena()
    {
        ActivarMenuPausa();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ResetejarEscena()
    {
        ActivarMenuPausa();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Sortir()
    {
        Application.Quit();
    }

    public void TornarMenuPrincipal()
    {
        ActivarMenuPausa();
        SceneManager.LoadScene(0);
    }
    public void ActivarMenuPausa()
    {
        if (menu.activeSelf)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            camera.enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            camera.enabled = false;
        }
        
        menu.SetActive(!menu.activeSelf);
    }
}
