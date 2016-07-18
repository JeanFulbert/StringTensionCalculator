namespace StringTensionsCalculator.Tests

open System
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

type FloatWithoutNaN() =
    static member Float() =
        Arb.Default.Float()
        |> Arb.filter (fun f ->
            not <| System.Double.IsNaN(f) &&
            not <| System.Double.IsInfinity(f)) 

type FloatWithoutNaNPropertyAttribute() =
    inherit PropertyAttribute(
        Arbitrary = [| typeof<FloatWithoutNaN> |],
        QuietOnSuccess = true)
