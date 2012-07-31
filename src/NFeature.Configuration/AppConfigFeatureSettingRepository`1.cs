﻿// Copyright 2012, Ben Aston (ben@bj.ma).
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

using System.Linq;

namespace NFeature.Configuration
{
	public class AppConfigFeatureSettingRepository<TFeatureEnum> :
		AppConfigFeatureSettingRepository<TFeatureEnum, DefaultTenantEnum>,
		IFeatureSettingRepository<TFeatureEnum>
		where TFeatureEnum : struct
	{
		protected override System.Collections.Generic.IEnumerable<FeatureConfigurationElement<TFeatureEnum, DefaultTenantEnum>> LoadConfigElements()
		{
			var configurationManager = new ConfigurationManager<FeatureConfigurationSection<TFeatureEnum>, TFeatureEnum> {GetConfiguration = GetConfiguration};

		    return configurationManager.Section().
				FeatureSettings.Cast<FeatureConfigurationElement<TFeatureEnum, DefaultTenantEnum>>();
		}
	}

   
}