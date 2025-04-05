using UnityEngine;
using System;

public class ClickProcessor : MonoBehaviour
{
    public event Action Clicked;

    private void OnMouseDown()
    {
        Clicked?.Invoke();
    }
}
