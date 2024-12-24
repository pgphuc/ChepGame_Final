using UnityEngine;

public class Character : MonoBehaviour
{
    
    //thuộc tính cho phép update trên inspector cho lên đầu
    [SerializeField] private Animator Anim; 
    
    //ưu tiên theo mức độ truy cập
    private float HealthPoint;
    private string currentAnim;
    private bool isDead => HealthPoint <= 0;

    //Phương thức để kế thừa cho lên trc

    private void Start()
    {
        OnInit();
    }
    
    public virtual void OnInit()
    {
        HealthPoint = 100;
    }

    public virtual void OnDespawn()
    {
        
    }
    
    
    protected virtual void OnDeath()
    {
        
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
            OnDeath();
        }
    }

    
    
}
