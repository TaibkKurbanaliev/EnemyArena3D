using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player/Config")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public MovementConfig WalkingConfig { get; private set; }
    [field: SerializeField] public MovementConfig RunningConfig { get; private set; }
}
