using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public int maxHealthPoints = 10;
    public int currHealthPoint = 10;
    public float walkSpeed = 5f;
    public float attackSpeed = 1f;

    public abstract IEnumerator Attack();

    public abstract void DealDamage(int damage);
}

public interface IDamageable
{
    public void DealDamage(int damage);
}