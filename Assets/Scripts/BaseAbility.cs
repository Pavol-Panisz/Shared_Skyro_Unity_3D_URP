using UnityEngine;
using UnityEngine.UI;

public abstract class BaseAbility : MonoBehaviour
{
    [SerializeField] protected KeyCode key;
    [SerializeField, Min(0f)] protected float cooldown;
    [SerializeField] protected Image overlayCooldownImage;

    private float _cooldownTimer;
    
    private void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
            overlayCooldownImage.fillAmount = Mathf.Clamp01(_cooldownTimer / cooldown);
            return;
        }

        if (Input.GetKeyDown(key))
        {
            Activate();
            _cooldownTimer = cooldown;
            overlayCooldownImage.fillAmount = 1f;
        }
    }

    protected abstract void Activate();
}