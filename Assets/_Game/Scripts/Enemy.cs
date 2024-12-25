using System.Numerics;
using UnityEngine;
using UnityEngine.XR;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject AttackArea;
    
    private float attackRange = 2;
    public float speed = 3;
    private IState currentState;
    private bool isRight = true;
    private Character target;
    public Character Target => target;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        DeactiveAttack();
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(HealthBar.gameObject);
        Destroy(gameObject);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void SetTarget(Character character)
    {
        this.target = character;

        if (InTargetRange())
        {
            ChangeState((new AttackState()));
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());  
        }
        else
        {
            ChangeState(new IdleState());
        }
    }
    //Hàm để di chuyển enemy
    public void Moving()
    {
        ChangeAnim("Run");
        rb.linearVelocity = transform.right * speed;
    }

    public void StopMoving()
    {
        ChangeAnim("Idle");
        rb.linearVelocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("Attack");
        ActiveAttack();
        Invoke("DeactiveAttack", 0.5f);
    }
    private void ActiveAttack()
    {
        
        AttackArea.SetActive(true);
    }

    private void DeactiveAttack()
    {
        
        AttackArea.SetActive(false);
    }

    public bool InTargetRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWall"))
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        // transform.rotation = isRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    
}
