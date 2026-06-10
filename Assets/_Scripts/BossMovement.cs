using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Target Position")]
    public Vector3 battlePosition = new Vector3(0f, 3f, 0f);

    [Header("Patrol")]
    public float patrolRange = 2f;

    private bool arrived = false;

    private void Update()
    {
        if (!arrived)
        {
            MoveToBattlePosition();
        }
        else
        {
            float x = Mathf.Sin(Time.time) * patrolRange;

            transform.position =
                new Vector3(
                    x,
                    battlePosition.y,
                    transform.position.z
                );
        }
    }

    private void MoveToBattlePosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            battlePosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(
            transform.position,
            battlePosition) < 0.1f)
        {
            arrived = true;

            Debug.Log("Boss Ready!");
        }
    }
}