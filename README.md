# 004_AsyncAwait
## Manual Asynchronous Implementation in C#

This project illustrates the implementation of asynchronous operations in C# without using the `async` and `await` keywords. Instead, it utilizes the `AsyncStateMachine` structure.

## The AsyncStateMachine Structure

```csharp
private struct AsyncStateMachine : IAsyncStateMachine
{
    // Field to store the state of the asynchronous operation.
    public int state;

    // Counter for the MoveNext method calls.
    int counterCallMoveNext;

    // Reference to the MyClass object for invoking asynchronous methods.
    public MyClass outer;

    // Builder for creating asynchronous methods.
    public AsyncVoidMethodBuilder builder;

    // Method implementing asynchronous execution.
    void IAsyncStateMachine.MoveNext()
    {
        Console.WriteLine("\n Method MoveNext called {0} times, thread id: {1}", ++counterCallMoveNext, Environment.CurrentManagedThreadId);

        if (state == -1)
        {
            // Logic for executing the asynchronous operation - Part I.
        }
        else
        {
            // Logic for executing the asynchronous operation - Part II.
        }
    }

    // Method that sets the state machine object.
    readonly void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
        Console.WriteLine("Method SetStateMachine called, thread id: {0}", Environment.CurrentManagedThreadId);
        Console.WriteLine("stateMachine GetHashCode: {0}", stateMachine.GetHashCode());
        Console.WriteLine("this.GetHashCode: {0}", this.GetHashCode());
    }
}
```
## Purpose of the AsyncStateMachine Structure

The `AsyncStateMachine` structure represents a state machine used to manage the asynchronous execution of an operation. The `state` field holds the current state of the asynchronous operation, allowing us to determine which part of the operation to execute. The `counterCallMoveNext` field is used to count the `MoveNext()` method calls, aiding in tracking the execution flow.

The main `MoveNext()` method implements asynchronous execution. Depending on the current state (`state`), the corresponding part of the asynchronous operation is executed. The `SetStateMachine()` method sets the state machine object to ensure proper handling of asynchronous execution.

This approach allows manual control of asynchronous execution without using the `async` and `await` keywords.
