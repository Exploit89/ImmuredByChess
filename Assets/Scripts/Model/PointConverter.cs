using UnityEngine;

public class PointConverter
{
    private float _verticalUplift = 0.01f;

    public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        float x = gridPoint.x;
        float z = gridPoint.y;
        return new Vector3(x, _verticalUplift, z);
    }

    public Vector2Int GridPoint(int column, int row)
    {
        Vector2Int gridPoint = new Vector2Int(column, row);
        return gridPoint;
    }

    public Vector2Int GridFromPoint(Vector3 point)
    {
        int column = Mathf.FloorToInt(point.x);
        int row = Mathf.FloorToInt(point.z);
        return new Vector2Int(column, row);
    }
}
