namespace StringTensionsCalculator.Tests

open System
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

type Notes =
    static member Note() =
        gen {
            let! height = Gen.choose(0, 8)
            let! name = Arb.from<NoteName> |> Arb.toGen
            return { name=name; height = height }
        }
        |> Arb.fromGen

type NotePropertyAttribute() =
    inherit PropertyAttribute(
        Arbitrary = [| typeof<Notes> |],
        QuietOnSuccess = true)
