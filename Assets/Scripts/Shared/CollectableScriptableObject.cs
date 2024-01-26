using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectable", menuName = "ScriptableObject/Collectable")]
public class CollectableScriptableObject : ScriptableObject
{
    public string collectableName;
    public Sprite collectableIcon;

    public void OnCollect(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
