// Learn more about F# at http://fsharp.org

open System
open Microsoft.FSharp.Core
open System.Text.RegularExpressions

module StringCalculator = 
// Function that checks if an string is number
 let isDigit d = fst (System.Int32.TryParse(d))
// get delimiter based on a pattern
 let getDelimiter (s,pattern:string) =
   let m = Regex.Match(s, pattern) 
   if (m.Success) then 
   let d = Regex.Match(s,pattern).Groups.[1].Value
   d.Substring(2,(d.LastIndexOf("\n")-2)) else ","  
    
// add numbers function that calculate sume of numbers in string 
 let addNumbers (input:string):int =
// check if input is empty
  if(String.IsNullOrEmpty(input)) then 0 else
// if input is followed by two chacter
  if(Regex.IsMatch(input, "\d[^a-z0-9 ][^a-z0-9 ]")) then -1 else 
// split input by delimiter 
  let d = getDelimiter (input, "^(//.*\n)(.*)")
  let numbers = input.Split([|d; "\n"|], StringSplitOptions.None)
// parse each string to int and then sum the numbers in the list 
  let sum =   numbers |> Seq.filter (fun x ->  isDigit x)  |> Seq.map System.Int32.Parse  |> Seq.sum
  (sum)

[<EntryPoint>]
let main arge = 
// Test 1 : "1"
System.Console.WriteLine(String.Concat("Test 1: (1) ", StringCalculator.addNumbers ("1")))
// Test 2 : ""
System.Console.WriteLine(String.Concat(@"Test 2: ("") ",StringCalculator.addNumbers ("")))
// Test 3 : test case when send ; delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 3: with ; as delimiter  (//;\n1;2;4) ",StringCalculator.addNumbers ("//;\n1;2;4")))
// Test 4 : test case when send invalid delimiter
System.Console.WriteLine(String.Concat(@"Test 4: (1;2;4) ",StringCalculator.addNumbers ("1;2;4")))
// Test 5 : test case when send,  delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 5: (//,\n1,2,9) ",StringCalculator.addNumbers ("//,\n1,2,9")))
// Test 6 : test case when send \n  delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 6: (//\n\n1\n2\n9) ",StringCalculator.addNumbers ("//\n\n1\n2\n9")))
// Test 7 : test case when send delimiter is not in  first line 
System.Console.WriteLine(String.Concat(@"Test 7: (1,2\n3) ",StringCalculator.addNumbers ("1,2\n3")))
// invlaid input return -1
System.Console.WriteLine(String.Concat(@"Test 8: invalid input return -1 (1,\n) ",StringCalculator.addNumbers ("1,\n")))

0