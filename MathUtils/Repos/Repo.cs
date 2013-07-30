//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace MathUtils.Repos
//{
//    public class Repo<T> : IIndexedRepo<T>
//    {
//        public Repo(IEnumerable<T> items) : this(items.ToList(), 0, new int?())
//        {
//        }

//        protected readonly int _lowerBound;
//        private readonly int _upperBound;

//        protected readonly List<T> _listOfItems = new List<T>();
//        public IEnumerable<T> Items
//        {
//            get
//            {
//                for (var i = _lowerBound; i < _upperBound; i++)
//                {
//                    yield return _listOfItems[i];
//                }
//            }
//        }

//        public int Size
//        {
//            get { return _upperBound - _lowerBound; }
//        }

//        public T this[int index]
//        {
//            get { return _listOfItems[index + _lowerBound]; }
//        }

//        public IIndexedRepo<T> SubRepo(int start, int count)
//        {
//            return new Repo<T>(_listOfItems, _lowerBound + start, count);
//        }

//        protected Repo(List<T> listOfItems, int lowerBound, int? count)
//        {
//            _lowerBound = lowerBound;
//            _listOfItems = listOfItems;

//            if (!count.HasValue)
//            {
//                _upperBound = _listOfItems.Count;
//                return;
//            }

//            if (_listOfItems.Count < count.Value + lowerBound)
//            {
//                throw new ArgumentException("last item is past the end of the list");
//            }

//            _upperBound = count.Value + lowerBound;
//        }
//    }
//}
