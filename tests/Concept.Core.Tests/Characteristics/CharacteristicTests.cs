using Concept.Core.Characteristics;

namespace Concept.Core.Tests.Characteristics;

public sealed class CharacteristicTests
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
    public void ToString_ShouldReturnNameAndValue()
    {
        var intelligence = new Intelligence(100);

        intelligence.ToString().Should().Be($"{intelligence.Name}: {intelligence.Value}");
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenTwoCharacteristicsHaveTheSameName_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);
        var anotherIntelligence = new Intelligence(50);

        intelligence.Equals((object)anotherIntelligence).Should().BeTrue();

    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenTheyAreTheSameInstance_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);

        intelligence.Equals((object)intelligence).Should().BeTrue();

    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenTwoCharacteristicsHaveDifferentNames_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);
        var strength = new Strength(50);

        // ReSharper disable once SuspiciousTypeConversion.Global
        intelligence.Equals((object)strength).Should().BeFalse();

    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOtherIsNull_DetailObjectOverride()
    {
        var intelligence = new Intelligence(100);

        // ReSharper disable once SuspiciousTypeConversion.Global
        intelligence.Equals((object?)null).Should().BeFalse();
        (intelligence == null).Should().BeFalse();
    }

    [Fact]
    public void EqualsOperator_ShouldReturnTrue_WhenTwoCharacteristicsHaveTheSameName()
    {
        var intelligence = new Intelligence(100);
        var anotherIntelligence = new Intelligence(50);

        (intelligence == anotherIntelligence).Should().BeTrue();

    }

    [Fact]
    public void EqualsOperator_ShouldReturnFalse_WhenTwoCharacteristicsHaveDifferentNames()
    {
        var intelligence = new Intelligence(100);
        var strength = new Strength(50);

        // ReSharper disable once SuspiciousTypeConversion.Global
        (intelligence == strength).Should().BeFalse();

    }

    [Fact]
    public void NotEqualsOperator_ShouldReturnTrue_WhenOtherIsNull()
    {
        var intelligence = new Intelligence(100);
        var strength = new Strength(50);

        (intelligence != strength).Should().BeTrue();
    }

    [Fact]
    public void NotEqualsOperator_ShouldReturnFalse_WhenOtherIsNull()
    {
        var intelligence1 = new Intelligence(100);
        var intelligence2 = new Intelligence(100);

        (intelligence1 != intelligence2).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldReturnTheSameValue_WhenTwoCharacteristicsHaveTheSameName()
    {
        var intelligence = new Intelligence(100);
        var anotherIntelligence = new Intelligence(50);

        intelligence.GetHashCode().Should().Be(anotherIntelligence.GetHashCode());
    }
}
