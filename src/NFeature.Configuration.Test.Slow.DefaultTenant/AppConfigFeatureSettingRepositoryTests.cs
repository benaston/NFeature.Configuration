// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NFeature.
// 
// NFeature is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NFeature is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NFeature.  If not, see <http://www.gnu.org/licenses/>.

namespace NFeature.Configuration.Test.Slow.DefaultTenant
{
	using System.Linq;
	using NUnit.Framework;

	[TestFixture]
	[Category("Slow")]
	public class AppConfigFeatureSettingRepositoryTests
	{
		[Test]
		public void GetFeatureSettings_WhenInvokedWithCustomFullName_ReturnsAllCorrectFeatureSetting() {
			var r = new AppConfigFeatureSettingRepository<Feature>();
			var settings = r.GetFeatureSettings();

			Assert.That(settings.Count() == 5);
		}

		[Test]
		public void
			GetFeatureSettings_WhenInvoked_FeatureSettingsAreMarkedAsBeingEstablishedCorrectly() {
			var r = new AppConfigFeatureSettingRepository<Feature>();
			var settings = r.GetFeatureSettings();

			Assert.That(
				settings.Where(i => i.Feature == Feature.TestFeatureE).First().FeatureState ==
				FeatureState.Established);
		}

		[Test]
		public void
			GetFeatureSettings_WhenInvoked_FeatureSettingsWithNoSpecifiedTenantAreAvailableToAllTenant() {
			var r = new AppConfigFeatureSettingRepository<Feature>();
			var settings = r.GetFeatureSettings();

			Assert.That(
				settings.Where(i => i.Feature == Feature.TestFeatureD).First().SupportedTenants.Contains(
					DefaultTenantEnum.All));
		}

		[Test]
		public void
			GetFeatureSettings_WhenInvoked_FeatureSettingsWithSpecifiedTenantsAreAvailableToThoseTenantsOnly
			() {
			var r = new AppConfigFeatureSettingRepository<Feature>();
			var settings = r.GetFeatureSettings();

			Assert.That(
				settings.Where(i => i.Feature == Feature.TestFeatureA).First().SupportedTenants.Length == 1);
			Assert.That(
				settings.Where(i => i.Feature == Feature.TestFeatureA).First().SupportedTenants.Contains(
					DefaultTenantEnum.All));
		}

		[Test]
		public void GetFeatureSettings_WhenInvoked_ReturnsAllCorrectNumberOfFeatureSettings() {
			var r = new AppConfigFeatureSettingRepository<Feature>();
			var settings = r.GetFeatureSettings();

			Assert.That(settings.Count() == 5);
		}

		[Test]
		public void GetFeatureSettings_WhenInvoked_ReturnsAllCorrectNumberOfFeatureSettings_For_Feature2() {
			var r = new AppConfigFeatureSettingRepository<Feature2>();
			var settings = r.GetFeatureSettings();

			Assert.That(settings.Count() == 1);
		}
	}
}

// ReSharper restore InconsistentNaming