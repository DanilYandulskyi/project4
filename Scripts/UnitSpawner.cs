using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private BaseSpawner _baseSpawner;

    public int UnitPrice => _unit.Price;

    public Unit SpawnUnit(Vector3 position)
    {
        Unit unit = Instantiate(_unit, position, Quaternion.identity);
        unit.Initialize(_baseSpawner);

        return unit;
    }
}
