using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCollider : Interactables 
{
    BoxCollider boxCollider;
    private TimerManager timer;
    public bool timerActive;

    [SerializeField] private CG_Fade hud;

    private void Start()
    {
        timer = GameObject.FindGameObjectWithTag("TimerObject").GetComponent<TimerManager>();   
        boxCollider = GetComponent<BoxCollider>();
        timerActive = true; 
        if (!timer) Debug.Log($"Timer not found on {name}");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))   
        {
            Debug.Log("Timer Started"); 
            timer.TimerStart();
            timerActive = false;

            hud.gameObject.SetActive(true);
            hud.GetComponent<CG_Fade>().FadeIn();
        }
    }

    public void Update()
    {
        boxCollider.enabled = timerActive; 
    }
}
