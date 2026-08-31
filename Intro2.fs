(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;


(* Object language expressions with variables *)

type expr = 
    | CstI of int
    | Var of string
    | Prim of string * expr * expr
    | If of expr * expr * expr

//1.1(ii)
let e1 = CstI 17;;
let e2 = Prim("+", CstI 3, Var "a");;
let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;
let e4 = Prim ("max", (Prim("+", CstI 5, CstI 10)), (CstI 16))
(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x -> lookup env x
    | Prim (ope, e1, e2) -> 
        let i1 = eval e1 env
        let i2 = eval e2 env
        match ope with
            | "+" -> i1 + i2
            | "*" -> i1 * i2
            | "-" -> i1 - i2
            | "==" -> if i1 = i2 then 1 else 0
            | "max" -> if i1 > i2 then i1 else i2
            | "min" -> if i1 < i2 then i1 else i2
            | _ -> failwith "unknown primitive"
    | If (e1, e2, e3) -> 
        let i1 = eval e1 env
        let i2 = eval e2 env
        let i3 = eval e3 env
        if i1 <> 0 then i2 else i3

    //old eval patterns
    // | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    // | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    // | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    // | Prim("==", e1, e2)-> if eval e1 env = eval e2 env then 1 else 0
    // | Prim("max", e1, e2) -> if eval e1 env > eval e2 env then eval e1 env else eval e2 env
    // | Prim("min", e1, e2) -> if eval e1 env < eval e2 env then eval e1 env else eval e1 env 
    // | Prim _            -> failwith "unknown primitive";;


//(V test)
let env2: (string * int) list = [("a", 1); ("b", 1)]
let env3: (string * int) list = [("a", 1); ("b", 2)]
let e5 = Prim("==", Var "a", Var "b")
let e6 = If(e5, CstI 11, CstI 22)

eval e6 env2
eval e6 env3


let e1v = eval e4 env;;
let e1v2  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;



// 1.2 (i)
type aexpr = 
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr 
    | Sub of aexpr * aexpr

// 1.2 (ii)
Sub(Var "v", Add(Var "w", Var "z"))
Mul (CstI 2, Sub(Var "v", Add(Var "w", Var "z")))

// 1.2 (iii)
let rec fmt (e : aexpr) : string = 
    match e with
    | CstI i -> string i
    | Var x -> x
    | Add(e1, e2) -> "(" + fmt e1 + " + " + fmt e2 + ")"
    | Mul(e1, e2) -> "(" + fmt e1 + " * " + fmt e2 + ")"
    | Sub(e1, e2) -> "(" + fmt e1 + " - " + fmt e2 + ")"
    | _ -> failwith "unknown aexpr"


// 1.2 (iv)
let rec simplify (e : aexpr) : aexpr = 
    match e with
    | Add(CstI 0, e) -> e
    | Add(e, CstI 0) -> e
    | Sub(e, CstI 0) -> e
    | Mul(CstI 1, e) -> e
    | Mul(e, CstI 1) -> e
    | Mul(CstI 0, e) -> CstI 0
    | Mul(e, CstI 0) -> CstI 0
    | Sub(e, e1) when e = e1 -> CstI 0
    | _ -> e

// 1.2 (v)
let rec symDiff (x : string) (e : aexpr) : aexpr =
    match e with
    | CstI i -> CstI 0
    | Var y -> if y = x then CstI 1 else CstI 0 
    | Add(e1, e2) -> Add(symDiff x e1, symDiff x e2) 
    | Mul(e1, e2) -> Add((Mul(symDiff x e1, e2)),(Mul(e1, symDiff x e2)))
    | Sub(e1, e2) -> Sub(symDiff x e1, symDiff x e2)