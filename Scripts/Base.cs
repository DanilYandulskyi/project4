using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units = new List<Unit>();
    [SerializeField] private List<Place> _places = new List<Place>();
    [SerializeField] private List<Gold> _collectedGold = new List<Gold>();

    [SerializeField] private Scanner _scanner;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private ClickProcessor _clickProcessor;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private FlagHandler _flagHandler;
    [SerializeField] private GoldUIView _goldUIView;

    [SerializeField] private int _priceToSpawn;

    private int _unitsMaxCount;
    private bool _isSelected;
    private bool _isFlagTaken = false;

    private void Start()
    {
        _unitsMaxCount = _places.Count;

        _clickProcessor.Clicked += SetFlag;

        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].CollectedGold += CollectGold;

            Place place = GetFreePlace();

            _units[i].SetInitialPosition(place.transform.position);
            place.BecomeFull();
        }
    }

    private void OnMouseDown()
    {
        _isSelected = !_isSelected;
    }

    private void Update()
    {
        Unit unit = GetFreeUnit();

        if (unit != null)
        {
            if (_scanner.Gold.Count > 0 && (_isFlagTaken || _units.Count == 1 || _flagHandler.IsFlagSet == false || _collectedGold.Count < _priceToSpawn))
            {
                Gold gold = _scanner.Gold[0];
                _scanner.RemoveGold(gold);
                unit.SetGold(gold);
            }
            else if (_isFlagTaken == false && _units.Count > 1 && _flagHandler.IsFlagSet && _collectedGold.Count >= _priceToSpawn)
            {
                 unit.SetFlag(_flagHandler.Flag);
                _units.Remove(unit);
                _isFlagTaken = true;

                unit.CollectedGold -= CollectGold;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Unit unit in _units)
        {
            unit.CollectedGold -= CollectGold;
        }

        _clickProcessor.Clicked -= SetFlag;
    }

    public void Initialize(Scanner scanner, UnitSpawner unitSpawner, FlagHandler flagHandler, Raycaster raycaster, ClickProcessor clickProcessor, GoldUIView goldUIView)
    {
        _scanner = scanner;
        _unitSpawner = unitSpawner;
        _flagHandler = flagHandler;
        _raycaster = raycaster;
        _clickProcessor = clickProcessor;
        _goldUIView = goldUIView;
    }

    public void Assign(Unit unit)
    {
        _units.Add(unit);
        
        unit.CollectedGold += CollectGold;

        _units[_units.Count - 1].SetInitialPosition(GetFreePlace().transform.position);
    }

    public void SetFlag()
    {
        if (_isSelected)
        {
            Vector3 flagSetPosition;

            if (_raycaster.Cast(Input.mousePosition, out flagSetPosition))
            {
                _flagHandler.SetFlag(new Vector3(flagSetPosition.x, flagSetPosition.y, flagSetPosition.z));
                _isSelected = false;
            }
        }
    }

    private void CollectGold(Gold gold)
    {
        if (_collectedGold.Contains(gold) == false && gold != null)
        {
            _collectedGold.Add(gold);
            _goldUIView.UpdateText(_collectedGold.Count);

            if (_unitSpawner.UnitPrice <= _collectedGold.Count && (_units.Count == 1 || _flagHandler.IsFlagSet == false) && _units.Count <= _unitsMaxCount)
            {
                Place place = GetFreePlace();

                Unit spawnedUnit = _unitSpawner.SpawnUnit(place.transform.position);
                place.BecomeFull();

                spawnedUnit.CollectedGold += CollectGold;

                spawnedUnit.SetInitialPosition(place.transform.position);
                _units.Add(spawnedUnit);

                DeleteGold(_unitSpawner.UnitPrice);
                spawnedUnit.Stop();
            }
        }
    }

    private void DeleteGold(int amount)
    {
        if (amount <= _collectedGold.Count)
        {
            _collectedGold.RemoveRange(0, amount);

            _goldUIView.UpdateText(_collectedGold.Count);
        }
    }

    private Place GetFreePlace()
    {
        for (int i = 0; i < _places.Count; i++)
        {
            if (_places[i].IsFull == false)
            {
                return _places[i];
            }
        }

        return null;
    }

    private Unit GetFreeUnit()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].IsStanding)
            {
                return _units[i];
            }
        }

        return null;
    }
}