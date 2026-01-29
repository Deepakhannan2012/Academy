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
      TDEndQueue<int> TQueue = new ();
      TestEnqueueRear (TQueue);
      TestEnqueueFront (TQueue);
      TestDequeueFront (TQueue);
      TestDequeueRear (TQueue);
   }

   #region Implementation -------------------------------------------
   // Tests the method DequeueFront
   static void TestDequeueFront (TDEndQueue<int> mQ) {
      WriteLine ("TestDequeueFront:");
      for (int i = 1; i <= 8; i++) mQ.EnqueueRear (i);
      bool passed = true;
      for (int j = 1; j <= 8; j++) {
         int val = mQ.DequeueFront ();
         WriteLine ($"Dequeued: {val}");
         if (val != j) passed = false;
      }
      if (!mQ.IsEmpty) passed = false;
      WriteLine ($"TestDequeueFront Passed? {passed}");
      WriteLine ();
   }

   // Tests the method DequeueRear
   static void TestDequeueRear (TDEndQueue<int> mQ) {
      WriteLine ("TestDequeueRear:");
      for (int i = 1; i <= 8; i++) mQ.EnqueueFront (i);
      bool passed = true;
      for (int j = 1; j <= 8; j++) {
         int val = mQ.DequeueRear ();
         WriteLine ($"Dequeued: {val}");
         if (val != j) passed = false;
      }
      if (!mQ.IsEmpty) passed = false;
      WriteLine ($"TestDequeueRear Passed? {passed}");
      WriteLine ();
   }

   // Tests the method EnqueueFront
   static void TestEnqueueFront (TDEndQueue<int> mQ) {
      WriteLine ("TestEnqueueFront:");
      for (int i = 1; i <= 4; i++) mQ.EnqueueFront (i);
      bool passed = true;
      for (int j = 1; j <= 4; j++) {
         int val = mQ.DequeueRear ();
         WriteLine ($"Dequeued: {val}");
         if (val != j) passed = false;
      }
      WriteLine ($"TestEnqueueFront Passed? {passed}");
      WriteLine ();
   }

   // Tests the method EnqueueRear
   static void TestEnqueueRear (TDEndQueue<int> mQ) {
      WriteLine ("TestEnqueueRear:");
      for (int i = 1; i <= 4; i++) mQ.EnqueueRear (i);
      bool passed = true;
      for (int j = 1; j <= 4; j++) {
         int val = mQ.DequeueFront ();
         WriteLine ($"Dequeued: {val}");
         if (val != j) passed = false;
      }
      WriteLine ($"TestEnqueueRear Passed? {passed}");
      WriteLine ();
   }
   #endregion
}
#endregion

#region class TQueue<T> ---------------------------------------------------------------------------
class TDEndQueue<T> {
   #region Constructor ----------------------------------------------
   public TDEndQueue () {
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
      --mEnd;
      ModCapacity (ref mEnd);
      mCount--;
      return mItems[mEnd];
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
