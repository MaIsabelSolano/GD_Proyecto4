using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement1 : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance = 0.05f;

    public bool activated = false;

    private Transform _targetWaypoint;
    private int _currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _targetWaypoint = _waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                _targetWaypoint.position,
                _speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, _targetWaypoint.position) < _checkDistance) {
                _targetWaypoint = GetNextWaypoint();
            }
        }
    }

    private Transform GetNextWaypoint() {
        _currentWaypointIndex ++;
        if (_currentWaypointIndex >= _waypoints.Length) {
            _currentWaypointIndex = 0;
        }

        return _waypoints[_currentWaypointIndex];
    }

    public void ActivatePlatform() {
        activated = true;
    }
}
