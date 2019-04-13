// Learn more about F# at http://fsharp.org

open System
open Microsoft.FSharp.Core
open System.Text.RegularExpressions
open System.Linq

module StringCalculator = 
// Function that checks if an string is number
 let isDigit d = fst (System.Int32.TryParse(d))
 // get numbers partition 
 let getNumbers  input =
   let m = Regex.Match(input,@"^(//.*\n)(.*)") 
   if (m.Success) then   m.Groups.[2].Value else input 
// function to check the pattern and return first group
 let (|FirstRegexGroup|_|) pattern input =
   let m = Regex.Match(input,pattern) 
   if (m.Success) then Some m.Groups.[1].Value else None  

// get delimiter based on a pattern
 let getDelimiter s : string []=
     match s with
    | FirstRegexGroup  "^(//\[.*\]\n)(.*)" host -> 
          host.Substring(2,(host.LastIndexOf("\n")-2)).Replace(']',' ').Replace('[',' ' ).Split(" ").Where(fun n-> n <> "").ToArray()
                     
    | FirstRegexGroup "^(//.*\n)(.*)" host -> 
             host.Substring(2,(host.LastIndexOf("\n")-2)).Split("")
    | _ -> [|","; "\n"|]  
    
// add numbers function that calculate sume of numbers in string 
 let addNumbers (input:string):int =
  try
// check if input is empty
  if(String.IsNullOrEmpty(input)) then 0 else
// if input is followed by two chacter
   if(Regex.IsMatch(input, "\d,\n")) then -1 else 
// get delimiter based on a pattern 
  let d = getDelimiter (input)
// get numbers part
  let  values = getNumbers input
// split input by delimiter 
  let numbers = values.Split(d, StringSplitOptions.None)
 // handle negative numbers
  if( Array.exists (fun elem -> isDigit elem && System.Int32.Parse elem < 0) numbers)  
  then failwith("Negatives not allowed: " + String.Join(", ",  numbers |> Seq.filter (fun x ->   isDigit x && System.Int32.Parse x < 0))) 
  else
// parse each string to int and then sum the numbers in the list 
  let sum =   numbers |> Seq.filter (fun x ->  isDigit x)  |> Seq.map System.Int32.Parse
                      |> Seq.filter (fun x -> x <=1000) |> Seq.sum
  (sum)
   with
    | :? System.Exception as ex -> 
          Console.Write( ex.Message); -1
[<EntryPoint>]
let main arge = 
// Test 1 : "1"
System.Console.WriteLine(String.Concat("Test 1: (1) ",  StringCalculator.addNumbers ("1")))
// Test 2 : ""
System.Console.WriteLine(String.Concat(@"Test 2: ("") ",StringCalculator.addNumbers ("")))
// Test 3 : test case when send ; delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 3: with ; as delimiter  (//;\n1;2;4) ",StringCalculator.addNumbers ("//;\n1;2;4")))
// Test 4 : test case when send invalid delimiter
System.Console.WriteLine(String.Concat(@"Test 4: (1;2;4) ",StringCalculator.addNumbers ("1;2;4")))
// Test 5 : test case when send,  delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 5: (//,\n1,2,9) ",StringCalculator.addNumbers ("//,\n1,2,9")))
// Test 7 : test case when send delimiter is not in  first line 
System.Console.WriteLine(String.Concat(@"Test 7: (1,2\n3) ",StringCalculator.addNumbers ("1,2\n3")))
// Test 8 : test case when send \n  delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 8: (//[***]\n1***2***9) ",StringCalculator.addNumbers ("//[***]\n1***2***9")))
// invlaid input return -1
System.Console.WriteLine(String.Concat(@"Test 9: invalid input return -1 (1,\n) ",StringCalculator.addNumbers ("1,\n")))
// ignore numbers bigger than 1000
System.Console.WriteLine(String.Concat(@"Test 10: Test Case when a number bigger than 1001 (1,1000,1001,500) ",StringCalculator.addNumbers ("1,1000,1001,500")))
//negative numbers
System.Console.WriteLine(String.Concat(@" //Test 11: Calling Add with a negative number (1,-9,-8) ",StringCalculator.addNumbers ("1,-9,-8")))
// Test 8 : test case when send \n  delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 12 multiple delimiter: (//[*][%]\n1*2%3) ",StringCalculator.addNumbers ("//[*][%]\n1*2%3")))
// Test 8 : test case when send \n  delimiter at first line 
System.Console.WriteLine(String.Concat(@"Test 12 multiple delimiter: (//[***][%]\n1***2%3) ",StringCalculator.addNumbers ("//[***][%]\n1***2%3")))
0