using UnityEngine;

public class DemoEnemy : MonoBehaviour
{
    public DifficultySetting difficulty;

    float damage;

    void Start()
    {
        damage = difficulty.basicEnemyDamage;
    }
}
