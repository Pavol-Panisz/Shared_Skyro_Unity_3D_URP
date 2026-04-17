using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public int maxHealthPoints {  get; private set; }
    public int currHealthPoint {  get; set;}
    public float walkSpeed { get; private set; }
    public float attackSpeed {  get; set; }

    public abstract IEnumerator Attack();

    public abstract void DealDamage(int damage);
}

public interface IDamageable
{
    public void DealDamage(int damage);
}