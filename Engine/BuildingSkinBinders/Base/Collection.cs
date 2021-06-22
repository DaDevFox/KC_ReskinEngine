using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinEngine.Engine
{
    public class Mod : IEnumerable
    {
        public string ModName { get; internal set; }
        public string CompatabilityIdentifier { get; internal set; }

        public int Priority { get; internal set; } = 0;

        public Dictionary<string, List<SkinBinder>> Binders { get; private set; } = new Dictionary<string, List<SkinBinder>>();

        public static Mod Create(string name, string compatabilityIdentifier)
        {
            return new Mod() {
                ModName = name,
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

        public SkinBinder Get(string identifier)
        {
            if (!Binders.ContainsKey(identifier))
                return null;

            List<SkinBinder> binders = Binders[identifier];

            return binders.Count > 0 ? binders[SRand.Range(0, binders.Count - 1)] : null;
        }

        public int NumBinders(string identifier) => Binders.ContainsKey(identifier) ? Binders[identifier].Count : 0;

        //public void Bind()
        //{
        //    foreach(SkinBinder binder in this)
        //    {


        //    }
        //}

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public ModEnumerator GetEnumerator()
        {
            return new ModEnumerator(Binders);
        }

        public class ModEnumerator : IEnumerator<SkinBinder>
        {
            private List<SkinBinder> binders;

            int index = -1;

            public ModEnumerator(Dictionary<string,List<SkinBinder>> binderIndex)
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
