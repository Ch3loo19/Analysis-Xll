using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System;

namespace Functions_Logic.Tests.Importers.Tests
{
    [Label("CsvImporter tests")]
    [FeatureDescription(@"In order to work with ESGs I need to import them from the drive ")]
    public partial class EsgCsvImporterTests
    {
        [Label("SCENARIO-1: Import a missing file")]
        [Scenario]
        public void Import_missing_file_test()
        {
            Runner.RunScenario(
                Given => a_non_existent_csv_path(),
                Then => I_cannot_import_a_non_existent_esg());
        }

        [Label("SCENARIO-2: Import a file with the wrong extension")]
        [Scenario]
        public void Import_wrong_file_test()
        {
            Runner.RunScenario(
                Given => a_wrong_extension_csv_path(),
                Then => I_cannot_import_a_file_with_wrong_extension());
        }

        [Label("SCENARIO-3: Import a file with spurious data")]
        [Scenario]
        public void Import_spurious_data_test()
        {
            Runner.RunScenario(
                Given => a_valid_csv_path_with_spurious_data(),
                Then => I_cannot_import_a_file_with_spurious_data());
        }
             
        [Label("SCENARIO-4: Import a valid file succeeds")]
        [Scenario]
        [TestCaseSource(nameof(GetValidCsvImportData))]
        public void Import_valid_file_test(string filePath, object maxHor, object maxSim, int horExpectd, int simExpected)
        {
            Runner.RunScenario(
                Given => I_can_import_a_valid_file(filePath, maxHor, maxSim),
                Then => I_can_get_the_esg_from_cache(horExpectd, simExpected));
        }

        [Label("SCENARIO-5: Import a valid file with invalid read-in parameters")]
        [Scenario]
        [TestCaseSource(nameof(GetInvalidCsvImportData))]
        public void Import_valid_file_with_invalid_parameters_test(string filePath, object maxHor, object maxSim, Type exceptionType)
        {
            Runner.RunScenario(
             _ => I_cannot_import_a_file_when_passing_invalid_parameters(filePath, maxHor,maxSim, exceptionType));
        }



    }
}