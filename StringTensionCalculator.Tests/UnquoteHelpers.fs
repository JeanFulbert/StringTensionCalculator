namespace StringTensionsCalculator.Tests

module UnquoteHelpers =
    let inline equalsWithinTolerance x tol y = abs(x-y) < tol

    let inline (+-=!) x = equalsWithinTolerance x 0.00000001