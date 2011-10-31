﻿// ReSharper disable InconsistentNaming
namespace NFeature.Configuration.Test.Slow
{
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    [Category("Slow")]
    public class AppConfigFeatureSettingRepositoryTests
    {
        [Test]
        public void GetFeatureSettings_WhenInvokedWithCustomFullName_ReturnsAllCorrectFeatureSetting()
        {
            var r = new AppConfigFeatureSettingRepository<Feature, Tenant>();
            var settings = r.GetFeatureSettings();

            Assert.That(settings.Count() == 5);
        }

        [Test]
        public void GetFeatureSettings_WhenInvoked_FeatureSettingsAreMarkedAsBeingEstablishedCorrectly()
        {
            var r = new AppConfigFeatureSettingRepository<Feature, Tenant>();
            var settings = r.GetFeatureSettings();

            Assert.That(
                settings.Where(i => i.Feature == Feature.TestFeatureE).First().FeatureState ==
                FeatureState.Established);
        }

        [Test]
        public void GetFeatureSettings_WhenInvoked_FeatureSettingsWithNoSpecifiedTenantAreAvailableToAllTenant()
        {
            var r = new AppConfigFeatureSettingRepository<Feature, Tenant>();
            var settings = r.GetFeatureSettings();

            Assert.That(
                settings.Where(i => i.Feature == Feature.TestFeatureD).First().SupportedTenants.Contains(
                    Tenant.All));
        }

        [Test]
        public void GetFeatureSettings_WhenInvoked_FeatureSettingsWithSpecifiedTenantsAreAvailableToThoseTenantsOnly()
        {
            var r = new AppConfigFeatureSettingRepository<Feature, Tenant>();
            var settings = r.GetFeatureSettings();

            Assert.That(
                settings.Where(i => i.Feature == Feature.TestFeatureA).First().SupportedTenants.Length == 1);
            Assert.That(
                settings.Where(i => i.Feature == Feature.TestFeatureA).First().SupportedTenants.Contains(
                    Tenant.TenantA));
        }

        [Test]
        public void GetFeatureSettings_WhenInvoked_ReturnsAllCorrectNumberOfFeatureSettings()
        {
            var r = new AppConfigFeatureSettingRepository<Feature, Tenant>();
            var settings = r.GetFeatureSettings();

            Assert.That(settings.Count() == 5);
        }
    }
}

// ReSharper restore InconsistentNaming