# Assignment 1
Exercise 1.1
An expression type was made for constants, variables, operations, and if expressions.

The environment is a list of variable names and values. The lookup function finds the value of a variable. The eval function uses pattern matching to evaluate the expressions.

Exercise 1.2
A new type was made for arithmetic expressions.

fmt changes an expression into a readable string.
simplify removes simple parts such as + 0 and * 1.
symDiff finds the derivative of an expression using basic differentiation rules.
Exercise 2.1
Let bindings were added to the expression type.

A helper function goes through the bindings one by one. Each value is evaluated and then added to the environment. This also handles nested let expressions and variables with the same name.

Exercise 2.2
freevars finds variables that are not bound inside an expression.

union is used to combine variable lists, and minus is used to remove bound variables.

Exercise 2.3
Expressions are compiled by changing variable names into numbers. getindex finds the position of each variable in the compile-time environment.

The compiled expressions are then evaluated using a list of values.

Substitution
Two substitution functions were made.

nsubst replaces variables directly but can cause variable capture. subst avoids this by giving bound variables new names using newVar.

Stack Machine
A simple stack machine was made for arithmetic expressions. rcomp changes an expression into stack instructions, and reval runs the instructions.

A second stack machine was made where both values and variables use the same stack. scomp keeps track of the stack and creates the correct instructions.

Testing
Test expressions and environments are included in the code. They are used to check evaluation, let bindings, substitution, free variables, compilation, and stack-machine results.
