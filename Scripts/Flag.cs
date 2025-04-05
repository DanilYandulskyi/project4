using UnityEngine;
using System;

public class Flag : MonoBehaviour, IUnitTarget
{
    public Transform Transform => transform;

    public event Action Disabled;
    
    public void Disable()
    {
        Disabled?.Invoke();
        gameObject.SetActive(false);
    }
}
