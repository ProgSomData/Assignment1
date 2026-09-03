# Assignment 1

For all exercises, we have commented out old code and added our new solutions in the lines underneath.
We have commented in each file where an assignment is answered:

Example: exercise 2.1 in intcomp1.fs

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
//  | Let(x, erhs, ebody) -> 
    //   let xval = eval erhs env
    //   let env1 = (x, xval) :: env 
    //   eval ebody env1
    //2.1
    | Let (xs, ebody) ->
        //helper function that binds all x's in the list, since eval wont on a list
        let rec aux bindings env = 
            match bindings with
            | [] -> env
            | (x, erhs) :: xs -> 
                let xval = eval erhs env
                let env1 = (x, xval) :: env
                aux xs env1
        let finalEnv = aux xs env
        eval ebody finalEnv
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive";;
