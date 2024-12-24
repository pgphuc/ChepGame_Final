using UnityEngine;

public class PatrolState : IState
{
    private float timer = 0;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        randomTime = Random.Range(5f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
        }
        if (timer < randomTime)
        {
            enemy.Moving();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
