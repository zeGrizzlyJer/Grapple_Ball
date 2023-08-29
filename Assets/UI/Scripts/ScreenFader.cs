using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    private Animator anim;

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
            Debug.Log("FadeOff");
            anim.SetBool("Fade", false);
        }
        else
        {
            Debug.Log("FadeOn");
            anim.SetBool("Fade", true);
        }
    }
}
