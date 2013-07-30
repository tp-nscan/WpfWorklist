using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace MathUtils.Repos
{
    public static class RepoEx
    {
        public static IImmutableStack<T> PopToCollection<T>(this IImmutableStack<T> stack, int size, out ReadOnlyCollection<T> repo)
        {
            var list = new List<T>();
            for (var i = 0; i < size; i++)
            {
                list.Add(stack.Peek());
                stack = stack.Pop();
            }

            repo = new ReadOnlyCollection<T>(list);
            return stack;
        }

        public static IImmutableStack<T> MakeStack<T>(this IReadOnlyCollection<T> repo)
        {
            return repo.Aggregate(ImmutableStack<T>.Empty, (current, item) => current.Push(item));
        }

        public static IEnumerable<IReadOnlyCollection<T>> Split<T>(this IReadOnlyCollection<T> orderedRepo, int pieceCount)
        {
            if ((orderedRepo.Count % pieceCount != 0))
            {
                throw new Exception(String.Format("Cant split repo into {0} equal pieces", pieceCount));
            }
            for (var i = 0; i < pieceCount; i++)
            {
                yield return orderedRepo.GetRange((i * orderedRepo.Count) / pieceCount, orderedRepo.Count / pieceCount);
            }
        }

        public static IReadOnlyCollection<T> GetRange<T>(this IReadOnlyCollection<T> orderedRepo, int startPos, int length)
        {
            var retList = new List<T>();
            for (var i = startPos; i < length + length; i++)
            {
                retList.Add(orderedRepo.ElementAt(i));
            }
            return new ReadOnlyCollection<T>(retList);
        }

    }
}
