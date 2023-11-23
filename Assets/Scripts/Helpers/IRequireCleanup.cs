using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequireCleanup
{
    // OnDisable,
    // If GameManager hasn't cleaned up => CleanUp.
    // If GameManager has cleaned up => Don't CleanUp.
    public void OnDisable();

    // OnCleanup,
    // Remove all delegates
    public void OnCleanup();
}
