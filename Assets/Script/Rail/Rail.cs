using UnityEngine;
using System.Collections;

public enum PlayMode
{
    Linear,
    Catmull
}

[ExecuteInEditMode]
public class Rail : MonoBehaviour
{
    public Node[] nodes;

    private void Start()
    {
        nodes = GetComponentsInChildren<Node>();
    }

    public Vector3 PositionOnRail(int seg,float ratio,PlayMode mode)
    {
        switch (mode)
        {
            case PlayMode.Linear:
                return LinearPosition(seg,ratio);
            case PlayMode.Catmull:
                return CatmullPosition(seg,ratio);
            default:
                return Vector3.zero;
        }
    }

    private Vector3 LinearPosition(int seg, float ratio)
    {
        var n1 = nodes[seg];
        var n2 = nodes[seg+1];

        return Vector3.Lerp(n1.transform.position, n2.transform.position, ratio);
    }
    private Vector3 CatmullPosition(int seg, float t)
    {
        Vector3 p1,p2,p3,p4;
        // ? First node
        if (seg == 0)
        {
            p1 = nodes[seg].transform.position;
            p2 = p1;
            p3 = nodes[seg + 1].transform.position;
            p4 = nodes[seg + 2].transform.position;
        }
        else if (seg == nodes.Length - 2)
        {
            p1 = nodes[seg - 1].transform.position;
            p2 = nodes[seg].transform.position;
            p3 = nodes[seg + 1].transform.position;
            p4 = p3;
        }
        else
        {
            p1 = nodes[seg-1].transform.position;
            p2 = nodes[seg].transform.position;
            p3 = nodes[seg + 1].transform.position;
            p4 = nodes[seg + 2].transform.position;
        }

        float t2 = t * t;
        float t3 = t2 * t;

        float x = 0.5f * (
            (2.0f * p2.x)
            + (-p1.x + p3.x)
            * t + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2
            + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3);

        float y = 0.5f * (
            (2.0f * p2.y)
            + (-p1.y + p3.y)
            * t + (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y) * t2
            + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y) * t3);

        float z = 0.5f * (
            (2.0f * p2.z)
            + (-p1.z + p3.z)
            * t + (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z) * t2
            + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z) * t3);

        return new Vector3(x, y, z);
    }

    public Quaternion LinearOrientation(int seg,float ratio)
    {
        var n1 = nodes[seg];
        var n2 = nodes[seg + 1];

        return Quaternion.Lerp(n1.transform.rotation, n2.transform.rotation, ratio);
    }

    /*
    public void OnDrawGizmos()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Node n1 = nodes[i];
            Node n2 = nodes[i + 1];
            Handles.DrawDottedLine(n1.transform.position, n2.transform.position, 3.0f);
        }
    }
    */
}
