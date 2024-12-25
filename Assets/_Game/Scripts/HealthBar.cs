using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private Vector3 offset;
    
    private float currentHP;
    private float maxHP;

    private Transform target;

    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, currentHP / maxHP, Time.deltaTime * 5f);
        transform.position = target.position + offset;
    }

    public void OnInit(float maxHP, Transform target)
    {
        this.target = target;
        this.maxHP = maxHP;
        currentHP = maxHP;
        imageFill.fillAmount = currentHP / maxHP;
    }

    public void SetNewHP(float currentHP)
    {
        this.currentHP = currentHP;
        // imageFill.fillAmount = currentHP / maxHP;
    }
}
