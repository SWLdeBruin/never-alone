using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;

    [Space(10)]

    [SerializeField] private float _speed = 1f;

    private bool _isPatrolling = true;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (!_isPatrolling) return;

        if (Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Count)
            {
                _currentWaypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
    }

    public void CancelPatrol()
    {
        _isPatrolling = false;
    }
}
