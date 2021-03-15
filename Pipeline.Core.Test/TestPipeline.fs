namespace Pipeline.Core.Test

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Pipeline.Core

[<TestClass>]
type TestPipeline () =

    [<TestMethod>]
    member this.TestAppendPipelineStage () =
        let pipeline = [1;2;3]
        let append = Pipeline.Append pipeline 4
        Assert.AreEqual([1;2;3;4], append)

    [<TestMethod>]
    member this.TestRemovePipelineStage () =
        let pipeline = [1;2;3]
        let removed = Pipeline.Remove pipeline 2
        Assert.AreEqual([1;3], removed)

    [<TestMethod>]
    member this.TestInsertPipelineStage () =
        let pipeline = [1;2;3]
        let inserted = Pipeline.After pipeline 99 1
        Assert.AreEqual([1;2;99;3], inserted)
        
    [<TestMethod>]
    member this.TestRunPipeline () =
        let pipeline = [
            fun x -> x + 1;
            fun x -> x * 2;
            fun x -> x * x;
        ]
        let result = Pipeline.Run pipeline 4
        Assert.AreEqual([4;5;10;100], result);
      