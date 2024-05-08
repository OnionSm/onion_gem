using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMWitchIdle : FSMWitchBase
{
    [SerializeField] private float idle_time;
    [SerializeField] private float heal_cool_down;
    public override void EnterState()
    {
        Debug.Log("Enter Idle State");
        this.LoadComponent();
    }
    public override void UpdateState()
    {
        CountIdleTime();
        CanHeal();
        ResetHealCoolDownTime();
        CanChangeState();
    }
    public override void OnCollisionEnter()
    {

    }
    private void CountIdleTime()
    {
        this.idle_time += Time.deltaTime;
    }

    // Check Witch can heal when idle_state > heal_cool_down
    private void CanHeal()
    {
        if (this.idle_time >= heal_cool_down && this.CheckWitchHP())
        {
            FSMWitchManager.Instance.SwitchState(FSMWitchManager.Instance.witch_heal);
        }
    }

    // Check Witch current hp is higher than max hp
    private bool CheckWitchHP()
    {
        if (WitchManager.Instance.witch_hp_current <= WitchManager.Instance.witch_hp_max * 0.4f)
            return true;
        return true;
    }

    protected void LoadComponent()
    {
        this.idle_time = 0f;
        this.heal_cool_down = 5f;
        WitchManager.Instance.cool_down_count = WitchManager.Instance.cool_down_time_skill;
    }
    protected void ResetHealCoolDownTime()
    {
        if(WitchManager.Instance.in_attack_zone_ball_lighting)
        {
            this.idle_time = 0f;
        }
    }
    
    private void ChangeOtherState()
    {
        if (WitchManager.Instance.chasing == true && (WitchManager.Instance.in_attack_zone_ball_lighting == false && WitchManager.Instance.in_attack_zone_water_push == false))
        {
            FSMWitchManager.Instance.SwitchState(FSMWitchManager.Instance.witch_chase);
        }
        if (WitchManager.Instance.in_attack_zone_ball_lighting == true && WitchManager.Instance.in_attack_zone_water_push == false)
        {
            FSMWitchManager.Instance.SwitchState(FSMWitchManager.Instance.witch_attack);
        }
        if (WitchManager.Instance.in_attack_zone_water_push == true)
        {
            FSMWitchManager.Instance.SwitchState(FSMWitchManager.Instance.witch_attack_fast);
        }
    }
    private void CanChangeState()
    {
        if(WitchManager.Instance.cool_down_count > 0)
        {
            return;
        }
        else
        {
            ChangeOtherState();
        }
    }
}
