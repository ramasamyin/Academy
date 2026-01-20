using System.Collections.Generic;

namespace A10._2;

class Program {
   static void Main () {
      TQueue<int> queue = new ();
      for (int i = 1; i <= 10; i++) queue.EnqueueRear (i);
      for (int i = 1; i <= 10; i++) Console.WriteLine (queue.DequeueFront ());
   }

   class TQueue<T> {
      public void EnqueueRear (T a) {
         if (mCount == mData.Length) Resize ();
         mData[mTail] = a;
         mTail = (mTail + 1) % mData.Length;
         mCount++;
      }

      public void EnqueueFront (T a) {
         if (mCount == mData.Length) Resize ();
         mFront = (mFront - 1 + mData.Length) % mData.Length;
         mData[mFront] = a;
         mCount++;
      }

      public T DequeueFront () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
         T value = mData[mFront];
         mFront = (mFront + 1) % mData.Length;
         mCount--;
         return value;
      }

      public T DequeueRear () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
         mTail = (mTail - 1 + mData.Length) % mData.Length;
         T value = mData[mTail];
         mCount--;
         return value;
      }

      public void Resize () {
         int newCapacity = mData.Length * 2;
         T[] newArr = new T[newCapacity];
         for (int i = 0; i < mCount; i++) newArr[i] = mData[(mFront + i) % mData.Length];
         mData = newArr;
         mFront = 0;
         mTail = mCount;
      }

      public bool IsEmpty => mCount == 0;

      T[] mData = new T[8];
      int mCount = 0;
      int mFront = 0;
      int mTail = 0;
   }
}
