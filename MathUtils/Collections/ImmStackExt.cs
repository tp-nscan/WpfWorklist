using System.Collections.Generic;
using System.Collections.Immutable;

namespace MathUtils.Collections
{
    public static class ImmStackExt
    {
        public static IImmutableStack<T> MakeSubStack<T>(this IImmutableStack<T> stack, out IImmutableStack<T> subStack, int count)
        {
            subStack = ImmutableStack<T>.Empty;
            if(stack.IsEmpty == true) return stack;
            for (int i = 0; i < count; i++)
            {
                subStack = subStack.Push(stack.Peek());
                stack = stack.Pop();
            }
            return stack;
        }

        public static IImmutableStack<T> ToImmutableStack<T>(this IEnumerable<T> list)
        {
            var stackRet = ImmutableStack<T>.Empty;
            foreach (var item in list)
            {
                stackRet = stackRet.Push(item);
            }
            return stackRet;
        }
    }
}
