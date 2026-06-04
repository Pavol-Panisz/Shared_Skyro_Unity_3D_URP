using UnityEngine;

public class Player : Character
{
    [SerializeField] protected UnityEngine.UI.Image healthImage;
    [SerializeField] protected UnityEngine.UI.Image swordImage;
    [SerializeField] protected GameObject gameOverPanel;
    
    [SerializeField] protected DamageDealer damageDealer;    
    [SerializeField, Min(0f)] protected float attackCooldown;
    
    private Camera _camera;

    private float _attackCooldownTimer;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Move();
        UpdateHealthBar();
        UpdateSwordBar();
        RotateTowardsCursor();
        Attack();
    }

    private void UpdateSwordBar()
    {
        swordImage.fillAmount = Mathf.Clamp01(_attackCooldownTimer / attackCooldown);
    }

    private void Move()
    {
        var moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        transform.position = (Vector2)transform.position + moveDirection * (walkSpeed * Time.deltaTime);
    }

    private void RotateTowardsCursor()
    {
        var mousePos = Input.mousePosition;
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, mouseWorldPos - (Vector2)transform.position));
    }

    private void UpdateHealthBar()
    {
        healthImage.fillAmount = 1f - Mathf.Clamp01((float)CurrentHealth / maxHealth);
    }

    protected override void Attack()
    {
        if (_attackCooldownTimer > 0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;
        
        damageDealer.ExternalAttack();
        
        _attackCooldownTimer = attackCooldown;
    }

    protected override void Die()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}