using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(1f, 1.5f);
    }

    public void OnExecute(Enemy enemy)
    {
        if (timer >= randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
        timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
