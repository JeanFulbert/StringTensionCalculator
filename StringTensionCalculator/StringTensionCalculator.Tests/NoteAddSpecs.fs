namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

module NoteAddSpecs =
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
