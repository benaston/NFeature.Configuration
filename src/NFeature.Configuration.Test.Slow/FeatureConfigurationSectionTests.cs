// Copyright 2011, Ben Aston (ben@bj.ma).
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

namespace NFeature.Configuration.Test.Slow
{
	using System;
	using System.Linq;
	using NUnit.Framework;

	[TestFixture]
	[Category("Slow")]
	public class FeatureConfigurationSectionTests
	{
		private readonly DateTime testStartDtg = new DateTime(1981, 3, 23, 18, 0, 1);
		private readonly DateTime testEndDtg = new DateTime(2081, 3, 23, 18, 0, 1);

		[Test]
		public void FeatureConfigurationSection_Retrieves_ArrayJsonSetting_OK()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[4].Settings.Count, Is.GreaterThan(0));
			Assert.That((f[4].Settings["My.Type2, MyAssembly"])[0].Value == "one"); //hell yeah
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Correct_Dependencies()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[0].Dependencies.Length == 2);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Correct_EndDtg()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[3].EndDtg == testEndDtg);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Correct_EndDtg_When_None_Specified()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[2].EndDtg == DateTime.MinValue);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Correct_Number_Of_SupportedTenants()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[0].SupportedTenants.Length == 1);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Correct_StartDtg()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[2].StartDtg == testStartDtg);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Correct_SupportedTenants()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[0].SupportedTenants[0] == Tenant.TenantA);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_FeatureState_OK()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[0].State == FeatureState.Enabled);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_MultipleJsonSettings_OK()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[1].Settings.Count == 2);
			Assert.That((string) f[1].Settings["testFeatureSetting1"] == "testFeatureSetting1Value");
			Assert.That((string) f[1].Settings["testFeatureSetting2"] == "testFeatureSetting2Value");
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Multiple_Features_OK()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f.Count == 5);
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_Name_OK()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[0].Name == "TestFeatureA");
		}

		[Test]
		public void FeatureConfigurationSection_Retrieves_SingleJsonSetting_OK()
		{
			var s = ConfigurationManager<FeatureConfigurationSection<Feature, Tenant>>.Section();
			var f = s.FeatureSettings;

			Assert.That(f[0].Settings.Count, Is.GreaterThan(0));
			Assert.That(f[0].Settings.First().Key == "testFeatureSetting1");
			Assert.That((string) f[0].Settings.First().Value == "testFeatureSetting1Value");
		}
	}
}

// ReSharper restore InconsistentNaming