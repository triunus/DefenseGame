using UnityEngine;

public class ParentObjectData : MonoBehaviour
{
    [SerializeField]
    private Transform wallParent;
    [SerializeField]
    private Transform obstacleParent;
    [SerializeField]
    private Transform enemyParent;

    public Transform WallParent { get; }
    public Transform ObstacleParent { get; }
    public Transform EnemyParent { get; }
}