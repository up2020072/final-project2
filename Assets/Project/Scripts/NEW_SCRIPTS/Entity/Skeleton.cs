using UnityEngine;

public class Skeleton : Enemy
{
    public float AvoidRange;
    public bool RunAway;
    public bool MoveCloser;
    public void Start()
    {
        this.Init();
        Health = gameObject.AddComponent<HealthBar>();
        Health.InitHealthBar(30, 30, HealthBar.HealthType.Armour, 10, gameObject);
    }
    public override void Controller()
    {
        Vector3 PlayerPos = GameData.Data.player.transform.position;
        float distance = (PlayerPos - transform.position).magnitude;
        MoveDirection = (PlayerPos - transform.position);
        MoveDirection.Normalize();

        RunAway = distance < AvoidRange;
        if (RunAway) MoveDirection = -MoveDirection;
        MoveCloser = distance > AvoidRange + 1 && distance < SearchRange;
        if (MoveCloser || RunAway && Health.CurrentHealth > 0) { Move(); }
        Attacking = (!RunAway && !MoveCloser && HeldItem != null);
        GetComponent<Animator>().SetBool("Attack", Attacking);
    }
}
