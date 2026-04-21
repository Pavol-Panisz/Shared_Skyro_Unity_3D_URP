using UnityEngine;

[RequireComponent(typeof(FightingSystem))]
public class EnemyAI : AIMovement
{
    private FightingSystem _fightingSystem;

    private bool _isThereAPlayer;

    protected override void Awake()
    {
        _fightingSystem = GetComponent<FightingSystem>();
        base.Awake();
    }

    protected virtual void Start()
    {
        MoveTo(PlayerMovement.Instance.transform);
    }

    protected override void Update()
    {
        base.Update();

        transform.forward = new Vector3(PlayerMovement.Instance.transform.position.x, 0f, PlayerMovement.Instance.transform.position.z) - new Vector3(transform.position.x, 0f, transform.position.z);
    }

    protected override void OnDestinationReached(Transform destination)
    {
        base.OnDestinationReached(destination);
        if(destination == PlayerMovement.Instance.transform)
        {
            _isThereAPlayer = true;
            _fightingSystem.attacks[0].OnAttack(true);
        }
    }

    protected override void RotateObjectToTheMoveDirection(Vector3 moveDirection) { }

    protected override void OnDestinationGone()
    {
        base.OnDestinationGone();
        _isThereAPlayer = false;
        _fightingSystem.attacks[0].OnAttack(false);
    }
}
