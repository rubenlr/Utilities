using System.ComponentModel;

namespace Utilities.Common.Querable.Enum
{
    public enum OperadorQuery
    {
        [Description("{0} = @{1}")]
        Equals,
        [Description("{0} like {1}")]
        Contains,
        [Description("{0} <> @{1}")]
        Different,
        [Description("{0} between @{1} and @{2}")]
        Between,
        [Description("{0} in ({1})")]
        In,
        [Description("{0} >= @{1}")]
        GreaterOrEquals,
        [Description("{0} <= @{1}")]
        LessOrEquals,
        [Description("{0} > @{1}")]
        Greater,
        [Description("{0} < @{1}")]
        Less
    }
}