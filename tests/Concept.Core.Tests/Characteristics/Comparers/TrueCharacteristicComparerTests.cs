using Concept.Core.Characteristics;
using Concept.Core.Characteristics.Comparers;

namespace Concept.Core.Tests.Characteristics.Comparers;

public sealed class TrueCharacteristicComparerTests
{
    class Intelligence(byte value) : Characteristic(value)
    {
        public override string Name => "Intelligence";
        public override string Description => "The ability to learn, understand and make decisions";

    }

    class Strength(byte value) : Characteristic(value)
    {
        public override string Name => "Strength";
        public override string Description => "The ability to exert force on physical objects";

    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenTwoCharacteristicsHaveTheSameName_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);
        var anotherIntelligence = new Intelligence(50);
        CharacteristicComparer comparer = TrueCharacteristicComparer.Instance;

        comparer.Equals(intelligence, anotherIntelligence).Should().BeTrue();

    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenTheyAreTheSameInstance_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);
        CharacteristicComparer comparer = TrueCharacteristicComparer.Instance;

        comparer.Equals(intelligence, intelligence).Should().BeTrue();

    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenTwoCharacteristicsHaveDifferentNames_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);
        var strength = new Strength(50);
        CharacteristicComparer comparer = TrueCharacteristicComparer.Instance;

        comparer.Equals(intelligence, strength).Should().BeFalse();

    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOtherIsNull_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);
        CharacteristicComparer comparer = TrueCharacteristicComparer.Instance;

        comparer.Equals(intelligence, null).Should().BeFalse();

    }

    [Fact]
    public void GetHashCode_ShouldReturnTheSameValue_WhenTwoCharacteristicsHaveTheSameName()
    {
        var intelligence = new Intelligence(100);
        var anotherIntelligence = new Intelligence(50);
        CharacteristicComparer comparer = TrueCharacteristicComparer.Instance;

        comparer.GetHashCode(intelligence).Should().Be(comparer.GetHashCode(anotherIntelligence));
    }
}
