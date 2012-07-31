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

using System.Linq;

namespace NFeature.Configuration
{
	using System;
	using System.Configuration;

	public static class ConfigurationManager<T> where T : ConfigurationSectionBase, new()
	{
		public static T Section(Func<T> onMissingSection = null)
		{
			var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			// find section that matches type
			T section = configuration.Sections.Cast<ConfigurationSection>().Where(sec => sec.ElementInformation.Type == typeof(T)).Cast<T>().FirstOrDefault();

			if (section == null) {
				return new T().OnMissingConfiguration() as T;
			}

			return section;
		}
	}
}