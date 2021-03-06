﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MathUtils.AvlTree
{
    public class Map<TKey, TValue> : IEnumerable<TValue>
        where TKey : IComparable<TKey>
    {
        public static readonly Map<TKey, TValue> Empty = new Map<TKey, TValue>(null);

        private readonly Node<TKey, TValue> _node;

        public Node<TKey, TValue> Root
        {
            get { return _node; }
        }

        private Map(Node<TKey, TValue> node)
        {
            _node = node;
        }

        public TValue this[TKey key]
        {
            get
            {
                var node = Get(key, _node);
                return node == null ? default(TValue) : node.Value;
            }
        }

        public Map<TKey, TValue> Add(TKey key, TValue value)
        {
            var result = Add(_node, key, value);
            return new Map<TKey, TValue>(result);
        }

        private static Node<TKey, TValue> Get(TKey key, Node<TKey, TValue> node)
        {
            if (node == null)
                return null;

            var comparison = node.Key.CompareTo(key);
            if (comparison == 0)
                return node;

            if (comparison > 0)
                return Get(key, node.Left);
            return Get(key, node.Right);
        }

        private static Node<TKey, TValue> Add(Node<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null)
                return new Node<TKey, TValue>(key, value, null, null);

            var comparison = node.Key.CompareTo(key);
            if (comparison == 0)
                return new Node<TKey, TValue>(key, value, node.Left, node.Right);

            var l = node.Left;
            var r = node.Right;

            if (comparison > 0)
                l = Add(node.Left, key, value);
            else
                r = Add(node.Right, key, value);

            var n = new Node<TKey, TValue>(node.Key, node.Value, l, r);
            var lh = n.Left == null ? 0 : n.Left.Height;
            var rh = n.Right == null ? 0 : n.Right.Height;
            var b = lh - rh;

            if (Math.Abs(b) == 2) // 2 or -2 means unbalanced
            {
                if (b == 2) // L
                {
                    var llh = n.Left.Left == null ? 0 : n.Left.Left.Height;
                    var lrh = n.Left.Right == null ? 0 : n.Left.Right.Height;
                    var lb = llh - lrh;
                    if (lb == 1) // LL
                    {
                        // rotate right
                        n = RotateRight(n);
                    }
                    else // LR
                    {
                        // rotate left
                        // rotate right
                        l = RotateLeft(l);
                        n = new Node<TKey, TValue>(n.Key, n.Value, l, r);
                        n = RotateRight(n);
                    }
                }
                else // R
                {
                    var rlh = n.Right.Left == null ? 0 : n.Right.Left.Height;
                    var rrh = n.Right.Right == null ? 0 : n.Right.Right.Height;
                    var rb = rlh - rrh;
                    if (rb == 1) // RL
                    {
                        // rotate right
                        // rotate left
                        r = RotateRight(r);
                        n = new Node<TKey, TValue>(n.Key, n.Value, l, r);
                        n = RotateLeft(n);
                    }
                    else // RR
                    {
                        // rotate left
                        n = RotateLeft(n);
                    }
                }
            }

            return n;
        }

        private static Node<TKey, TValue> RotateRight(Node<TKey, TValue> node)
        {
            //       (5)            4     
            //       / \           / \    
            //      4   D         /   \   
            //     / \           3     5  
            //    3   C    -->  / \   / \ 
            //   / \           A   B C   D
            //  A   B                     

            var l = node.Left.Left;
            var r = new Node<TKey, TValue>(node.Key, node.Value, node.Left.Right, node.Right);
            var n = new Node<TKey, TValue>(node.Left.Key, node.Left.Value, l, r);
            return n;
        }

        private static Node<TKey, TValue> RotateLeft(Node<TKey, TValue> node)
        {
            //    (3)               4     
            //    / \              / \    
            //   A   4            /   \   
            //      / \          3     5  
            //     B   5   -->  / \   / \ 
            //        / \      A   B C   D
            //       C   D                

            var l = new Node<TKey, TValue>(node.Key, node.Value, node.Left, node.Right.Left);
            var r = node.Right.Right;
            var n = new Node<TKey, TValue>(node.Right.Key, node.Right.Value, l, r);
            return n;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _node.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



//     public sealed class AVLTree<K, V> : IBinarySearchTree<K, V> where K : IComparable<K>
//    {
//        private sealed class EmptyAVLTree : IBinarySearchTree<K, V>
//        {
//            // IBinaryTree
//            public bool IsEmpty { get { return true; } }
//            public V Value { get { throw new Exception("empty tree"); } }
//            IBinaryTree<V> IBinaryTree<V>.Left { get { throw new Exception("empty tree"); } }
//            IBinaryTree<V> IBinaryTree<V>.Right { get { throw new Exception("empty tree"); } }
//            // IBinarySearchTree
//            public IBinarySearchTree<K, V> Left { get { throw new Exception("empty tree"); } }
//            public IBinarySearchTree<K, V> Right { get { throw new Exception("empty tree"); } }
//            public IBinarySearchTree<K, V> Search(K key) { return this; }
//            public K Key { get { throw new Exception("empty tree"); } }
//            public IBinarySearchTree<K, V> Add(K key, V value) { return new AVLTree<K, V>(key, value, this, this); }
//            public IBinarySearchTree<K, V> Remove(K key) { throw new Exception("Cannot remove item that is not in tree."); }
//            // IMap
//            public bool ClosureContains(K key) { return false; }
//            public V Lookup(K key) { throw new Exception("not found"); }
//            IMap<K, V> IMap<K, V>.Add(K key, V value) { return this.Add(key, value); }
//            IMap<K, V> IMap<K, V>.Remove(K key) { return this.Remove(key); }
//            public IEnumerable<K> Keys { get { yield break; } }
//            public IEnumerable<V> Values { get { yield break; } }
//            public IEnumerable<KeyValuePair<K,V>> Pairs { get { yield break; } }
//        }


//        private static readonly EmptyAVLTree empty = new EmptyAVLTree();
//        public static IBinarySearchTree<K, V> Empty { get { return empty; } }
//        private readonly K key;
//        private readonly V value;
//        private readonly IBinarySearchTree<K, V> left;
//        private readonly IBinarySearchTree<K, V> right;
//        private readonly int height;
//        private AVLTree(K key, V value, IBinarySearchTree<K, V> left, IBinarySearchTree<K, V> right)
//        {
//            this.key = key;
//            this.value = value;
//            this.left = left;
//            this.right = right;
//            this.height = 1 + Math.Max(Height(left), Height(right));
//        }
//        // IBinaryTree
//        public bool IsEmpty { get { return false; } }
//        public V Value { get { return value; } }
//        IBinaryTree<V> IBinaryTree<V>.Left { get { return left; } }
//        IBinaryTree<V> IBinaryTree<V>.Right { get { return right; } }
//        // IBinarySearchTree
//        public IBinarySearchTree<K, V> Left { get { return left; } }
//        public IBinarySearchTree<K, V> Right { get { return right; } }
//        public IBinarySearchTree<K, V> Search(K key)
//        {
//            int compare = key.CompareTo(Key);
//            if (compare == 0)
//                return this;
//            else if (compare > 0)
//                return Right.Search(key);
//            else
//                return Left.Search(key);
//        }
//        public K Key { get { return key; } }
//        public IBinarySearchTree<K, V> Add(K key, V value)
//        {
//            AVLTree<K, V> result;
//            if (key.CompareTo(Key) > 0)
//                result = new AVLTree<K, V>(Key, Value, Left, Right.Add(key, value));
//            else
//                result = new AVLTree<K, V>(Key, Value, Left.Add(key, value), Right);
//            return MakeBalanced(result);
//        }

//        public IBinarySearchTree<K, V> Remove(K key)
//        {
//            IBinarySearchTree<K, V> result;
//            int compare = key.CompareTo(Key);
//            if (compare == 0)
//            {
//                // We have a match. If this is a leaf, just remove it 
//                // by returning Empty.  If we have only one child,
//                // replace the node with the child.
//                if (Right.IsEmpty && Left.IsEmpty)
//                    result = Empty;
//                else if (Right.IsEmpty && !Left.IsEmpty)
//                    result = Left;
//                else if (!Right.IsEmpty && Left.IsEmpty)
//                    result = Right;
//                else
//                {
//                    // We have two children. Remove the next-highest node and replace
//                    // this node with it.
//                    IBinarySearchTree<K, V> successor = Right;
//                    while (!successor.Left.IsEmpty)
//                        successor = successor.Left;
//                    result = new AVLTree<K, V>(successor.Key, successor.Value, Left, Right.Remove(successor.Key));
//                }
//            }
//            else if (compare < 0)
//                result = new AVLTree<K, V>(Key, Value, Left.Remove(key), Right);
//            else
//                result = new AVLTree<K, V>(Key, Value, Left, Right.Remove(key));
//            return MakeBalanced(result);
//        }

//        // IMap
//        public bool ClosureContains(K key) { return !Search(key).IsEmpty; }
//        IMap<K, V> IMap<K, V>.Add(K key, V value) { return this.Add(key, value); }
//        IMap<K, V> IMap<K, V>.Remove(K key) { return this.Remove(key); }
//        public V Lookup(K key)
//        {
//            IBinarySearchTree<K, V> tree = Search(key);
//            if (tree.IsEmpty)
//                throw new Exception("not found");
//            return tree.Value;
//        }
//        public IEnumerable<K> Keys { get { return from t in Enumerate() select t.Key; } }
//        public IEnumerable<V> Values { get { return from t in Enumerate() select t.Value; } 
//        }
//        public IEnumerable<KeyValuePair<K,V>> Pairs 
//        {
//            get 
//            {
//                return from t in Enumerate() select new KeyValuePair<K, V>(t.Key, t.Value);
//            } 
//        }
//        private IEnumerable<IBinarySearchTree<K, V>> Enumerate()
//        {
//            var stack = Stack<IBinarySearchTree<K,V>>.Empty;
//            for (IBinarySearchTree<K,V> current = this; !current.IsEmpty || !stack.IsEmpty; current = current.Right)
//            {
//                while (!current.IsEmpty)
//                {
//                    stack = stack.Push(current);
//                    current = current.Left;
//                }
//                current = stack.Peek();
//                stack = stack.Pop();
//                yield return current;
//            } 
//        }

//        // Static helpers for tree balancing
//        private static int Height(IBinarySearchTree<K, V> tree)
//        {
//            if (tree.IsEmpty)
//                return 0;
//            return ((AVLTree<K,V>)tree).height;
//        }

//        private static IBinarySearchTree<K, V> RotateLeft(IBinarySearchTree<K, V> tree)
//        {
//            if (tree.Right.IsEmpty)
//                return tree;
//            return new AVLTree<K, V>( tree.Right.Key, tree.Right.Value,
//                new AVLTree<K, V>(tree.Key, tree.Value, tree.Left, tree.Right.Left),
//                tree.Right.Right);
//        }

//        private static IBinarySearchTree<K, V> RotateRight(IBinarySearchTree<K, V> tree)
//        {
//            if (tree.Left.IsEmpty)
//                return tree;
//            return new AVLTree<K, V>( tree.Left.Key, tree.Left.Value, tree.Left.Left,
//                new AVLTree<K, V>(tree.Key, tree.Value, tree.Left.Right, tree.Right));
//        }

//        private static IBinarySearchTree<K, V> DoubleLeft(IBinarySearchTree<K, V> tree)
//        {
//            if (tree.Right.IsEmpty)
//                return tree;
//            AVLTree<K, V> rotatedRightChild = new AVLTree<K, V>(tree.Key, tree.Value, tree.Left, RotateRight(tree.Right));
//            return RotateLeft(rotatedRightChild);
//        }

//        private static IBinarySearchTree<K, V> DoubleRight(IBinarySearchTree<K, V> tree)
//        {
//            if (tree.Left.IsEmpty)
//                return tree;
//            AVLTree<K, V> rotatedLeftChild = new AVLTree<K, V>(tree.Key, tree.Value, RotateLeft(tree.Left), tree.Right);
//            return RotateRight(rotatedLeftChild);
//        }

//        private static int Balance(IBinarySearchTree<K, V> tree)
//        {
//            if (tree.IsEmpty)
//                return 0;
//            return Height(tree.Right) - Height(tree.Left);
//        }

//        private static bool IsRightHeavy(IBinarySearchTree<K, V> tree) { return Balance(tree) >= 2; }

//        private static bool IsLeftHeavy(IBinarySearchTree<K, V> tree) { return Balance(tree) <= -2; }

//        private static IBinarySearchTree<K, V> MakeBalanced(IBinarySearchTree<K, V> tree)
//        {
//            IBinarySearchTree<K, V> result;
//            if (IsRightHeavy(tree))
//            {
//                if (IsLeftHeavy(tree.Right))
//                    result = DoubleLeft(tree);
//                else
//                    result = RotateLeft(tree);
//            }
//            else if (IsLeftHeavy(tree))
//            {
//                if (IsRightHeavy(tree.Left))
//                    result = DoubleRight(tree);
//                else
//                    result = RotateRight(tree);
//            }
//            else
//                result = tree;
//            return result;
//        }
//    }
}
