using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkFactory : MonoBehaviour
{
    [SerializeField] GameObject inkPrefab;
    public void SpawnInk()
    {
        PoolManager.Create(inkPrefab, transform.position);
    }
}
