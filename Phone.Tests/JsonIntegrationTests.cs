using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Phone.App;

namespace Phone.Tests
{
    public class JsonIntegrationTests
    {
        private string _tempFilePath;

        [SetUp]
        public void Setup()
        {
            _tempFilePath = Path.GetTempFileName();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_tempFilePath);
        }

        [Test]
        public void PhoneReader_Should_Read_From_Json()
        {
            // Given
            var jsonIntegration = new JsonIntegration();
            var jsonPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"JsonFiles/phones.json");
            
            // When
            var phones = jsonIntegration.Read<IEnumerable<APhone>>(jsonPath);
            
            // Then
            var expectedPhones = new APhone[]
            {
                new MobilePhone
                {
                    Name = "iPhone",
                    Company = "Apple",
                    Price = 799m,
                    Color = "black",
                    MemorySize = 64
                },
                new RadioPhone
                {
                    Name = "Brick",
                    Company = "Huawei",
                    Price = 400m,
                    Range = 2000,
                    SupportsAutoAnswer = true
                },
                new MobilePhone
                {
                    Name = "Samsung S10",
                    Company = "Samsung",
                    Price = 700m,
                    Color = "grey",
                    MemorySize = 128
                }
            };

            phones.Should().BeEquivalentTo(expectedPhones);
        }

        [Test]
        public void PhoneWriter_Should_Write_To_Json()
        {
            // Given
            var jsonPath = _tempFilePath;
            var jsonIntegration = new JsonIntegration();
            
            var phones = new APhone[]
            {
                new MobilePhone
                {
                    Name = "mobile",
                    Company = "company",
                    Price = 32.4m,
                    Color = "green",
                    MemorySize = 32
                },
                new RadioPhone
                {
                    Name = "radio",
                    Company = "company",
                    Price = 10.5m,
                    SupportsAutoAnswer = false,
                    Range = 1488
                }
            };
            
            // When
            jsonIntegration.Write(jsonPath, phones);

            // Then
            var expectedJson = @"
                [
                    {
                        _type: ""MobilePhone"",
                        name: ""mobile"",
                        company: ""company"",
                        price: 32.4,
                        color: ""green"",
                        memorySize: 32
                    },
                    {
                        _type: ""RadioPhone"",
                        name: ""radio"",
                        company: ""company"",
                        price: 10.5,
                        range: 1488,
                        supportsAutoAnswer: false
                    }
                ]
            ";

            var actualJson = JArray.Parse(File.ReadAllText(jsonPath));
            actualJson.Should().BeEquivalentTo(expectedJson);

            // Cleanup
            File.Delete(jsonPath);
        }
    }
}