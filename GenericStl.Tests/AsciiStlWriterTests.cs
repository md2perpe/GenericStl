﻿using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using GenericStl.Tests.TestDataStructures;
using Xunit;

namespace GenericStl.Tests
{
    
    [UseReporter(typeof (NUnitReporter))]
    public class AsciiStlWriterTests : StlWriterBaseTests<AsciiStlWriter<Triangle, Vertex, Normal>>
    {
        public AsciiStlWriterTests()
        {
            _objectUnderTest = new AsciiStlWriter<Triangle, Vertex, Normal>(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal);
        }

        private readonly AsciiStlWriter<Triangle, Vertex, Normal> _objectUnderTest;

        protected override AsciiStlWriter<Triangle, Vertex, Normal> CreateWriter(Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle, Func<Vertex, Tuple<float, float, float>> extractVertex, Func<Normal, Tuple<float, float, float>> extractNormal)
        {
            return new AsciiStlWriter<Triangle, Vertex, Normal>(extractTriangle, extractVertex, extractNormal);
        }

        protected override AsciiStlWriter<Triangle, Vertex, Normal> CreateWriter(IDataStructureExtractor<Triangle, Vertex, Normal> extractor)
        {
            return new AsciiStlWriter<Triangle, Vertex, Normal>(extractor);
        }

        [Fact]
        public void WriteFile_WithBlock_ReturnsExpectedResult()
        {
            var namer = Approvals.GetDefaultNamer();
            var file = Path.Combine(namer.SourcePath, namer.Name + ".received.stl");
            _objectUnderTest.WriteToFile(file, TestHelpers.BlockExpectedResult);
            Approvals.Verify(new FileInfo(file));
        }

        [Fact]
        public void Write_WithBlock_ReturnsExpectedResult()
        {
            var result = _objectUnderTest.Write(TestHelpers.BlockExpectedResult);
            Approvals.Verify(new ApprovalTextWriter(result, ".stl"));
        }
    }
}