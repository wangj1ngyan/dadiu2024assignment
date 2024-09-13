using UnityEngine;

[CreateAssetMenu(menuName = "Game/MobSettings")]
public class MobSettings : ScriptableObject
{
    public float MoveSpeed = 3f;
    public float MaxHealth = 10f;
    public float Acceleration;
}
