using UnityEngine;

public class EnemyAI : AIMovement
{
    private bool _isThereAPlayer;

    protected virtual void Start()
    {
        MoveTo(PlayerMovement.Instance.transform);
    }

    protected override void OnDestinationReached(Transform destination)
    {
        base.OnDestinationReached(destination);
        if(destination == PlayerMovement.Instance.transform)
        {
            _isThereAPlayer = true;
        }
    }

    protected override void OnDestinationGone()
    {
        base.OnDestinationGone();
        _isThereAPlayer = false;
    }
}
