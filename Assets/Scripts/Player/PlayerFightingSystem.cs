using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFightingSystem : FightingSystem
{
    [SerializeField] private Transform _playerAimTarget;
    [SerializeField] private Transform _attacksCooldownsContainer;
    [SerializeField] private GameObject _attackCooldownUIPrefab;

    [SerializeField] private LayerMask _aimLayerMask;

    private void Update()
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight) / 2f), out RaycastHit raycastHit, 999f, _aimLayerMask);

        if(hit)
        {
            _playerAimTarget.position = raycastHit.point;
        }
        else
        {
            _playerAimTarget.position = Camera.main.transform.position + (Camera.main.transform.forward) * 999f;
        }
    }

    protected override AttackScript SpawnAttack(AttackScriptableObject attackScriptableObject)
    {
        AttackScript attackScript = base.SpawnAttack(attackScriptableObject);
        if (attackScript != null)
        {
            attackScript.gameObject.AddComponent<AttackInputsManager>();

            SetTargetToTheRangedWeapon(attackScript, _playerAimTarget);
            attackScript.attackCooldownUI = SpawnAttackCooldownUI(attackScriptableObject);
        }

        return attackScript;
    }

    private Image SpawnAttackCooldownUI(AttackScriptableObject attackScriptableObject)
    {
        Image newImage = Instantiate(_attackCooldownUIPrefab, _attacksCooldownsContainer).GetComponent<Image>();
        newImage.sprite = attackScriptableObject.attackSprite;
        return newImage;
    }
}
