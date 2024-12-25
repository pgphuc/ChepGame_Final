using UnityEngine;

public class Character : MonoBehaviour
{
    
    //thuộc tính cho phép update trên inspector cho lên đầu
    [SerializeField] private Animator Anim; 
    [SerializeField] protected HealthBar HealthBar;
    [SerializeField] private CombatText CombatTextPrefab;
    
    //ưu tiên theo mức độ truy cập
    private float HealthPoint;
    private string currentAnim;
    public bool isDead => HealthPoint <= 0;

    //Phương thức để kế thừa cho lên trc

    private void Start()
    {
        OnInit();
    }
    
    public virtual void OnInit()
    {
        HealthPoint = 150;
        HealthBar.OnInit(HealthPoint, transform);
    }

    public virtual void OnDespawn()
    {
        
    }
    
    
    protected virtual void OnDeath()
    {
        ChangeAnim("Death");
        Invoke("OnDespawn", 0.5f);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            Anim.ResetTrigger(currentAnim);//Xóa anim hiện tại và chèn vào anim mới
            currentAnim = animName;//gán giá trị anim mới cho bến
            Anim.SetTrigger(animName);//set trigger thực hiện anim mới
        }
    }
    public void OnHit(float Damage)
    {
        if (!isDead)
        {
            HealthPoint -= Damage;
        }
        else
        {
            HealthPoint = 0;
            OnDeath();
        }
        HealthBar.SetNewHP(HealthPoint);
        Instantiate(CombatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(Damage);
    }

    
    
}
