using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingFramework.Reskin.Engine
{
    public class Collection : IEnumerable
    {
        public string CollectionName { get; internal set; }
        public string CompatabilityIdentifier { get; internal set; }

        public Dictionary<string, List<SkinBinder>> Binders { get; private set; } = new Dictionary<string, List<SkinBinder>>();

        public static Collection Create(string name, string compatabilityIdentifier)
        {
            return new Collection() {
                CollectionName = name,
                CompatabilityIdentifier = compatabilityIdentifier
            };
        }

        public void Add(SkinBinder binder)
        {
            if (Binders.ContainsKey(binder.TypeIdentifier))
                Binders[binder.TypeIdentifier].Add(binder);
            else
                Binders.Add(binder.TypeIdentifier, new List<SkinBinder>() { binder });
        }

        public void Bind()
        {
            foreach(SkinBinder binder in this)
            {


            }
        }

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public CollectionEnumerator GetEnumerator()
        {
            return new CollectionEnumerator(Binders);
        }

        public class CollectionEnumerator : IEnumerator<SkinBinder>
        {
            private List<SkinBinder> binders;

            int index = -1;

            public CollectionEnumerator(Dictionary<string,List<SkinBinder>> binderIndex)
            {
                binders = new List<SkinBinder>();
                foreach(string key in binderIndex.Keys)
                {
                    foreach(SkinBinder binder in binderIndex[key])
                    {
                        binders.Add(binder);
                    }
                }
            }

            public SkinBinder Current
            {
                get
                {
                    try
                    {
                        return binders[index];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                binders.Clear();
                index = -1;
            }

            public bool MoveNext()
            {
                index++;
                return (index < binders.Count);
            }

            public void Reset()
            {
                index = -1;
            }
        }

        #endregion
    }
}
