using System.Collections;
using System.Collections.Generic;

namespace MathUtils.ImList
{
    public class DllNode<TD> : IEnumerable<DllNode<TD>>
    {
        readonly TD _mData;
        readonly DllNode<TD> _mPrev;
        readonly DllNode<TD> _mNext;

        public DllNode(TD data, DllNode<TD> prev, IEnumerator<TD> rest)
        { 
            _mData = data;
            _mPrev = prev;
            if (rest.MoveNext()) {
                _mNext = new DllNode<TD>(rest.Current, this, rest);
            }
        }

        public IEnumerator<DllNode<TD>> GetEnumerator()
        {
            var curNode = _mNext;
            while (curNode != null)
            {
                yield return curNode;
                curNode = curNode.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TD Data
        {
            get { return _mData; }
        }

        public DllNode<TD> Next
        {
            get { return _mNext; }
        }

        public DllNode<TD> Prev
        {
            get { return _mPrev; }
        }
    }

    public static class DllNode {    
      public static DllNode<TD> Create<TD>(IEnumerable<TD> enumerable)
      {
        using (var enumerator = enumerable.GetEnumerator()) {
          if (!enumerator.MoveNext()) {
            return null;
          }

          return new DllNode<TD>(enumerator.Current, null, enumerator);
        }
      }
    }
}
