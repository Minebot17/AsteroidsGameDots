using System.Collections.Generic;

namespace Asteroids.Utils.Extension_Methods.Collections
{
    public static class CollectionsExtensions
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    
        public static List<T> ToList<T>(this HashSet<T> set)
        {
            var result = new List<T>();

            foreach (T element in set)
            {
                result.Add(element);
            }
        
            return result;
        }

        public static List<T> ToList<T>(this Queue<T> queue)
        {
            var result = new List<T>();

            for (int i = 0; i < queue.Count; i++)
            {
                result.Add(queue.Dequeue());
            }

            return result;
        }
    
        public static List<T> ToList<T>(this Stack<T> stack)
        {
            var result = new List<T>();

            for (int i = 0; i < stack.Count; i++)
            {
                result.Add(stack.Pop());
            }

            return result;
        }
    
        public static HashSet<T> ToHashSet<T>(this List<T> list)
        {
            var result = new HashSet<T>();

            foreach (T element in list)
            {
                result.Add(element);
            }
        
            return result;
        }

        public static Queue<T> ToQueue<T>(this List<T> list)
        {
            var result = new Queue<T>();

            foreach (T element in list)
            {
                result.Enqueue(element);
            }

            return result;
        }
    
        public static Stack<T> ToStack<T>(this List<T> list)
        {
            var result = new Stack<T>();

            for (int i = list.Count; i >= 0 ; i--)
            {
                result.Push(list[i]);
            }

            return result;
        }
    }
}