using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMapper
{
    public class Mapper : IMapper
    {

        private readonly Dictionary<Tuple<Type, Type>, object> _maps = new Dictionary<Tuple<Type, Type>, object>();

        public Mapper()
        {
            var interfaceType = typeof(IMapping);

            var asms = AppDomain.CurrentDomain.GetAssemblies().ToList();

            var mappingDefinitions = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(Activator.CreateInstance);

            foreach (IMapping mappingCollection in mappingDefinitions)
            {
                mappingCollection.Mapper = this;
                mappingCollection.Load();
            }      
        }    

        public void Clear()
        {
            _maps.Clear();
        }

        /// <summary>
        /// <para>Add a new map</para>
        /// </summary>
        /// <typeparam name="TSource">From Type</typeparam>
        /// <typeparam name="TTArget">To Type</typeparam>
        /// <param name="map">Mapping delegate</param>
        public void AddMap<TSource, TTArget>(Action<TSource, TTArget> map)
            where TSource : class
            where TTArget : class
        {
            _maps.Add(Tuple.Create(typeof(TSource), typeof(TTArget)), map);
        }

        /// <summary>
        /// <para>Map object data to an existing object</para>
        /// </summary>
        /// <typeparam name="TSource">From type</typeparam>
        /// <typeparam name="TTarget">To type</typeparam>
        /// <param name="source">From object</param>
        /// <param name="target">To object</param>
        public void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            var key = Tuple.Create(typeof(TSource), typeof(TTarget));
            var map = (Action<TSource, TTarget>)_maps[key];

            if (map == null)
                throw new Exception(string.Format("No map found for {0} => {1}", typeof(TSource).Name, typeof(TTarget).Name));

            map(source, target);
        }

        /// <summary>
        /// <para>Map object data to a new object</para>
        /// </summary>
        /// <typeparam name="TSource">From type</typeparam>
        /// <typeparam name="TTarget">To type</typeparam>
        /// <param name="source">From object</param>
        public TTarget Map<TSource, TTarget>(TSource source)
        {
            var key = Tuple.Create(typeof(TSource), typeof(TTarget));
            var map = (Action<TSource, TTarget>)_maps[key];

            if (map == null)
                throw new Exception(string.Format("No map found for {0} => {1}", typeof(TSource).Name, typeof(TTarget).Name));

            var target = (TTarget)Activator.CreateInstance(typeof(TTarget));
            map(source, target);
            return target;
        }

    }
}
