using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinCollider : Interactables
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetSceneByName("MainMenu") != null) SceneManager.LoadScene("MainMenu");
        else Debug.Log("The scene entitled \"win\" does not exist.");
        Destroy(gameObject); 
    }
}
