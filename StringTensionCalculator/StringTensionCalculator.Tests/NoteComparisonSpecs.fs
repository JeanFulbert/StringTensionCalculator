namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

module NoteComparisonSpecs =
    [<NoteProperty>]
    let ``Note is lesser than its upper octave`` (note:Note) =
        (note.height < 8) ==> lazy

        let octaveHeight = Gen.elements [1..(8 - note.height)] |> Arb.fromGen
        Prop.forAll octaveHeight (fun h ->
            let otherNote = { note with height = note.height + h }
            note <! otherNote)

    [<NoteProperty>]
    let ``Note is higher than its lower octave`` (note:Note) =
        (note.height > 0) ==> lazy

        let octaveHeight = Gen.elements [1..(note.height)] |> Arb.fromGen
        Prop.forAll octaveHeight (fun h ->
            let otherNote = { note with height = note.height - h }
            note >! otherNote)

    [<NoteProperty>]
    let ``Note is lower than note with a higher NoteName`` (note:Note) =
        (note.name < NoteName.GSharp) ==> lazy

        let nameAdd = Gen.elements [1..(12 - int note.name)] |> Arb.fromGen
        Prop.forAll nameAdd (fun n ->
            let otherNote = { note with name = enum ((int note.name) + n) }
            note <! otherNote)

    [<NoteProperty>]
    let ``Note is higher than note with a lower NoteName`` (note:Note) =
        (note.name > NoteName.A) ==> lazy

        let nameRemove = Gen.elements [1..(int note.name)] |> Arb.fromGen
        Prop.forAll nameRemove (fun n ->
            let otherNote = { note with name = enum ((int note.name) - n) }
            note >! otherNote)