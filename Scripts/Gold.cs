using UnityEngine;

public class Gold : MonoBehaviour, IUnitTarget
{
    public Transform Transform => transform;
    public bool IsActiveAndEnabled => isActiveAndEnabled;

    public void StartFollow(Transform target)
    {
        transform.parent = target;
    }

    public void StopFollow()
    {
        transform.parent = null;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

