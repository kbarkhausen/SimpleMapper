using System;

namespace SimpleMapper
{
    public interface IMapper
    {
        void Clear();

        void AddMap<TSource, TTArget>(Action<TSource, TTArget> map)
            where TSource : class
            where TTArget : class;

        void Map<TSource, TTarget>(TSource source, TTarget target);

        TTarget Map<TSource, TTarget>(TSource source);
    }
}
