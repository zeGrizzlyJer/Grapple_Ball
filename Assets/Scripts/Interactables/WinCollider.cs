using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinCollider : Interactables
{
    [SerializeField] private string sceneName = "MainMenu"; 

    protected override void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetSceneByName(sceneName) != null) SceneManager.LoadScene(sceneName);
        else Debug.Log("The scene entitled \"win\" does not exist.");
        Destroy(gameObject); 
    }
}
