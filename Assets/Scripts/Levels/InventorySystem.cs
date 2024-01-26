using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Canvas _checklist;
    [SerializeField] private TMP_Text _inventoryTextPrefab;
    [SerializeField] private float _itemOffset = 0.25f;

    [SerializeField] private UnityEvent _onInventoryComplete;

    private Dictionary<CollectableScriptableObject, int> _collectableItems = new();
    private List<TMP_Text> _inventoryTexts = new();

    private void Start()
    {
        GameObject[] collectableObjects = GameObject.FindGameObjectsWithTag("PickupableObject");

        foreach (GameObject collectable in collectableObjects)
        {
            PickupableItem pickupableItem = collectable.GetComponent<PickupableItem>();
            CollectableScriptableObject collectableScriptableObject = pickupableItem.GetCollectable();

            if (_collectableItems.ContainsKey(collectableScriptableObject))
            {
                _collectableItems[collectableScriptableObject]++;
            }
            else
            {
                _collectableItems.Add(collectableScriptableObject, 1);
            }
        }

        int i = 0;
        foreach (KeyValuePair<CollectableScriptableObject, int> item in _collectableItems)
        {
            TMP_Text text = Instantiate(_inventoryTextPrefab, _checklist.transform);
            text.transform.SetParent(_checklist.transform, false);
            if (i > 0)
            {
                text.rectTransform.position = new Vector3(_inventoryTexts[i-1].rectTransform.position.x, _inventoryTexts[i-1].rectTransform.position.y - _itemOffset, 0);
            }
            text.text = $"{item.Key.collectableName}: {item.Value}";
            _inventoryTexts.Add(text);

            i++;
        }
    }

    public void CollectItem(CollectableScriptableObject itemName)
    {
        if (_collectableItems.ContainsKey(itemName))
        {
            _collectableItems[itemName]--;
        }

        int id = 0;

        foreach (KeyValuePair<CollectableScriptableObject, int> item in _collectableItems)
        {
            if (item.Key == itemName)
            {
                break;
            }

            id++;
        }

        // edit text of item with id
        if (_collectableItems[itemName] > 0)
        {
            _inventoryTexts[id].text = $"{itemName.collectableName}: {_collectableItems[itemName]}";
        }
        else
        {
            _inventoryTexts[id].text = $"{itemName.collectableName}";

            Color color = _inventoryTexts[id].color;
            color.a = 0.5f;
            _inventoryTexts[id].color = color;
        }

        if (CheckInventory())
        {
            _onInventoryComplete.Invoke();
        }
    }

    private bool CheckInventory()
    {
        foreach (KeyValuePair<CollectableScriptableObject, int> item in _collectableItems)
        {
            if (item.Value > 0)
            {
                return false;
            }
        }

        return true;
    }
}
