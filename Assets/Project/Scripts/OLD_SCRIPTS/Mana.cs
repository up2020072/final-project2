using UnityEngine;

public class Mana : MonoBehaviour
{
    public GameObject ManaBar;
    public Vector2 Scale;
    public float CurrentMana;
    public float MaxMana;

    void Update()
    {
        CurrentMana = GameData.Data.CurrentMana;
        MaxMana = GameData.Data.MaxMana;
        Scale = new Vector2(CurrentMana / MaxMana * 1.29f, 0.3f);
        if (Scale.x > 0)
        {
            transform.localScale = Scale;
        }
    }

}
