using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    public Flag SpawnFlag(Vector3 position)
    {
        return Instantiate(_flagPrefab, position, Quaternion.identity);
    }

}
