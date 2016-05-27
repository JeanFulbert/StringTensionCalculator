namespace StringTensionsCalculator

open System

type NoteName =
|A = 0
|ASharp = 1
|B = 2
|C = 3
|CSharp = 4
|D = 5
|DSharp = 6
|E = 7
|F = 8
|FSharp = 9
|G = 10
|GSharp = 11

type NoteHeight = int

[<CustomComparison; StructuralEquality>]
type Note =
    { name: NoteName
      height: NoteHeight
    }
    interface IComparable with
        member this.CompareTo other =
            match other with
            | :? Note as n -> compare (this.height, this.name) (n.height,n.name)
            | _ -> invalidArg "other" "cannot compare value of different types"
    override this.ToString() =
        let n = this.name |> sprintf "%A"
        let formattedName =
            match n with
            | x when x.EndsWith("Sharp") -> n.[0].ToString() + "#"
            | _ -> n
        formattedName + (this.height |> sprintf "%d")
        

[<Measure>] type hz