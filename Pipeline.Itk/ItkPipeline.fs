module Pipeline.Itk.ItkPipeline

open Pipeline.Core
open Pipeline.Core.Pipeline
open Pipeline.Itk.ItkGaussian
open itk.simple

type AddStageEvent =
    | Identity of unit
    | GaussianBlur of sigma: double
    
let private AdaptAddStageEvent (event: AddStageEvent) =
    match (event) with
    | Identity () -> id
    | GaussianBlur σ -> ApplyGaussianBlur2D σ

type private AddStageEventMessage = (AddStageEvent * AsyncReplyChannel<TransformStage<Image>>)

let private processor = MailboxProcessor<AddStageEventMessage>.Start(fun inbox -> 
    let rec innerLoop pipeline = async {
        let! (event, reply) = inbox.Receive()
        let pipeline' = event |> AdaptAddStageEvent |> AddTransform pipeline
        reply.Reply(pipeline'.Head)
        return! innerLoop pipeline'
    }
    innerLoop [id])

let AddStage event = processor.PostAndReply(fun channel -> (event, channel))