using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCheckPos : OnionBehaviour
{
    public static EnemyCheckPos Instance;
    public Vector2 enemy_pos;
    public bool have_enemy;


    private void Awake()
    {
        EnemyCheckPos.Instance = this;
    }
    void Start()
    {
        this.LoadComponent();
    }

    
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            this.have_enemy = true;
            this.enemy_pos = other.transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        this.have_enemy = false;
    }
    protected override void LoadComponent()
    {
        this.have_enemy = false;
    }
}
