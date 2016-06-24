namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

module NoteAddNegativeSpecs =
    [<NoteProperty>]
    let ``Adding -n semitones, where n is a multiple of 12, decrease the height by modulo`` (note:Note) =
        (note.height > 0) ==> lazy

        let octaveHeight = Gen.elements [1..note.height] |> Arb.fromGen
        Prop.forAll octaveHeight (fun h ->
            let semitonesToAdd = h * 12
            let noteAfterNegativeAdded = note |> Notes.add -semitonesToAdd

            noteAfterNegativeAdded =! { note with height = note.height - h })

    [<NoteProperty>]
    let ``Adding -n semitones, where n is less than the name difference, decrease the name but not the height`` (note:Note) =
        (note.name > NoteName.A) ==> lazy
        
        let nameAdd = Gen.elements [1..(int note.name)] |> Arb.fromGen
        Prop.forAll nameAdd (fun n ->
            let noteAfterNegativeAdded = note |> Notes.add -n
            
            let nameAfterNegativeAdded = enum ((int note.name) - n)
            
            noteAfterNegativeAdded =! { note with name = nameAfterNegativeAdded })

    [<NoteProperty>]
    let ``Adding -n semitones, where n is higher than the difference between A and the note name, increase the name and the height`` (note:Note) =
        (note.height > 0) ==> lazy

        let diffToPreviousA = int note.name
        let maxSemitonesAdd = note.height * 12 + int note.name

        let semitonesAdd = Gen.elements [diffToPreviousA..maxSemitonesAdd] |> Arb.fromGen

        Prop.forAll semitonesAdd (fun n ->
            let actual = note |> Notes.add -n

            let expectedHeightRemoved =
                if n <= diffToPreviousA
                then 0
                else ((n - diffToPreviousA - 1) / 12) + 1
            actual.height =! note.height - expectedHeightRemoved

            let expectedNoteName =
                match n % 12 with
                | s when s <= diffToPreviousA -> enum (diffToPreviousA - s)
                | s -> enum (12 - (s - diffToPreviousA))
            actual.name =! expectedNoteName)