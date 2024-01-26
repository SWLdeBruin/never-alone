using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousDetection : MonoBehaviour
{
    [SerializeField] private int _gameOverSceneId = 0;
    [SerializeField] private float _dangerousTime = 1f;

    [SerializeField] private EnemyPatrolling _enemyPatrolling;

    private GameObject _enemy;

    private Transform _playerTransform;

    private void Start()
    {
        if (_enemyPatrolling != null)
        {
            _enemy = _enemyPatrolling.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = other.transform;
        }
    }

    private void Update()
    {
        if (_playerTransform != null)
        {
            if (_enemyPatrolling == null)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                if (Vector3.Distance(_enemy.transform.position, _playerTransform.position) < 0.1f)
                {
                    StartCoroutine(GameOver());
                }
                else
                {
                    _enemyPatrolling.CancelPatrol();
                    _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _playerTransform.position, 5 * Time.deltaTime);
                }
            }
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(_dangerousTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameOverSceneId);
    }
}
