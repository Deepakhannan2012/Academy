// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement the Double Ended Queue collection using a circular buffer
// ------------------------------------------------------------------------------------------------
using static System.Console;
namespace A10._2;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      TQueue<int> TQueue = new ();
      TQueue.EnqueueRear (1);
      TQueue.EnqueueRear (2);
      TQueue.EnqueueFront (3);
      TQueue.EnqueueFront (4);
      TQueue.EnqueueRear (5);
      TQueue.EnqueueFront (6);
      TQueue.DequeueRear ();
      TQueue.DequeueFront ();
      TQueue.EnqueueFront (7);
      Write ($"Custom TQueue:\nCount => {TQueue.Count}" +
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
   public T DequeueFront () {
      IsValidOp ();
      ModCapacity (ref mStart);
      mCount--;
      return mItems[mStart++];
   }

   // Removes and returns the element front the back of the queue
   public T DequeueRear () {
      IsValidOp ();
      ModCapacity (ref mEnd);
      mCount--;
      return mItems[mEnd--];
   }

   // Adds the element to the front of the queue
   public void EnqueueFront (T a) {
      if (mCount == Capacity) Resize ();
      mStart--;
      ModCapacity (ref mStart);
      mItems[mStart] = a;
      mCount++;
   }

   // Adds the element to the back of the queue
   public void EnqueueRear (T a) {
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
   void ModCapacity (ref int num) => num = (num >= 0) ? num % Capacity : num + Capacity;

   // Doubles the size of the underlying array
   void Resize () {
      T[] tempArr = new T[Capacity * 2];
      for (int i = 0; i < Capacity; i++) {
         ModCapacity (ref mStart);
         tempArr[i] = mItems[mStart++];
      }
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
