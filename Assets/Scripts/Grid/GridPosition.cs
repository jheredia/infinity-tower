using System;

public class GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public bool isActive = true;
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object other) =>
        other is GridPosition position && x == position.x && z == position.z;

    public bool Equals(GridPosition other) => this == other;

    public override int GetHashCode() => HashCode.Combine(x, z);

    public override string ToString() => $"X: {x}; Z: {z}";

    public static bool operator ==(GridPosition a, GridPosition b) => a.x == b.x && a.z == b.z;

    public static bool operator !=(GridPosition a, GridPosition b) => !(a == b);

    public static GridPosition operator +(GridPosition a, GridPosition b) => new(a.x + b.x, a.z + b.z);

    public static GridPosition operator -(GridPosition a, GridPosition b) => new(a.x - b.x, a.z - b.z);
}
