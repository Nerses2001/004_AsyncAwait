

using System.Runtime.CompilerServices;

namespace _004_AsyncAwait
{
    internal class MyClass
    {
        public void Operation()
        {
            Console.WriteLine("Operation ThreadId {0}",Environment.CurrentManagedThreadId);
            Console.WriteLine("Begin");
            Console.WriteLine("End");
        }

        AsyncStateMachine _stateMachine;

        public void OperationAsync()
        {
            _stateMachine.outer = this;
            _stateMachine.builder = AsyncVoidMethodBuilder.Create();
            _stateMachine.state = -1;
            _stateMachine.builder.Start(ref _stateMachine);
        }
        private struct AsyncStateMachine : IAsyncStateMachine
        {
            public int state;

            int counterCallMoveNext;

            public MyClass outer;
            public AsyncVoidMethodBuilder builder;

            void IAsyncStateMachine.MoveNext()
            {
                Console.WriteLine("\n Method MoveNext calles {0}- time is the thread id {1}", ++counterCallMoveNext, Environment.CurrentManagedThreadId);

                if (state == -1)
                {
                    Console.WriteLine("OperationAsync (Part I) ThreadId {0}", Environment.CurrentManagedThreadId);

                    Task task = new(outer.Operation);

                    task.Start();

                    state = 0;
                    TaskAwaiter awaiter = task.GetAwaiter();

                    builder.AwaitOnCompleted(ref awaiter, ref this);

                    return;
                }
                Console.WriteLine("OperationAsync (Part II) ThreadId {0}", Environment.CurrentManagedThreadId);
            }

            readonly void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
                Console.WriteLine("Method SetStateMachine with Thread Id {0}",
                    Environment.CurrentManagedThreadId);

                Console.WriteLine("stateMachine GetHashCode {0}", stateMachine.GetHashCode());

                Console.WriteLine("this.GetHashCode {0}", this.GetHashCode());
            }
        }
    }
}
