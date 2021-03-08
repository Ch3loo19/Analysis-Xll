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
    public partial class FxCsvImporterTests : FeatureFixture
    {
        //+ Constants
        const string DeploymentFolder = @"DeploymentItems";
        const string ValidFile = @"CAD - ESG - FX.csv";
        const string SpuriousData1 = @"FxSpurious1.csv";
        const string SpuriousData2 = @"FxSpurious2.csv";
        const string SpuriousData3 = @"FxSpurious3.csv";
        const string NonExistentFile = @"DesuDesu.csv";
        const string WrongExtensionFile = @"AUD - ESG - FX.xlsb";

        //+ Fileds
        string _relativePath;
        string _hash;
        Cache<FxArray> _cache;

        //+ Steps

        private void a_valid_csv_path_with_correct_data()
        {
            _relativePath = Path.Join(DeploymentFolder, ValidFile);
            File.Exists(_relativePath).ShouldBeTrue();
        }

        private void a_valid_csv_path_with_spurious_data(string file)
        {
            _relativePath = Path.Join(DeploymentFolder, file);
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
            Should.Throw<ArgumentException>(() => CsvImporter.ReadFxArray(_relativePath, string.Empty, string.Empty)).Message.ShouldBe("Non-existent file");
        }

        private void I_cannot_import_a_file_with_wrong_extension()
        {
            Should.Throw<ArgumentException>(() => CsvImporter.ReadFxArray(_relativePath, string.Empty, string.Empty)).Message.ShouldBe("Invalid extension");
        }

        private void I_cannot_import_a_file_with_spurious_data()
        {
            Should.Throw<ArgumentException>(() => CsvImporter.ReadFxArray(_relativePath, string.Empty, string.Empty));
        }

        private void I_can_import_a_valid_file(string filePath, object maxHor, object maxSim)
        {
            _hash = CsvImporter.ReadFxArray(filePath, maxHor, maxSim);
            _hash.ShouldNotBeNull();
        }

        private void I_can_get_the_fx_array_from_cache(int expectedHorizon, int expectedSim)
        {
            var result = _cache.TryGetItem(_hash, out var fxArray);
            result.ShouldBeTrue("The FxArray was not found in the cache");
            fxArray.ShouldNotBeNull();
            fxArray.MaxHorizon.ShouldBe(expectedHorizon);
            fxArray.MaxSim.ShouldBe(expectedSim);
        }

        private void I_cannot_import_a_file_when_passing_invalid_parameters(string filePath, object maxHor, object maxSim, Type exceptionType)
        {
            Should.Throw(() => CsvImporter.ReadFxArray(filePath, maxHor, maxSim), exceptionType);
        }

        //+ Test Data

        private static IEnumerable<object> GetSpuriousCsvImportData()
        {
            yield return new object[] { SpuriousData1 };
            yield return new object[] { SpuriousData2 };
            yield return new object[] { SpuriousData3 };
        }


        private static IEnumerable<object> GetValidCsvImportData()
        {
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), string.Empty, string.Empty, 4, 10000 };
            yield return new object[] { string.Concat(@"""", Path.Combine(DeploymentFolder, ValidFile), @""""), string.Empty, string.Empty, 4, 10000 };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), 10, 20000, 4, 10000 };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), 3, string.Empty, 3, 10000 };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), string.Empty, 5000, 4, 5000 };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), 3, 5000, 3, 5000 };
        }

        private static IEnumerable<object> GetInvalidCsvImportData()
        {
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), -1, string.Empty, typeof(ArgumentException) };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), string.Empty, -1, typeof(ArgumentException) };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), 2.3, string.Empty, typeof(GenericArgumentParseException) };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), string.Empty, 3.5, typeof(GenericArgumentParseException) };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), "desu", string.Empty, typeof(GenericArgumentParseException) };
            yield return new object[] { Path.Combine(DeploymentFolder, ValidFile), string.Empty, "desu", typeof(GenericArgumentParseException) };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _cache = Cache<FxArray>.GetCache;
        }

        [TearDown]
        public void TestCompletion()
        {
            _cache.ResetCache();
        }

    }
}