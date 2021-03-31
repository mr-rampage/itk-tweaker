module Pipeline.Core.Monad

type State<'Value, 'State> =
    State of ('State -> 'Value * 'State)
    
module State =
    let run (State f) = f 
    let ret x =
        State (fun s -> (x, s))
    
    let bind f x =
        State (fun state ->
            let x, state' = run x state
            run (f x) state'
        )
    let get = State (fun s -> (s, s))
    let put x = State (fun _ -> ((), x))

type StateBuilder() =
    member this.Return(x)= State.ret x
    member this.ReturnFrom(xS)= xS
    member this.Bind(xS,f)= State.bind f xS
    
let state = StateBuilder()