using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Global;
using static Global.EasyObject;

// ReSharper disable once CheckNamespace
namespace Test;
public class Add2Test {
    [SetUp]
    public void Setup() {
        Echo($"{FullName(this)}#Setup() called");
    }
    [Test]
    public void Test01() {
        AssertIdentical(333, UniversalTransformer.Add2(111, 222));
    }
}