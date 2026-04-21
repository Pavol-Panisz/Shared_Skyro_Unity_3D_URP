using System.Collections;
using UnityEngine;

public class PlayerP : Character, IDamageable
{
    public static PlayerP instance;

    public Transform swordPivot;
    public GameObject sword;

    private Rigidbody2D rb;
    private Camera viewCamera;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;
    }

    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * walkSpeed, Input.GetAxis("Vertical") * walkSpeed) * Time.deltaTime;

        Vector3 mousePos = viewCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dirToMouse = mousePos - transform.position;
        dirToMouse.z = 0;
        swordPivot.up = dirToMouse;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

    }

    public override void DealDamage(int damage)
    {
        currHealthPoint -= damage;
        if (currHealthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override IEnumerator Attack()
    {
        sword.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(false);
        yield return new WaitForSeconds(0.4f);
    }
}

public abstract class BaseAbility : MonoBehaviour
{
    public float cooldown;
    public abstract IEnumerator AbilityActivate();
}