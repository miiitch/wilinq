
module public ParserHelpers

open System

/// Intercepts parsing error
let internal handleParsingError (ctxt: Microsoft.FSharp.Text.Parsing.ParseErrorContext<_>)=
    raise(Exception(ctxt.Message))
    
/// Trims the field name from lexer input
let internal trimField (field: System.String) =
    field.TrimStart( [|'['|])
         .TrimEnd([|']'|])

/// Simplifies strings from lexer input
let internal removeDoubledQuotes (str:System.String) = 
    str.Replace("''","'")
    
/// Simplifies strings from lexer input
let internal removeDoubledDoubleQuotes (str:System.String) = 
    str.Replace("\"\"","\"")
        
/// case-insensitive string comparison
let inline (=.) (x: string) (y: string) =     
    StringComparer.InvariantCultureIgnoreCase.Compare(x,y) = 0        /// handle today value statement (too complex to be done in the fslex file)let internal processTodayStatement (today: string) (n: int) =     if today =. "today"        then WiLinq.LinqProvider.Wiql.ValueStatement.CreateToday(-n)        else raise (System.ArgumentException("parameter is not today"))

/// handle today value statement (too complex to be done in the fslex file)
let internal processTodayStatement (today: System.String) (n: System.Int32): WiLinq.LinqProvider.Wiql.ValueStatement=
     if today =. "today"
        then WiLinq.LinqProvider.Wiql.ValueStatement.CreateToday(-n)
        else raise (System.ArgumentException("parameter is not today"))

let internal processValueStatementList (values:WiLinq.LinqProvider.Wiql.ValueStatement list) =
    new WiLinq.LinqProvider.Wiql.ListValueStatement(new System.Collections.Generic.List<WiLinq.LinqProvider.Wiql.ValueStatement>(values |> List.rev) ) :> WiLinq.LinqProvider.Wiql.ValueStatement


/// Creates a test statement from lexer input 
let internal generateTestStatement  (field,fieldType) op value=
    new WiLinq.LinqProvider.Wiql.FieldOperationStatement(field,op,value,fieldType) :> WiLinq.LinqProvider.Wiql.WhereStatement

/// Creates a flat query
let internal generateQuery (fieldList: System.String list) (whereList: (WiLinq.LinqProvider.Wiql.WhereStatement list) option) (orderList: (WiLinq.LinqProvider.Wiql.OrderStatement list) option)=
    let query = new WiLinq.LinqProvider.Wiql.Query()
    query.Fields.AddRange(fieldList  |> List.rev)
    match whereList with   
    | Some statements -> query.WhereStatements.AddRange(statements |> List.rev)
    | None -> ()
    match orderList with
    | Some statements -> query.OrderStatements.AddRange(statements |> List.rev)
    | None -> ()
    query;

/// Creates a hierarchical query
let internal generateQueryForLinks (fieldList: System.String list) (whereList: (WiLinq.LinqProvider.Wiql.WhereStatement list) option)  (orderList: (WiLinq.LinqProvider.Wiql.OrderStatement list) option) (queryMode: WiLinq.LinqProvider.Wiql.QueryMode)=
    let query = new WiLinq.LinqProvider.Wiql.Query(queryMode)
    query.Fields.AddRange(fieldList  |> List.rev)
    match whereList with   
    | Some statements -> query.WhereStatements.AddRange(statements |> List.rev)
    | None -> ()
    match orderList with
    | Some statements -> query.OrderStatements.AddRange(statements |> List.rev)
    | None -> ()
    query;

