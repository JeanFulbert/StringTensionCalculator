namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

module NoteAddSpecs =
    [<NoteProperty>]
    let ``Adding 0 semitones returns the same note`` (note:Note) =
        let actual = note |> Notes.add 0
        note =! actual

    [<NoteProperty>]
    let ``Adding n semitones, where n is a multiple of 12, increase the height by modulo`` (note:Note) =
        (note.height < 8) ==> lazy

        let octaveHeight = Gen.elements [1..(8 - note.height)] |> Arb.fromGen
        Prop.forAll octaveHeight (fun h ->
            let semitonesToAdd = h * 12
            let noteAfterAdded = note |> Notes.add semitonesToAdd

            noteAfterAdded.name =! note.name
            noteAfterAdded.height =! note.height + h)

    [<NoteProperty>]
    let ``Adding n semitones, where n is less than the name difference, increase the name but not the height`` (note:Note) =
        (note.name < NoteName.GSharp) ==> lazy
        
        let nameAdd = Gen.elements [1..(11 - int note.name)] |> Arb.fromGen
        Prop.forAll nameAdd (fun n ->
            let noteAfterAdded = note |> Notes.add n

            noteAfterAdded.height =! note.height
            let nameAfterAdded = enum ((int note.name) + n)
            noteAfterAdded.name =! nameAfterAdded)

    [<NoteProperty>]
    let ``Adding n semitones, where n is higher than the difference between G# and the note name, increase the name and the height`` (note:Note) =
        (note.height < 8) ==> lazy

        let diffToNextA = 12 - int note.name
        let maxSemitonesAdd =
            ((8 - note.height) * 12) +
            (int NoteName.GSharp - int note.name)

        let semitonesAdd = Gen.elements [diffToNextA..maxSemitonesAdd] |> Arb.fromGen

        Prop.forAll semitonesAdd (fun s ->
            let actual = note |> Notes.add s

            let expectedHeightAdd = ((s - diffToNextA) / 12) + 1
            actual.height =! note.height + expectedHeightAdd

            let expectedNoteName =
                match s % 12 with
                | s when s >= diffToNextA -> enum (s - diffToNextA)
                | x -> enum (int note.name  + x)
            actual.name =! expectedNoteName)