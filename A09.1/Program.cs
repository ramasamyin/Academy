// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// To implement a TQueue<T>
// ------------------------------------------------------------------------------------------------
namespace A09._1;

class Program {
   static void Main (string[] args) {
      TQueue<int> queue = new ();
      for (int i = 1; i <= 10; i++) queue.Enqueue (i);
      for (int i = 1; i <= 10; i++) Console.WriteLine (queue.Dequeue ());
   }
}
class TQueue<T> {

   public void Enqueue (T a) {
      if (mCount == mData.Length) {
         int newCapacity = mData.Length * 2;
         T[] newArr = new T[newCapacity];
         for (int i = 0; i < mCount; i++) newArr[i] = mData[(mFront + i) % mData.Length];
         mData = newArr;
         mFront = 0;
         mTail = mCount;
      }
      mData[mTail] = a;
      mTail = (mTail + 1) % mData.Length;
      mCount++;
   }

   public T Dequeue () {
      if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
      T value = mData[mFront];
      mFront = (mFront + 1) % mData.Length;
      mCount--;
      return value;
   }

   public bool IsEmpty => mCount == 0;

   T[] mData = new T[8];
   int mCount = 0;
   int mFront = 0;
   int mTail = 0;
}
