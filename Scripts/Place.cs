using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private bool _isFull;

    public bool IsFull => _isFull;

    public void BecomeFull()
    {
        _isFull = true;
    }
}
