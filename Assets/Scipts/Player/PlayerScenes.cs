using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScenes : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] PlayerCam camara;
    [SerializeField] GameObject CrossAir;
    // Start is called before the first frame update
    void Start()
    {
        menuPausa.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPausa.activeSelf) Pausar();
            else Renaudar();

        }
    }

    public void ResetejarEscena()
    {
        Renaudar();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Sortir()
    {
        Application.Quit();
    }
    
    public void TornarMenu()
    {
        Renaudar();
        SceneManager.LoadScene(0);
    }

    public void Pausar()
    {
        Time.timeScale = 0;
        menuPausa.SetActive(true);
        camara.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CrossAir.SetActive(false);
    }

    public void Renaudar()
    {
        Time.timeScale = 1;
        menuPausa.SetActive(false);
        camara.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CrossAir.SetActive(true);
    } 

    void SeguentEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
