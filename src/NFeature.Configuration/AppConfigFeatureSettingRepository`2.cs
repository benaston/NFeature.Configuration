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

using System.Collections.Generic;
using System.Configuration;

namespace NFeature.Configuration
{
	using System;
	using System.Linq;

	/// <summary>
	/// 	Responsible for retrieving FeatureSettings from a web.config/app.config file.
	/// </summary>
	public class AppConfigFeatureSettingRepository<TFeatureEnum, TTenantEnum> :
		IFeatureSettingRepository<TFeatureEnum, TTenantEnum>
		where TFeatureEnum : struct
		where TTenantEnum : struct
	{

		public Func<System.Configuration.Configuration> GetConfiguration = () => ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

		public FeatureSetting<TFeatureEnum, TTenantEnum>[] GetFeatureSettings() {

			IEnumerable<FeatureConfigurationElement<TFeatureEnum, TTenantEnum>> configElements;

			configElements = LoadConfigElements();
				

			return
				configElements.Select(
					fcse =>
					new FeatureSetting<TFeatureEnum, TTenantEnum> {
						IsRequiredByFeatureSubsystem = fcse.IsRequiredByFeatureSubsystem,
						//this needs to be set first because it affects validation
						Dependencies = fcse.Dependencies,
						Feature = (TFeatureEnum) Enum.Parse(typeof (TFeatureEnum), fcse.Name),
						FeatureState = fcse.State,
						SupportedTenants = fcse.SupportedTenants,
						Settings = fcse.Settings,
						StartDtg = fcse.StartDtg,
						EndDtg = fcse.EndDtg,
					}).ToArray();
		}

		protected virtual IEnumerable<FeatureConfigurationElement<TFeatureEnum, TTenantEnum>> LoadConfigElements()
		{
			IEnumerable<FeatureConfigurationElement<TFeatureEnum, TTenantEnum>> configElements;
			var configurationManager = new ConfigurationManager<FeatureConfigurationSection<TFeatureEnum, TTenantEnum>, TFeatureEnum, TTenantEnum> {GetConfiguration = GetConfiguration};
			configElements = configurationManager.Section().
				FeatureSettings.Cast
				<FeatureConfigurationElement<TFeatureEnum, TTenantEnum>>();
			return configElements;
		}
	}
}