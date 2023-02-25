#region AutoResetEvent

/*

AutoResetEvent _workerEvent = new AutoResetEvent(false);
AutoResetEvent _mainEvent = new AutoResetEvent(false);


Thread t = new Thread(AnyProcess);
t.Start();


Console.WriteLine("Waiting for Proc function");
_workerEvent.WaitOne();


Console.WriteLine("Starting some main process!");
for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Main : {i}");
    Thread.Sleep(1000);
}


_mainEvent.Set();
Console.WriteLine("Worker is doing some job. Let's wait it!");
_workerEvent.WaitOne();
Console.WriteLine("Completed!");


void AnyProcess()
{
    Console.WriteLine("Starting some function process!");
    Thread.Sleep(3000);
    Console.WriteLine("Okay!");
    _workerEvent.Set();
    Console.WriteLine("Main thread id working. I am waiting for it!");
    _mainEvent.WaitOne();

    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Proc : {i}");
        Thread.Sleep(1000);
    }

    _workerEvent.Set();
}

*/

#endregion










#region CreatingTask


// void TaskMethod(string name)
// {
//     Console.WriteLine($@"{name} is running
//     Id: {Thread.CurrentThread.ManagedThreadId}
//     IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
// }



// new Task(() =>
// {
//     Console.WriteLine("xxx");
//     Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
//     Console.WriteLine(Thread.CurrentThread.IsBackground);
// }).Start();
// 





//// Way 1
// var t1 = new Task(() =>
// {
//     for (int i = 0; i < 100; i++)
//     {
//         Console.WriteLine(i);
//     }
// });
// 
// t1.Start();


// new Task(() =>
// {
//     TaskMethod("TTT");
// }).Start();





// Way 2

// Task.Factory.StartNew(() => { TaskMethod("Task3"); });
// var t3 = Task.Factory.StartNew(() => { TaskMethod("Task3"); });



// Way 3
// Task.Run(() => { TaskMethod("Task4"); });
// Task t4 = Task.Run(() => { TaskMethod("Task4"); });

// Console.ReadLine();


#endregion






#region Status, Func

//int TaskMethod(object? name)
//{
//    Console.WriteLine($@"{name} is running
//    Id: {Thread.CurrentThread.ManagedThreadId}
//    IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");

//    Thread.Sleep(3000);

//    return 42;
//}


// TaskMethod("Main Thread Task");




// Task<int>? t1 = new Task<int>(TaskMethod, "t1");
// t1.Start();


// int result;

//// result = t1.Result; // Join
//result = await t1;
//Console.WriteLine(result);




//Task<int>? t2 = new Task<int>(TaskMethod, "t2");
//t2.RunSynchronously();
//Console.ReadLine();




//t1.ContinueWith((t) =>
//{
//    Console.WriteLine("ContinueWith");
//    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//});





//Task tt1 = new Task(() =>
//{
//    Thread.Sleep(3000);
//    Console.WriteLine("tt1");
//});

//Task tt2 = new Task(() =>
//{
//    Thread.Sleep(5000);
//    Console.WriteLine("tt2");
//});


//tt1.Start();
//tt2.Start();

//await Task.WhenAll(tt1, tt2).ContinueWith((t) =>
//{
//    Console.WriteLine("WhenAll ContinueWith");
//});


//await Task.WhenAll(tt1, tt2);
//Console.WriteLine("Hakuna");


//await Task.WhenAny(tt1, tt2);
//Console.WriteLine("Hakuna");
//Console.ReadLine();






// Status
 
 //Task<int> t3 = new Task<int>(() => TaskMethod("Task 3"));

 //Console.WriteLine(t3.Status);
 //t3.Start();
 //Console.WriteLine(t3.Status);
 
 //while (!t3.IsCompleted)
 //{
 //    Console.WriteLine(t3.Status);
 //    Thread.Sleep(500);
 //}
 
 //Console.WriteLine(t3.Status);





//var result = Task.Delay(5);
//Console.WriteLine(result.Status);
//Console.WriteLine("Finished.");


#endregion







int TaskMethod(string name, int seconds)
{
    Console.WriteLine($@"{name} is running
        Id: {Thread.CurrentThread.ManagedThreadId}
        IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");

    //throw new NotImplementedException();

    Thread.Sleep(TimeSpan.FromSeconds(seconds));

    return 42;
}




var firstTask = new Task<int>(() => TaskMethod("FirstTask", 3));

//firstTask.ContinueWith((task) =>
//{
//    Console.WriteLine($@"First Result: {task.Result}
//    Id: {Thread.CurrentThread.ManagedThreadId}
//    IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
//});


var secondTask = new Task<int>(() => TaskMethod("SecondTask", 2));

// secondTask.ContinueWith((task) =>
// {
//     Console.WriteLine($@"Second Result: {task.Result}
//     Id: {Thread.CurrentThread.ManagedThreadId}
//     IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
// }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);


//secondTask.ContinueWith((task) =>
//{
//    Console.WriteLine($@"Second Result: {task.Result}
//    Id: {Thread.CurrentThread.ManagedThreadId}
//    IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
//}, TaskContinuationOptions.LongRunning);



// firstTask.Start();
// secondTask.Start();



//Console.ReadLine();



//  NotOnRanToCompletion = 0x10000,
//  NotOnFaulted = 0x20000,
//  NotOnCanceled = 0x40000,

// OnlyOnRanToCompletion = NotOnFaulted | NotOnCanceled,
// OnlyOnFaulted = NotOnRanToCompletion | NotOnCanceled,
// OnlyOnCanceled = NotOnRanToCompletion | NotOnFaulted









var thirdTask = new Task<int>(() =>
{
    var innerTask = Task.Factory.StartNew(
        () => { TaskMethod("A", 2); }, TaskCreationOptions.AttachedToParent);


    innerTask.ContinueWith(t => TaskMethod("Inner Task Continue", 7));

    return TaskMethod("B", 1);

});

thirdTask.Start();


while (!thirdTask.IsCompleted)
{
    Console.WriteLine(thirdTask.Status);
    Thread.Sleep(100);
}

Console.WriteLine(thirdTask.Status);

Console.ReadLine();