using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public string ID;
    public string Name;
    public Sprite Sprite;
    public RuntimeAnimatorController EntityAnimator;
    public EntityBase()
    {
        this.ID = "entity";
        this.Name = "Entity";
    }
}
public class Entity : EntityBase
{
    public float Speed;
    public float Scale;
    public bool Attacking;
    public HealthBar Health;
    public LayerMask Targets;
    private Rigidbody2D rb;
    public Vector2 MoveDirection;
    public Items HeldItem;
    public Entity()
    {
        this.Speed = 1;
        this.Scale = 1;
    }
    public void Init()
    {
        gameObject.AddComponent<Animator>();
        GetComponent<Animator>().runtimeAnimatorController = EntityAnimator;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite;
        renderer.sortingLayerName = "Entity";

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.mass = 0.02f;
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.drag = 0;
    }
    private void FixedUpdate()
    {
        Controller();
        Animations();
    }
    public virtual void Controller() { }
    public void Animations()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal", MoveDirection.x);
        animator.SetFloat("Vertical", MoveDirection.y);
        animator.SetFloat("Movement", MoveDirection.magnitude);
        // use ?
        if (MoveDirection.x > 0) { renderer.flipX = true; }
        else renderer.flipX = false;
    }
    public void Move()
    {
        rb.MovePosition(rb.position + (MoveDirection * Speed * Time.deltaTime));
    }
    private void UseItem()
    {
        HeldItem.UseItem(gameObject, Targets);
    }
}
public class Enemy : Entity
{
    //just  a default class
    public float SearchRange;
    public Enemy()
    {
        this.SearchRange = 5;
    }
    public void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
    public override void Controller()
    {
        Vector3 PlayerPos = GameData.Data.player.transform.position;
        float distance = (PlayerPos - transform.position).magnitude;
        MoveDirection = PlayerPos - transform.position;
        MoveDirection.Normalize();
        if (distance < SearchRange && Health.CurrentHealth > 0) { Move(); }
        Attacking = (distance < (HeldItem as Melee).range && HeldItem != null);
        GetComponent<Animator>().SetBool("Attack", Attacking);
    }
}
