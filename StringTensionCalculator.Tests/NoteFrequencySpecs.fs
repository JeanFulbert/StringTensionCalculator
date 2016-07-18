namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open StringTensionsCalculator.Tests.UnquoteHelpers
open FsCheck
open FsCheck.NUnit

module FrequencySpecs =
    [<FloatWithoutNaNProperty>]
    let ``Frequency of A4 returns passed frequency`` (f:float<hz>) =
        let a4 = { name = NoteName.A; height = 4 }
        let actual = a4 |> Notes.frequency f
        f =! actual

    [<Property>]
    let ``Higher octaves are multiples of 2`` (noteName:NoteName) =
        let noteAtHeight4 = { name = noteName; height = 4 }
        let freqAtHeight4 = noteAtHeight4 |> Notes.frequency440

        let octaveHeight = Gen.elements [5..8] |> Arb.fromGen
        Prop.forAll octaveHeight (fun h ->
            let sut = { name = noteName; height = h }            
            let actual = sut |> Notes.frequency440

            let multiple = actual / freqAtHeight4
            multiple % 2. +-=! 0.)

    [<Property>]
    let ``Lower octaves are multiples of 1/2`` (noteName:NoteName) =
        let noteAtHeight4 = { name = noteName; height = 4 }
        let freqAtHeight4 = noteAtHeight4 |> Notes.frequency440

        let octaveHeight = Gen.elements [0..3] |> Arb.fromGen
        Prop.forAll octaveHeight (fun h ->
            let sut = { name = noteName; height = h }            
            let actual = sut |> Notes.frequency440

            let multiple = actual / freqAtHeight4
            (1.0 / multiple) % 2. +-=! 0.)