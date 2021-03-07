# Rule Server

## References

-   performance
    -   await/async
        -   model
            -   <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/task-asynchronous-programming-model>
        -   how to use
            -   <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/>
        -   create thread?
            -   <https://stackoverflow.com/a/59732462/9920172>
                -   > -   In conclusion, I'll say `async`/`await` code could use another thread, but only if the thread is created by another code, not by `async`/`await`.
                    > -   In this case, I think `Task.Delay` created the thread.
    -   tips
        -   <https://docs.microsoft.com/en-us/aspnet/core/performance/performance-best-practices?view=aspnetcore-5.0>
    -   rank of asp.net core
        -   <https://www.techempower.com/benchmarks/#section=data-r20&hw=ph&test=composite&a=2>
-   settings hot reload
    -   <https://stackoverflow.com/a/53061054/9920172>
-   expression tree
    -   <https://tyrrrz.me/blog/expression-trees>
