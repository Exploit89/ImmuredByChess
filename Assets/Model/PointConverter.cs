using UnityEngine;

public class PointConverter
{
    private float _offsetFromGrid = 3.5f;
    private float _offsetFromPoint = 4f;
    private float _floatMultiplier = 1f;

    public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        float x = -_offsetFromGrid + (_floatMultiplier * gridPoint.x);
        float z = -_offsetFromGrid + (_floatMultiplier * gridPoint.y);
        return new Vector3(x, 0, z);
    }

    public Vector2Int GridPoint(int column, int row)
    {
        Vector2Int gridPoint = new Vector2Int(column, row);
        return gridPoint;
    }

    public Vector2Int GridFromPoint(Vector3 point)
    {
        int column = Mathf.FloorToInt(_offsetFromPoint + point.x);
        int row = Mathf.FloorToInt(_offsetFromPoint + point.z);
        return new Vector2Int(column, row);
    }
}
