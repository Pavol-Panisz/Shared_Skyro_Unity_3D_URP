using TMPro;
using UnityEngine;

public class Player : Character
{
    [SerializeField]private TextMeshProUGUI healthText;

    Rigidbody rb;
	Camera viewCamera;
	Vector3 velocity;

	public override void CustomStart () {
		rb = GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
        canAttack = true;
	}

	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt(mousePos + Vector3.up * transform.position.y);
		velocity = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

        if (Input.GetMouseButtonDown(0) && canAttack) Attack();

        healthText.text = "Health: " + health;
	}

	void FixedUpdate() {
		rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
	}

    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward, Vector3.one / 2);
        IDamageable damageable;
        foreach (Collider collider in colliders)
        {
            if (collider.transform == transform) continue;
            collider.TryGetComponent(out damageable);

            if (damageable != null)
            {
                damageable.DealDamage(damage);  
            }
        }

        base.Attack();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.forward, Vector3.one);
    }
}
