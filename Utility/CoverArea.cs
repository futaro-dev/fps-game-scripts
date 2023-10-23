using UnityEngine;

public class CoverArea : MonoBehaviour {
    private CoverWaypoint[] covers;

    private void Awake() {
        covers = GetComponentsInChildren<CoverWaypoint>();
    }

    public CoverWaypoint GetRandomCover() {
        return covers[Random.Range(0, covers.Length - 1)];
    }

    // Finds the closest cover waypoint
    public CoverWaypoint GetClosestCover(Vector3 agentLocation) {
        CoverWaypoint closestWaypoint = null;
        float minimumDistance = Mathf.Infinity;
        Vector3 agentPosition = agentLocation;
        foreach (CoverWaypoint w in covers) {
            float distance = Vector3.Distance(w.transform.position, agentPosition);
            if (distance < minimumDistance) {
                closestWaypoint = w;
                minimumDistance = distance;
            }
        }

        return closestWaypoint;
    }
}
