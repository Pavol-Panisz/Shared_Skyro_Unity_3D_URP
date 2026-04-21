using System.Collections.Generic;
using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    private HealthSystem _attackerHealthSystem;

    [SerializeField] private Transform _attacksSpawnPosition;

    [SerializeField] private List<AttackScriptableObject> _startAttacks;
    public List<AttackScript> attacks { get; protected set; } = new List<AttackScript>();

    private void Awake()
    {
        _attackerHealthSystem = GetComponent<HealthSystem>();

        foreach(var attackScriptableObject in _startAttacks)
        {
            AttackScript attackScript = SpawnAttack(attackScriptableObject);
            if(attackScript)
                attacks.Add(attackScript);
        }
    }

    protected virtual AttackScript SpawnAttack(AttackScriptableObject attackScriptableObject)
    {
        GameObject newAttackObject = Instantiate(attackScriptableObject.attackPrefab, _attacksSpawnPosition);
        newAttackObject.transform.localPosition = Vector3.zero;
        newAttackObject.transform.localRotation = Quaternion.identity;

        if(newAttackObject.TryGetComponent(out AttackScript attackScript))
        {
            attackScript.attacker = _attackerHealthSystem;
            attackScript.attackScriptableObject = attackScriptableObject;
            return attackScript;
        }

        return null;
    }
}
