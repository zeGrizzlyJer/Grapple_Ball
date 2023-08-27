using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    private Animator anim;

    private bool faded;

    // --------------------------------//

    private void Start()
    {
        anim = GetComponent<Animator>();
        FadeScreen(false);
    }

    public void FadeScreen(bool i)
    {
        if (!i)
        {
            anim.SetBool("Fade", false);
        }
        else
        {
            anim.SetBool("Fade", true);
        }
    }
}
