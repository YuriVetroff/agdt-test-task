using System.Runtime.Serialization;

namespace AgdtTestTask.Medical.Fhir.Enums
{
    public enum FhirPrefix
    {
        [EnumMember(Value = "eq")]
        Equal,

        [EnumMember(Value = "ne")]
        NotEqual,

        [EnumMember(Value = "gt")]
        GreaterThan,

        [EnumMember(Value = "lt")]
        LessThan,

        [EnumMember(Value = "ge")]
        GreaterOrEqual,

        [EnumMember(Value = "le")]
        LessOrEqual,

        [EnumMember(Value = "sa")]
        StartAfter,

        [EnumMember(Value = "eb")]
        EndBefore,

        [EnumMember(Value = "ap")]
        Approximate,
    }
}
