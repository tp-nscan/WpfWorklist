using System;
using System.Collections;
using System.Collections.Generic;

namespace MathUtils.AvlTree
{
    public class Node<TK, TV> : IEnumerable<TV>
    {
        public TK Key { get; private set; }
        public TV Value { get; private set; }

        public int Height { get; private set; }

        public Node<TK, TV> Left { get; private set; }
        public Node<TK, TV> Right { get; private set; }

        public Node(TK key, TV value, Node<TK, TV> left, Node<TK, TV> right)
        {
            Key = key;
            Value = value;
            Left = left;
            Right = right;

            var l = left == null ? 0 : left.Height;
            var r = right == null ? 0 : right.Height;
            Height = Math.Max(l, r) + 1;
        }

        public IEnumerator<TV> GetEnumerator()
        {
            if (Left != null)
            {
                foreach (var n in Left)
                {
                    yield return n;
                }
            }

            yield return Value;

            if (Right != null)
            {
                foreach (var n in Right)
                {
                    yield return n;
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
