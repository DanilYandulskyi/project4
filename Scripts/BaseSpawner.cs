using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private ClickProcessor _clickProcessor;
    [SerializeField] private FlagHandler _flagHandler;
    [SerializeField] private GoldUIViewSpawner _goldUIViewSpawner;

    public Base SpawnBase(Vector3 position)
    {
        Base @base = Instantiate(_base, position, Quaternion.identity);
        @base.Initialize(_scanner, _unitSpawner, _flagHandler,
        _raycaster, _clickProcessor, _goldUIViewSpawner.Spawn(@base.transform));

        return @base;
    }
}
