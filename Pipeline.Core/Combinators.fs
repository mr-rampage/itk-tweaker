module Pipeline.Core.Combinators

let Thrush x f = f x
let Flip f y x = f x y
let Chain f g x = f(g x)(x)
let Converge f g h x = f(g x)(h x)