using UnityEngine;

public class Geometry
{
    public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        float x = -3.5f + 1.0f * gridPoint.x;
        float z = -3.5f + 1.0f * gridPoint.y;
        return new Vector3(x, 0, z);
    }

    public Vector2Int GridPoint(int column, int row)
    {
        Vector2Int gridPoint = new Vector2Int(column, row);
        return gridPoint;
    }

    public Vector2Int GridFromPoint(Vector3 point)
    {
        int column = Mathf.FloorToInt(4.0f + point.x);
        int row = Mathf.FloorToInt(4.0f + point.z);
        return new Vector2Int(column, row);
    }
}
