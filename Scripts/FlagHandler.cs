using UnityEngine;

public class FlagHandler : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private FlagSpawner _flagSpawner;

    private Flag _spawnedFlag;

    public bool IsFlagSet => _spawnedFlag != null;
    public Flag Flag => _spawnedFlag;

    public void SetFlag(Vector3 position)
    {
        if (IsFlagSet)
        {
            SetFlagPosition(position);
        }
        else
        {
            _spawnedFlag = _flagSpawner.SpawnFlag(position);
        }
    }

    public void SetFlagPosition(Vector3 position)
    {
        _spawnedFlag.gameObject.SetActive(true);
        _spawnedFlag.transform.position = position;
    }
}
