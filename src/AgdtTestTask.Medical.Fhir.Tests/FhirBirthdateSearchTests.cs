using AgdtTestTask.Core.Common.Interfaces;
using AgdtTestTask.Medical.Fhir.Extensions;

public class FhirBirthdateSearchTests
{
    [Fact]
    public void Eq_MatchesExactDay()
    {
        var dummies = CreateDummies();

        var result = dummies
            .BuildDateSearchExpression(["1985-06-15"], x => x.Birthdate)
            .ToList();

        Assert.Single(result);
        Assert.Equal(new DateTime(1985, 6, 15), result[0].Birthdate);
    }

    [Fact]
    public void Gt_MatchesDatesAfterInterval()
    {
        var dummies = CreateDummies();

        var result = dummies
            .BuildDateSearchExpression(["gt1985-06-15"], x => x.Birthdate)
            .ToList();

        Assert.Single(result);
        Assert.Equal(new DateTime(1990, 12, 31), result[0].Birthdate);
    }

    [Fact]
    public void Le_MatchesDatesBeforeOrEqual()
    {
        var dummies = CreateDummies();

        var result = dummies
            .BuildDateSearchExpression(["le1985-06-15"], x => x.Birthdate)
            .ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Birthdate == new DateTime(1980, 1, 1));
        Assert.Contains(result, p => p.Birthdate == new DateTime(1985, 6, 15));
    }

    [Fact]
    public void YearOnly_SearchMatchesWholeYear()
    {
        var dummies = CreateDummies();

        var result = dummies
            .BuildDateSearchExpression(["1985"], x => x.Birthdate)
            .ToList();

        Assert.Single(result);
        Assert.Equal(1985, result[0].Birthdate.Year);
    }

    [Fact]
    public void GeLe_DateInRange()
    {
        var dummies = CreateDummies();

        var result = dummies
            .BuildDateSearchExpression(
            [
                "ge1982-01-01",
                "le1989-01-01"
            ], x => x.Birthdate)
            .ToList();

        Assert.Single(result);
        Assert.Equal(1985, result[0].Birthdate.Year);
    }

    [Fact]
    public void InvalidParameter_ThrowsArgumentException()
    {
        var dummies = CreateDummies();

        Assert.Throws<ArgumentException>(() =>
            dummies
                .BuildDateSearchExpression(["not-a-date"], x => x.Birthdate)
                .ToList());
    }

    private static IQueryable<Dummy> CreateDummies()
    {
        return new[]
        {
            new Dummy
            {
                Birthdate = new DateTime(1980, 1, 1),
            },
            new Dummy
            {
                Birthdate = new DateTime(1985, 6, 15),
            },
            new Dummy
            {
                Birthdate = new DateTime(1990, 12, 31),
            }
        }.AsQueryable();
    }

    private class Dummy
    {
        public DateTime Birthdate { get; set; }
    }
}
