using FluentAssertions;

using NSubstitute;

namespace MyMoney.TestCommon.TestUtilities.NSubstitute;

public static class Must
{
    public static List<T> BeEmptyList<T>() => Arg.Do<List<T>>(x => x.Should().BeEmpty());

    public static List<T> BeListWith<T>(params T[] values) => Arg.Do<List<T>>(x => x.Should().BeEquivalentTo(values.ToList()));
}

