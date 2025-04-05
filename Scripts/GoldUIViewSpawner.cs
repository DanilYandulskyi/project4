using UnityEngine;

public class GoldUIViewSpawner : MonoBehaviour
{
    [SerializeField] private GoldUIView _goldUIView;

    public GoldUIView Spawn(Transform transform)
    {
        return Instantiate(_goldUIView, transform);
    }
}
