// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement the Queue collection using a circular buffer
// ------------------------------------------------------------------------------------------------
namespace A09._1;
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      Queue<int> queue = new ();
      TQueue<int> TQueue = new ();
      TQueue.Enqueue (1);
      queue.Enqueue (1);
      TQueue.Enqueue (2);
      queue.Enqueue (2);
      TQueue.Enqueue (3);
      queue.Enqueue (3);
      TQueue.Enqueue (4);
      queue.Enqueue (4);
      TQueue.Enqueue (5);
      queue.Enqueue (5);
      TQueue.Dequeue ();
      queue.Dequeue ();
      TQueue.Dequeue ();
      queue.Dequeue ();
      TQueue.Enqueue (6);
      queue.Enqueue (6);
      TQueue.Enqueue (7);
      queue.Enqueue (7);
      Write ("Comparing the default queue and the custom TQueue after a given set of operations:" +
             "\n\nDefault Queue:\nCount => {queue.Count}" +
             "\nCapacity => {queue.Capacity}\nElements => ");
      foreach (var item in queue) Write ($"{item} ");
      Write ($"\n\nCustom TQueue:\nCount => {TQueue.Count}" +
             $"\nCapacity => {TQueue.Capacity}\nElements => ");
      foreach (var item in TQueue) Write ($"{item} ");
      WriteLine ();
   }
}
#endregion

#region class TQueue<T> ---------------------------------------------------------------------------
class TQueue<T> {
   #region Constructor ----------------------------------------------
   public TQueue () {
      mItems = new T[4];
      (mCount, mStart, mEnd) = (0, 0, 0);
   }
   #endregion

   #region Properties -----------------------------------------------
   // Returns the size of the underlying array
   public int Capacity => mItems.Length;

   // Returns the number of elements in the queue
   public int Count => mCount;
   #endregion

   #region Methods --------------------------------------------------
   // Removes and returns the element from the front of the queue
   public T Dequeue () {
      IsValidOp ();
      ModCapacity (ref mStart);
      mCount--;
      return mItems[mStart++];
   }

   // Adds the element to the queue
   public void Enqueue (T a) {
      if (mCount == Capacity) Resize ();
      ModCapacity (ref mEnd);
      mItems[mEnd++] = a;
      mCount++;
   }

   // Checks if the queue is empty
   public bool IsEmpty => mCount == 0;

   // Returns the element from the front of the queue
   public T Peek () {
      IsValidOp ();
      ModCapacity (ref mStart);
      return mItems[mStart];
   }
   #endregion

   #region Implementation -------------------------------------------
   // Enables foreach iteration for the custom queue
   public IEnumerator<T> GetEnumerator () {
      for (int i = 0; i < mCount; i++) {
         int temp = mStart + i;
         ModCapacity (ref temp);
         yield return mItems[temp];
      }
   }

   // Throws an exception if the queue is empty
   void IsValidOp () {
      if (IsEmpty) throw new Exception ("Queue Empty");
   }

   // Returns the modulus of the input with the capacity of the queue
   void ModCapacity (ref int num) => num %= Capacity;

   // Doubles the size of the underlying array
   void Resize () {
      T[] tempArr = new T[Capacity * 2];
      int headPos = mStart;
      for (int i = 0; i < Capacity - headPos; i++) tempArr[i] = mItems[mStart++];
      for (int i = Capacity - 1; i >= Capacity - headPos; i--) tempArr[i] = mItems[--mEnd];
      (mStart, mEnd) = (0, Capacity);
      mItems = tempArr;
   }
   #endregion

   #region Private Data ---------------------------------------------
   // Underlying array for the queue
   T[] mItems;
   // Stores the size, start and end index of the queue
   int mCount, mStart, mEnd;
   #endregion
}
#endregion
