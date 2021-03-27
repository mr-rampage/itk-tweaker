module Pipeline.Core.Test.PipelineTest

open Microsoft.VisualStudio.TestTools.UnitTesting
open Pipeline.Core.Pipeline

[<TestClass>]
type TestPipeline() =
    let fakeAdapter = fun _ (i: int) -> i * 2

    [<TestMethod>]
    member this.TestConnectStages() =
        let e = "foo"
        let pipeline = [] |> AddStage fakeAdapter e
        Assert.AreEqual(198, pipeline.Head(99))

    [<TestMethod>]
    member this.TestRunPipeline() =
        let pipeline =
            []
            |> AddStage fakeAdapter "foo"
            |> AddStage fakeAdapter "bar"

        Assert.AreEqual([ 198; 396 ], RunPipeline pipeline 99)
