// Learn more about F# at http://fsharp.org

open System
open Microsoft.FSharp.Core
// add numbers function
let addNumbers (input:string):int = if(input ="") then 0 else
// split string by delimiter 
  let numbers = input.Split(',')
// parse each string to int and then sum the numbers in the list 
  let sum = numbers |> Seq.map System.Int32.Parse |> Seq.sum
  (sum)

[<EntryPoint>]
let main arge = 
// Test 1 : "1"
System.Console.WriteLine(String.Concat("Test 1: (1) ", addNumbers "1"))
// Test 2 : ""
System.Console.WriteLine(String.Concat(@"Test 2: ("") ",addNumbers ""))
// Test 3 : "1,2"
System.Console.WriteLine(String.Concat(@"Test 3: (1,2) ",addNumbers "1,2"))
 

0