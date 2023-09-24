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
    }
    public void MouseOver()
    {
        anim.SetBool("Selected", true);
    }
    public void MouseOff()
    {
        anim.SetBool("Selected", false);
    }

    IEnumerator PressDelay()
    {
        yield return new WaitForSeconds(0.4f);

        anim.SetBool("Pressed", false);
    }
}
