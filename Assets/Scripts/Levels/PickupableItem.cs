using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField] private CollectableScriptableObject _collectable;

    public CollectableScriptableObject GetCollectable()
    {
        return _collectable;
    }
}
