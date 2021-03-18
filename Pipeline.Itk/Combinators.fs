module Pipeline.Itk.Combinators


let Pipe f g x = g(f x)
let Chain f g x = f(g x)(x)
let Converge f g h x = f(g x)(h x)