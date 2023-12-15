using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerGizmos : MonoBehaviour
{
    public StalkerController controller;
    public StalkerStateManager stateMachine;
    private void Start()
    {
    }
    private void OnDrawGizmos()
    {
        Vector3 from = controller.stalkerBody.transform.position;
        Vector3 to = stateMachine.target.transform.position;
        Color color = controller.CanSee(stateMachine.target.transform.position) ? Color.blue : Color.red;
        DrawGizmoLine(from, to, color);

        DrawGizmoSlice(
            stateMachine.target.transform.position,
            stateMachine.spawningState.spawnRadius,
            stateMachine.spawningState.minSpawnAngle,
            stateMachine.spawningState.maxSpawnAngle,
            stateMachine.target.transform.eulerAngles.y,
            Color.blue);

    }
    public void DrawGizmoLine(Vector3 from, Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(from, to);
    }

    private void DrawGizmoSlice(Vector3 center, float circleRadius, float startAngle, float endAngle, float rotation, Color color)
    {
        const int segments = 36;
        float angleIncrement = (endAngle - startAngle) / segments;

        Gizmos.color = color; // Set the Gizmo color

        Vector3 startPoint = center + Quaternion.Euler(0, startAngle + rotation, 0) * Vector3.forward * circleRadius;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = startAngle + rotation + i * angleIncrement;
            Vector3 nextPoint = center + Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * circleRadius;

            // Draw the arc
            Gizmos.DrawLine(startPoint, nextPoint);

            startPoint = nextPoint;
        }

        // Draw spokes
        Vector3 startSpoke = center + Quaternion.Euler(0, startAngle + rotation, 0) * Vector3.forward * circleRadius;
        Vector3 endSpoke = center + Quaternion.Euler(0, endAngle + rotation, 0) * Vector3.forward * circleRadius;

        Gizmos.DrawLine(center, startSpoke);
        Gizmos.DrawLine(center, endSpoke);
    }

}
