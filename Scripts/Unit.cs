using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]
public class Unit : MonoBehaviour
{
    private const float DistanceToStop = 0.2f;

    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private int _price;
    [SerializeField] private Gold _gold;
    [SerializeField] private Flag _flag;
    [SerializeField] private Vector3 _target;

    private Vector3 _initialPosition;
    private UnitMover _mover;

    public event Action<Gold> CollectedGold;

    public Vector3 InitialPosition => _initialPosition;
    public bool IsStanding { get; private set; } = true;
    public int Price => _price;

    private void OnEnable()
    {
        _mover = GetComponent<UnitMover>();
    }

    public void Update()
    {
        if (_target != Vector3.zero)
        {
            Vector3 direction = _target - transform.position;

            _mover.Move(direction);

            if (Vector3.SqrMagnitude(_initialPosition - transform.position) <= DistanceToStop && _target == _initialPosition)
            {
                CollectGold();
                CollectedGold?.Invoke(_gold);
            }
        }
    }

    public void Initialize(BaseSpawner baseSpawner)
    {
        _baseSpawner = baseSpawner;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Gold gold))
        {
            if (gold == _gold)
            {
                TakeGold();
            }
        }
        else if (collision.gameObject.TryGetComponent(out Flag flag))
        {
            if (flag == _flag)
            {
                TakeFlag();
            }
        }
    }

    public void SetFlag(Flag flag)
    {
        _flag = flag;
        _target = _flag.transform.position;

        if (_flag.isActiveAndEnabled)
        {
            IsStanding = false;
        }
    }

    public void SetInitialPosition(Vector3 position)
    {
        _initialPosition = position;
    }

    public void Stop()
    {
        StartCoroutine(StopForSeconds(3));
    }

    public void SetGold(Gold gold)
    {
        if (IsStanding)
        {
            if (gold.IsActiveAndEnabled)
            {
                _target = gold.transform.position;
                _gold = gold;
                IsStanding = false;
            }
        }
    }

    private void TakeFlag()
    {
        if (_flag.isActiveAndEnabled)
        {
            _baseSpawner.SpawnBase(_flag.transform.position).Assign(this);
            _flag.Disable();
            _flag = null;
            IsStanding = true;
        }
    }

    private void TakeGold()
    {
        _gold.StartFollow(transform);
        _mover.StopMoving();
        _target = _initialPosition;
    }

    private void CollectGold()
    {
        if (_gold != null)
        {
            CollectedGold?.Invoke(_gold);
            IsStanding = true;
            _gold.StopFollow();
            _gold.Disable();
            _gold = null;
        }
    }

    private IEnumerator StopForSeconds(float time)
    {
        _mover.StopMoving();
        IsStanding = false;
        _gold = null;

        yield return new WaitForSeconds(time);

        IsStanding = true;
    }
}