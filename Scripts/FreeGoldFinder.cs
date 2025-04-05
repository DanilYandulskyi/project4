using UnityEngine;
using System.Collections.Generic;

public class FreeGoldFinder : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;

    private List<Gold> _takenGoldList;

    private void Awake()
    {
    }

    private void OnDestroy()
    {
    }

   
}