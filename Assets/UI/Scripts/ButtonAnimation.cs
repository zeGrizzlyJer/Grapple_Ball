using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    private Animator anim;

    // ------------------------------------- //

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPress()
    {
        anim.SetBool("Pressed", true);
        FindObjectOfType<AudioManager>().Play("MouseClick");
        StartCoroutine(PressDelay());

        FindObjectOfType<AudioManager>().Play("ButtonPress");
    }
    public void MouseOver()
    {
        anim.SetBool("Selected", true);

        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }
    public void MouseOff()
    {
        anim.SetBool("Selected", false);
    }

    IEnumerator PressDelay()
    {
        yield return new WaitForSeconds(2f);

        anim.SetBool("Pressed", false);
    }
}
