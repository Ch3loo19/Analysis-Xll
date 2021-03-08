using Functions_Logic.ESG_Classes;
using Functions_Logic.Importers;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;

namespace Functions_Logic.Tests.Importers.Tests
{
    public partial class EsgCsvImporterTests : FeatureFixture
    {
        //+ Constants
        const string DeploymentFolder = @"DeploymentItems";
        const string EquityFile = @"GBP - EqtyGBP - TR.csv";
        const string SpuriousData = @"SpuriousData.csv";
        const string NonExistentFile = @"DesuDesu.csv";
        const string WrongExtensionFile = @"GBP - CorpGBPAA2-3 - TR.xlsx";

        //+ Fileds
        string _relativePath;
        string _hash;
        Cache<StochArray> _cache;

        //+ Steps

        private void a_valid_csv_path_with_correct_data()
        {
            _relativePath = Path.Join(DeploymentFolder, EquityFile);
            File.Exists(_relativePath).ShouldBeTrue();
        }

        private void a_valid_csv_path_with_spurious_data()
        {
            _relativePath = Path.Join(DeploymentFolder, SpuriousData);
            File.Exists(_relativePath).ShouldBeTrue();
        }

        private void a_non_existent_csv_path()
        {
            _relativePath = Path.Join(DeploymentFolder, NonExistentFile);
            File.Exists(_relativePath).ShouldBeFalse();
        }

        private void a_wrong_extension_csv_path()
        {
            _relativePath = Path.Join(DeploymentFolder, WrongExtensionFile);
            File.Exists(_relativePath).ShouldBeTrue();
        }

        private void I_cannot_import_a_non_existent_esg()
        {
            Should.Throw<ArgumentException>(() => CsvImporter.ReadStochArray(_relativePath, string.Empty, string.Empty)).Message.ShouldBe("Non-existent file");         
        }

        private void I_cannot_import_a_file_with_wrong_extension()
        {
            Should.Throw<ArgumentException>(() => CsvImporter.ReadStochArray(_relativePath, string.Empty, string.Empty)).Message.ShouldBe("Invalid extension");
        }

        private void I_cannot_import_a_file_with_spurious_data()
        {
            Should.Throw<GenericArgumentParseException>(() => CsvImporter.ReadStochArray(_relativePath, string.Empty, string.Empty));          
        }

        private void I_can_import_a_valid_file(string filePath, object maxHor, object maxSim)
        {
            _hash = CsvImporter.ReadStochArray(filePath, maxHor, maxSim);
            _hash.ShouldNotBeNull();
        }

        private void I_can_get_the_esg_from_cache(int expectedHorizon, int expectedSim)
        {
            var result = _cache.TryGetItem(_hash, out var stochArray);
            result.ShouldBeTrue("The StochArray was not found in the cache");
            stochArray.ShouldNotBeNull();
            stochArray.MaxHorizon.ShouldBe(expectedHorizon);
            stochArray.MaxSim.ShouldBe(expectedSim);
        }

        private void I_cannot_import_a_file_when_passing_invalid_parameters(string filePath, object maxHor, object maxSim, Type exceptionType)
        {
            Should.Throw(() => CsvImporter.ReadStochArray(filePath, maxHor, maxSim),exceptionType);
        }

        //+ Test Data

        private static IEnumerable<object> GetValidCsvImportData()
        {
            yield return new object[] {Path.Combine(DeploymentFolder, EquityFile), string.Empty, string.Empty, 5, 10000 };
            yield return new object[] {string.Concat(@"""", Path.Combine(DeploymentFolder, EquityFile), @""""), string.Empty, string.Empty, 5, 10000 };
            yield return new object[] {Path.Combine(DeploymentFolder, EquityFile), 10, 20000, 5, 10000 };
            yield return new object[] {Path.Combine(DeploymentFolder, EquityFile), 3, string.Empty, 3, 10000 };
            yield return new object[] {Path.Combine(DeploymentFolder, EquityFile), string.Empty, 5000, 5, 5000};
            yield return new object[] {Path.Combine(DeploymentFolder, EquityFile), 3, 5000, 3, 5000 };
        }

        private static IEnumerable<object> GetInvalidCsvImportData()
        {
            yield return new object[] { Path.Combine(DeploymentFolder, EquityFile), -1, string.Empty, typeof(ArgumentException) };
            yield return new object[] { Path.Combine(DeploymentFolder, EquityFile), string.Empty, -1, typeof(ArgumentException) };
            yield return new object[] { Path.Combine(DeploymentFolder, EquityFile), 2.3, string.Empty, typeof(GenericArgumentParseException) };
            yield return new object[] { Path.Combine(DeploymentFolder, EquityFile), string.Empty, 3.5, typeof(GenericArgumentParseException) };
            yield return new object[] { Path.Combine(DeploymentFolder, EquityFile), "desu", string.Empty, typeof(GenericArgumentParseException) };
            yield return new object[] { Path.Combine(DeploymentFolder, EquityFile), string.Empty, "desu", typeof(GenericArgumentParseException) };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _cache = Cache<StochArray>.GetCache;
        }

        [TearDown]
        public void TestCompletion()
        {
            _cache.ResetCache();
        }

    }
}