module Pipeline.Core.Test.PipelineTest

open Microsoft.VisualStudio.TestTools.UnitTesting
open Pipeline.Core.Combinators
open Pipeline.Core.Pipeline

[<TestClass>]
type TestPipeline() =
    [<TestMethod>]
    member this.TestConnectStages() =
        let pipeline = AddTransform [] (fun i -> i * 2)
        Assert.AreEqual(198, pipeline.Head(99))

    [<TestMethod>]
    member this.TestRunPipeline() =
        let pipeline =
            []
            |> Flip AddTransform (fun i -> i * 2)
            |> Flip AddTransform (fun i -> i * 2)

        Assert.AreEqual([ 198; 396 ], RunPipeline pipeline 99)
