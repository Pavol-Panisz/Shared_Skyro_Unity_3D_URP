using UnityEngine;

public class Player : Character
{
    public static GameObject Instance;

    [SerializeField] protected UnityEngine.UI.Image healthImage;
    [SerializeField] protected  
        
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    #region InstanceSingleton
    protected override void Awake()
    {
        if (Instance == null)
            Instance = gameObject;
        else
            Destroy(gameObject);
        
        base.Awake();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    private void Update()
    {
        UpdateHealthBar();
        RotateTowardsCursor();
        Attack();
    }

    private void RotateTowardsCursor()
    {
        var mousePos = Input.mousePosition;
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, mouseWorldPos));
    }

    private void UpdateHealthBar()
    {
        healthImage.fillAmount = 1f - Mathf.Clamp01((float)CurrentHealth / maxHealth);
    }

    protected override void Attack()
    {
        Physics2D.OverlapBox();
    }

    protected override void Die() { }
}