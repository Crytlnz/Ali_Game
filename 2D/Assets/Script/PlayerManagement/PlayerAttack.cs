using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private Vector2 attackRange = new Vector2(1f, 1f);
    [SerializeField] private Vector2 attackOffset = new Vector2(1f, 0);
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip attackSound;

    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;

    public bool isAttacking { get; private set; } // Public supaya bisa diakses dari PlayerMovement

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(attackSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        isAttacking = true;

        // Reset isAttacking setelah durasi animasi selesai (ubah 0.4 sesuai animasi kamu)
        Invoke(nameof(StopAttack), 0.4f);

        Vector2 pos = (Vector2)transform.position + attackOffset * (int)transform.localScale.x;
        Collider2D[] hits = Physics2D.OverlapBoxAll(pos, attackRange, 0, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.Takedamage(damage);
            }
        }
    }

    private void StopAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 pos = (Vector2)transform.position + attackOffset * (int)Mathf.Sign(transform.localScale.x);
        Gizmos.DrawWireCube(pos, attackRange);
    }
}
