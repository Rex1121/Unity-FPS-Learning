using UnityEngine;

public class SwordMeleeCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 2f;
    public int attackDamage = 25;
    public float attackRate = 1f;
    public LayerMask enemyLayers;

    [Header("References")]
    public Animator animator;
    public Transform attackPoint;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // Default is left mouse click
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        if (animator != null)
            animator.SetTrigger("Attack");

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
