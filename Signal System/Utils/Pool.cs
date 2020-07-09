using System;
using System.Collections.Concurrent;

namespace Signals
{
    public class Pool<T>
    {
        //To use this class:
        //Pool<MyClass> pool = new Pool<MyClass> (() => new MyClass());  

        private ConcurrentBag<T> objects;
        private Func<T> objectGenerator;

        public Pool(Func<T> objectGenerator) {
            objects = new ConcurrentBag<T>();
            this.objectGenerator = objectGenerator;
        }

        public T Request() {
            T item;
            if (objects.TryTake(out item)) {
                return item;
            }

            return objectGenerator();
        }

        public void Recycle(T item) {
            objects.Add(item);
        }
    }
}
