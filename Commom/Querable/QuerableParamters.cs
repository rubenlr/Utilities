using Utilities.Common.Querable.Enum;

namespace Utilities.Common.Querable
{
    public abstract class QuerableParamters<T>
    {
        public T Name { set; get; }
    }

    public class QuerableEquals<T> : QuerableParamters<T>
    {
        public object Data { set; get; }
        public OperadorQuery Operador = OperadorQuery.Equals;
    }

    public class QuerableContains<T> : QuerableParamters<T>
    {
        public object ContainData { set; get; }
        public OperadorQuery Operador = OperadorQuery.Contains;
    }

    public class QuerableDifferent<T> : QuerableParamters<T>
    {
        public object DifferentData { set; get; }
        public OperadorQuery Operador = OperadorQuery.Different;
    }

    public class QuerableBetween<T> : QuerableParamters<T>
    {
        public object Begin { set; get; }
        public object End { set; get; }
        public OperadorQuery Operador = OperadorQuery.Between;
    }

    public class QuerableGreaterOrEquals<T> : QuerableParamters<T>
    {
        public object Data { set; get; }
        public OperadorQuery Operador = OperadorQuery.GreaterOrEquals;
    }

    public class QuerableLessOrEquals<T> : QuerableParamters<T>
    {
        public object Data { set; get; }
        public OperadorQuery Operador = OperadorQuery.LessOrEquals;
    }

    public class QuerableGreater<T> : QuerableParamters<T>
    {
        public object Data { set; get; }
        public OperadorQuery Operador = OperadorQuery.Greater;
    }

    public class QuerableLess<T> : QuerableParamters<T>
    {
        public object Data { set; get; }
        public OperadorQuery Operador = OperadorQuery.Less;
    }
}
