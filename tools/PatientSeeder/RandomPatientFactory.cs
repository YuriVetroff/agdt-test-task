internal static class RandomPatientFactory
{
    private static readonly Random Rng = new();

    private static readonly string[] MaleGivenNames =
    {
        "John", "Michael", "David", "James", "Daniel",
        "Robert", "Andrew", "Thomas", "William", "Mark"
    };

    private static readonly string[] FemaleGivenNames =
    {
        "Anna", "Sophia", "Emma", "Olivia", "Emily",
        "Maria", "Laura", "Sarah", "Jessica", "Helen"
    };

    private static readonly string[] FamilyNames =
    {
        "Smith", "Johnson", "Brown", "Taylor", "Anderson",
        "Peterson", "Walker", "Harris", "Clark", "Lewis"
    };

    public static object Create()
    {
        var gender = PickGender();
        var birthdate = RandomBirthdate();

        return new
        {
            gender = gender,
            active = Rng.NextDouble() > 0.15,

            name = new
            {
                id = Guid.NewGuid(),
                use = 1,
                given = new[]
                {
                    PickGivenName(gender)
                },
                family = Pick(FamilyNames)
            },

            // ISO-8601
            birthdate = birthdate.ToString("O")
        };
    }

    private static int PickGender()
    {
        return Rng.Next(0, 2) == 0 ? 1 : 2;
    }

    private static string PickGivenName(int gender)
    {
        return gender switch
        {
            1 => Pick(MaleGivenNames),
            2 => Pick(FemaleGivenNames),
            _ => throw new InvalidOperationException("Invalid gender")
        };
    }

    private static DateTime RandomBirthdate()
    {
        var start = new DateTime(1945, 1, 1);
        var end = new DateTime(2015, 12, 31);

        var range = (end - start).Days;

        return start
            .AddDays(Rng.Next(range))
            .AddHours(Rng.Next(0, 24));
    }

    private static string Pick(string[] values)
    {
        return values[Rng.Next(values.Length)];
    }
}