using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex+1);
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
