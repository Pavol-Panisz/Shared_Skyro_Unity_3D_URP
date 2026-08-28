using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public float cooldown = 1f;
    protected float lastUseTime;

    public virtual bool CanUse()
    {
        return Time.time >= lastUseTime + cooldown;
    }

    public void TryUse()
    {
        if (!CanUse()) return;

        Use();
        lastUseTime = Time.time;
    }

    protected abstract void Use();
}