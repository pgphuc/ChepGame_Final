using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] private Text HPText;
    public void OnInit(float HealthPoint)
    {
        HPText.text = HealthPoint.ToString();
        Invoke("OnDespawn", 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    
}
