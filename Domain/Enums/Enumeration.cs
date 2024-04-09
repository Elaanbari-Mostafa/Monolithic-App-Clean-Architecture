using System.Reflection;

namespace Domain.Enums;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);
        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                 enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                 (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }

    protected Enumeration(int id, string name)
        => (Id, Name) = (id, name);

    public int Id { get; protected init; }
    public string Name { get; protected init; } = string.Empty;

    public static TEnum? FromId(int id)
     => Enumerations
             .TryGetValue(id, out TEnum? _enum)
                  ? _enum
                  : default;

    public static TEnum? FromName(string name)
        => Enumerations
            .Values
                .SingleOrDefault(x => x.Name == name);

    public static IReadOnlyCollection<TEnum> GetValues() => Enumerations.Values.ToList();

    public static HashSet<TEnum> GetValuesFromIds(HashSet<int> ids)
        => GetValues().Where(role => ids.Contains(role.Id)).ToHashSet();

    public bool Equals(Enumeration<TEnum>? other)
        => other is { } && other.Id == Id && other.Name == Name;

    public override bool Equals(object? obj)
        => obj is { } && obj is Enumeration<TEnum> _enum && Equals(_enum);

    public override int GetHashCode() => Id.GetHashCode();
}