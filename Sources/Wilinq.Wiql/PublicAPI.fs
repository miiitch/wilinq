// This project type requires the F# PowerPack at http://fsharppowerpack.codeplex.com/releases
// Learn more about F# at http://fsharp.net
// Original project template by Jomo Fisher based on work of Brian McNamara, Don Syme and Matt Valerio
// This posting is provided "AS IS" with no warranties, and confers no rights.

module WiLinq.Wiql.Parser

open System
open Microsoft.FSharp.Text.Lexing

open Lexer
open Parser

let public Process wiql = 
      try
     //   let parse_error_rich = fun (ctxt:Microsoft.FSharp.Text.Parsing.ParseErrorContext<_>) -> ctxt.Message

        let lexbuff = LexBuffer<char>.FromString(wiql)                               
        let equation = Parser.start Lexer.tokenize lexbuff
        equation
      with
        | ex -> raise ex