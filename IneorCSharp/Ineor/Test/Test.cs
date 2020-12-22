using Ineor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ineor.Test
{
    [TestClass]
    public class Test
    {
        private IJsonDeterminator controller;
        public Test()
        {
            controller = new JsonDeterminator();
        }

        [TestMethod]
        public void ShouldReturnNullOnWrongCountryCode()
        {
            // Arrange
            var urlString = new
            {
                last_updated = "2016-01-01T22:34Z",
                disclaimer = "This data is compiled from official European Commission sources to be as accurate as possible, however no guarantee of accuracy is provided. Use at your own risk. Don't trust random people on the internet without doing your own research.",
                rates = new
                {
                    AT = new
                    {
                        country = "Austria",
                        standard_rate = 20,
                        reduced_rate = 10,
                        reduced_rate_alt = 13,
                        super_reduced_rate = false,
                        parking_rate = 12
                    }
                }
            };

            // Act
            var country = controller.GetCountry(urlString.ToString(), "SI");

            // Assert
            Assert.AreEqual(null, country);
        }

        [TestMethod]
        public void ShouldReturnValidCountry()
        {
            // Arrange
            var country = new
            {
                country = "Austria",
                standard_rate = 20,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctCountry = new Country()
            {
                country = "Austria",
                standard_rate = 20,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var urlString = new
            {
                last_updated = "2016-01-01T22:34Z",
                disclaimer = "This data is compiled from official European Commission sources to be as accurate as possible, however no guarantee of accuracy is provided. Use at your own risk. Don't trust random people on the internet without doing your own research.",
                rates = new
                {
                    AT = country
                }
            };

            // Act
            var resultCountry = controller.GetCountry(urlString.ToString(), "AT");

            // Assert
            Assert.AreEqual(correctCountry, resultCountry);
        }

        [TestMethod]
        public void ShouldReturnThreeHighestVatCountries()
        {
            // Arrange
            var country1 = new
            {
                country = "Austria",
                standard_rate = 12,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var country2 = new
            {
                country = "Slovenia",
                standard_rate = 14,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var country3 = new
            {
                country = "Croatia",
                standard_rate = 29,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var country4 = new
            {
                country = "Germany",
                standard_rate = 11,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry1 = new Country()
            {
                country = "Austria",
                standard_rate = 12,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry2 = new Country()
            {
                country = "Slovenia",
                standard_rate = 14,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry3 = new Country()
            {
                country = "Croatia",
                standard_rate = 29,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry4 = new Country()
            {
                country = "Germany",
                standard_rate = 11,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var urlString = new
            {
                last_updated = "2016-01-01T22:34Z",
                disclaimer = "This data is compiled from official European Commission sources to be as accurate as possible, however no guarantee of accuracy is provided. Use at your own risk. Don't trust random people on the internet without doing your own research.",
                rates = new
                {
                    AT = country1,
                    SL = country2,
                    CR = country2,
                    DE = country2,
                }
            };
            var expectedCountries = new List<Country>()
        {
            correctcountry1, correctcountry2, correctcountry3
        };

            // Act
            var resultCountries = controller.GetVatArray(urlString.ToString(), "highest");

            // Assert
            Assert.AreEqual(expectedCountries, resultCountries);
        }

        [TestMethod]
        public void ShouldReturnThreeLowestVatCountries()
        {
            // Arrange
            var country1 = new
            {
                country = "Austria",
                standard_rate = 12,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var country2 = new
            {
                country = "Slovenia",
                standard_rate = 14,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var country3 = new
            {
                country = "Croatia",
                standard_rate = 29,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var country4 = new
            {
                country = "Germany",
                standard_rate = 11,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry1 = new Country()
            {
                country = "Austria",
                standard_rate = 12,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry2 = new Country()
            {
                country = "Slovenia",
                standard_rate = 14,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry3 = new Country()
            {
                country = "Croatia",
                standard_rate = 29,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var correctcountry4 = new Country()
            {
                country = "Germany",
                standard_rate = 11,
                reduced_rate = 10,
                reduced_rate_alt = 13,
                super_reduced_rate = false,
                parking_rate = 12
            };
            var urlString = new
            {
                last_updated = "2016-01-01T22:34Z",
                disclaimer = "This data is compiled from official European Commission sources to be as accurate as possible, however no guarantee of accuracy is provided. Use at your own risk. Don't trust random people on the internet without doing your own research.",
                rates = new
                {
                    AT = country1,
                    SL = country2,
                    CR = country2,
                    DE = country2,
                }
            };
            var expectedCountries = new List<Country>()
            {
                correctcountry4, correctcountry1, correctcountry2
            };

            // Act
            var resultCountries = controller.GetVatArray(urlString.ToString(), "highest");

            // Assert
            Assert.AreEqual(expectedCountries, resultCountries);
        }
    }
}

