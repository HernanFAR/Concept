namespace Concept.Core.Nodes;

public readonly record struct NodeId
{
    public NodeId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Node id cannot be empty.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
