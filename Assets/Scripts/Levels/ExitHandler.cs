using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] private int _nextLevelId = 6;

    private bool _canExit = false;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void CanExitWorld()
    {
        _canExit = true;
        _spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canExit)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(_nextLevelId);
            }
        }
    }
}
