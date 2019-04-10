# Equation Former

Program works in two modes: interactive (console) and file I / O.

Input is an equation or a file with a list of equations.

The program opens the brackets in the equation, leads similar terms, moves all the terms to the left.

Examples:

Input:
```
(5x+ 5y)/5 -x^2=1
```

Output:
`- x^2 + x + y - 1 = 0"`


Input:
`x^2 + 2y = 2((2x-1)/2 + 2.5(x + y^2) - 7.11)`


Output: 
`x^2 - 5y^2 - 7x + 2y + 15.22 = 0`
