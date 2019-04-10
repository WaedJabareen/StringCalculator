// Learn more about F# at http://fsharp.org

open System
open Microsoft.FSharp.Core
open System.Text.RegularExpressions

module StringCalculator = 
// Function that checks if an string is number
 let isDigit x = fst (System.Int32.TryParse(x))

// add numbers function that calculate sume of numbers in string 
 let addNumbers (input, pattern :string):int =
// check if input is empty
  if(input ="") then 0 else
// if input does not match pattern return -1 
  if(Regex.IsMatch(input, pattern) <> true) then -1 else 
// split input by delimiter 
  let numbers = input.Split([|"\n"; ","|], StringSplitOptions.None)
// parse each string to int and then sum the numbers in the list 
  let sum =   numbers |> Seq.filter (fun x ->  isDigit x)  |> Seq.map System.Int32.Parse  |> Seq.sum
  (sum)

[<EntryPoint>]
let main arge = 
// Test 1 : "1"
System.Console.WriteLine(String.Concat("Test 1: (1) ", StringCalculator.addNumbers ("1" ,"^[0-9]\n,?")))
// Test 2 : ""
System.Console.WriteLine(String.Concat(@"Test 2: ("") ",StringCalculator.addNumbers ("", "^[0-9]\n,?")))
// Test 3 : "1\n,2,3\n6\n7"
System.Console.WriteLine(String.Concat(@"Test 3: (1\n,2,3\n6\n7) ",StringCalculator.addNumbers ("1\n,2,3\n6\n7" ,"^[0-9]\n,?")))
// Test 4 : "1,\n"
System.Console.WriteLine(String.Concat(@"Test 3: (1,\n) ",StringCalculator.addNumbers ("1,\n" ,"^[0-9]\n,?")))
 

0