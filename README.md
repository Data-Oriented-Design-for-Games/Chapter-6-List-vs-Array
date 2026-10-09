# Chapter 6 — List vs. Array

Sample project for **Chapter 6** of [*High Performance Unity Game Development (Using data-oriented design)*](https://www.manning.com/books/high-performance-unity-game-development) by Nitzan Wilnai (Manning).

This is a small timing test. It runs the same loops over a `List<int>` and over an `int[]` of
the same size, and compares the times. The question it answers: for simple work on every
element, how much does a `List` cost compared to a plain array?

## What it shows

- A `List<int>` wraps an array. Every `IntList[i]` goes through the list's indexer before it
  reaches that array. An `int[]` is accessed directly.
- Test 1 adds one to every element.
- Test 2 adds two collections together, element by element, and stores the result in a third.
- The lists are created with their full capacity and filled before the timer starts. Growing
  the list is not part of the measurement. Only the loops are timed.

## How the test works

All of the code is in one file, `Assets/Scripts/Main.cs`.

- `Main.Start` creates the lists and arrays used by Test 2.
- `Main.RunTest` is called by the **Run Test** button. It sets a flag.
- `Main.Update` sees the flag, runs Test 1 in one frame and Test 2 in the next, and writes the
  results.

Each test loops over `ArraySize` elements and repeats that `NumIterations` times. The list loop
and the array loop are timed separately with `Time.realtimeSinceStartupAsDouble`, and the times
are added up. This is Test 1:

```csharp
time = Time.realtimeSinceStartupAsDouble;
for (int i = 0; i < ArraySize; i++)
    IntList[i]++;
listTime += Time.realtimeSinceStartupAsDouble - time;

time = Time.realtimeSinceStartupAsDouble;
for (int i = 0; i < ArraySize; i++)
    IntArray[i]++;
arrayTime += Time.realtimeSinceStartupAsDouble - time;
```

The scene sets `ArraySize` to 100000 and `NumIterations` to 1000. The defaults written in
`Main.cs` are different (10000 for both), but the values saved in the scene are the ones used.

## Running it

1. Open the project in Unity **6000.3.22f1** (Unity 6.3 LTS) or newer.
2. Open `Assets/Scenes/MainScene.unity` and press **Play**.
3. Click the **Run Test** button. The screen does not update while a test runs. Results appear
   as text on screen, Test 1 first and then Test 2. Nothing is written to the Console.

To change the size of the test, select the `Main` object in the scene and edit `ArraySize` and
`NumIterations` in the Inspector.

## Reading the results

```
Test 1 of 2:
List Increment <seconds>
Array Increment <seconds>
Array is faster than List by <ratio>

Test 2 of 2:
List Addition <seconds>
Array Addition <seconds>
Array is faster than List by <ratio>
```

- The first two numbers in each test are total times in seconds.
- The ratio is the list time divided by the array time. A value of 2 means the list loop took
  twice as long as the array loop.
- If the list is faster, the last line reads `Array is slower than List by` instead. The ratio
  is still list time divided by array time, so it is below 1.
- The numbers depend on your machine and on whether you run in the Editor or in a built player.
  Run it a few times before you compare.

## More samples

All sample projects for the book: https://github.com/Data-Oriented-Design-for-Games
