using SatisTalepYonetimi.Domain.Enums;

namespace Test.Domain;

public class SalesRequestStatusEnumTests
{
    [Fact]
    public void AllStatuses_ShouldHaveUniqueValues()
    {
        var all = SalesRequestStatusEnum.List;
        var uniqueValues = all.Select(x => x.Value).Distinct().Count();
        Assert.Equal(all.Count, uniqueValues);
    }

    [Theory]
    [InlineData(1, "Beklemede")]
    [InlineData(2, "Yönetici Onayında")]
    [InlineData(3, "Yönetici Onayladı")]
    [InlineData(4, "Reddedildi")]
    [InlineData(5, "Satınalma Sürecinde")]
    [InlineData(9, "Tamamlandı")]
    [InlineData(10, "İptal Edildi")]
    public void FromValue_ShouldReturnCorrectEnum(int value, string expectedName)
    {
        var status = SalesRequestStatusEnum.FromValue(value);
        Assert.Equal(expectedName, status.Name);
    }

    [Fact]
    public void FromValue_InvalidValue_ShouldThrow()
    {
        Assert.Throws<Ardalis.SmartEnum.SmartEnumNotFoundException>(() =>
            SalesRequestStatusEnum.FromValue(999));
    }
}
