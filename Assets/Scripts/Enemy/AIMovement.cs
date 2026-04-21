using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class AIMovement : Movement
{
    private Transform _curTarget;
    private Transform _curCustomTarget;

    private NavMeshPath _curNavMeshPath = new NavMeshPath();
    private int _curCornerIndex;

    const int _pathUpdateCooldown = 12; //Path Update Cooldown in frames

    [SerializeField] private float _minDistanceToTheTarget = 2f;
    private bool _isDestinationReached;
    protected event Action<Transform> onDestinationReached;
    protected event Action onDestinationGone;

    private Vector3 _flatVector = new Vector3(1f, 0f, 1f);

    protected override void Awake()
    {
        base.Awake();

        _curCustomTarget = new GameObject("CustomTarget").transform;
        _curCustomTarget.gameObject.hideFlags = HideFlags.HideInInspector;

        if(_curNavMeshPath == null)
            _curNavMeshPath = new NavMeshPath();

        onDestinationReached += OnDestinationReached;
        onDestinationGone += OnDestinationGone;
    }

    protected override void Update()
    {
        base.Update();

        if(Time.frameCount % _pathUpdateCooldown == 0)
        {
            UpdatePath();
        }

        GetNextCorner();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_curNavMeshPath == null || _curNavMeshPath.corners.Length <= _curCornerIndex) return;

        if (Vector3.Distance(transform.position, _curTarget.position) < _minDistanceToTheTarget)
        {
            if(!_isDestinationReached)
            {
                _isDestinationReached = true;
                if(onDestinationReached != null)
                    onDestinationReached(_curTarget);
            }

            return;
        }
        else
        {
            if(_isDestinationReached)
            {
                _isDestinationReached = false;
                if(onDestinationGone != null)
                    onDestinationGone();
            }
        }

        Move(Vector3.Project(_curNavMeshPath.corners[_curCornerIndex] - transform.position, _flatVector).normalized);
    }

    private void UpdatePath()
    {
        if (_curTarget == null) return;
        NavMesh.CalculatePath(GetNearestPositionToTheNavmesh(transform.position), GetNearestPositionToTheNavmesh(_curTarget.position), NavMesh.AllAreas, _curNavMeshPath);
        _curCornerIndex = _curNavMeshPath.corners.Length > 1 ? 1 : 0;
    }

    private void GetNextCorner()
    {
        if (_curNavMeshPath == null || _curNavMeshPath.corners.Length <= _curCornerIndex) { return; }
        if (Vector3.Distance(GetNearestPositionToTheNavmesh(transform.position), _curNavMeshPath.corners[_curCornerIndex]) <= 0.1f)
        {
            _curCornerIndex += _curCornerIndex + 1 < _curNavMeshPath.corners.Length ? 1 : 0;
        }
    }

    private Vector3 GetNearestPositionToTheNavmesh(Vector3 position)
    {
        bool hit = NavMesh.SamplePosition(position, out NavMeshHit navMeshHit, 999f, NavMesh.AllAreas);
        if(hit)
        {
            return navMeshHit.position;
        }
        else
        {
            Debug.LogWarning("There is no NavMesh position to the " + position + " position.");
            return position;
        }
    }

    public void MoveTo(Vector3 position)
    {
        _curCustomTarget.position = position;
        MoveTo(_curCustomTarget);
    }

    public void MoveTo(Transform target)
    {
        _curTarget = target;
        _isDestinationReached = false;
    }

    protected virtual void OnDestinationReached(Transform destination)
    {

    }
    
    protected virtual void OnDestinationGone()
    {

    }
}