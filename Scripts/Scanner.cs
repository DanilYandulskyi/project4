using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Scanner : MonoBehaviour
{
    [SerializeField] private List<Gold> _gold = new List<Gold>();
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _scanDelay;
    [SerializeField] private float _scanRadius;
    [SerializeField] private List<Gold> _takenGold = new List<Gold>();

    public IReadOnlyList<Gold> Gold => _gold;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    public void RemoveGold(Gold gold)
    {
        if (_gold.Remove(gold))
        {
            _takenGold.Add(gold);

            List<Gold> goldListToRemove = new List<Gold>();

            foreach (var takenGold in _takenGold)
            {
                if (takenGold.IsActiveAndEnabled == false)
                    goldListToRemove.Add(takenGold);
            }

            foreach (var goldToRemove in goldListToRemove)
            {
                _takenGold.Remove(goldToRemove);
            }
        }
    }

    private IEnumerator Scan()
    {
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            yield return delay;

            Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Gold gold))
                {
                    if (_takenGold.Contains(gold) == false && gold.IsActiveAndEnabled && _gold.Contains(gold) == false)
                    {
                        _gold.Add(gold);
                    }
                }
            }
        }
    }
}
