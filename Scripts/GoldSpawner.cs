using UnityEngine;
using System.Collections;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] private Gold _gold;
    [SerializeField] private float _interval;

    private void Awake()
    {
        StartCoroutine(SpawnResource());
    }

    private IEnumerator SpawnResource()
    {
        WaitForSeconds waiter = new WaitForSeconds(_interval);

        while (enabled)
        {
            Spawn();
            yield return waiter;
        }
    }

    private void Spawn()
    {
        int minValue = -5;
        int maxValue = 5;

        Vector3 randomPosition = new Vector3(Random.Range(minValue, maxValue),
            0, Random.Range(minValue, maxValue));

        Instantiate(_gold, randomPosition, Quaternion.identity);
    }
}