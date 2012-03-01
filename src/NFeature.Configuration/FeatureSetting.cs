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

namespace NFeature.Configuration
{
	using System;
	using System.Collections.Generic;
	using Exceptions;
	using NSure;

	public class FeatureSetting<TFeatureEnum> : FeatureSetting<TFeatureEnum, DefaultTenantEnum>
		where TFeatureEnum : struct {}

	/// <summary>
	/// 	Corresponds to persisted feature information.
	/// </summary>
	public class FeatureSetting<TFeatureEnum, TTenantEnum>
		where TFeatureEnum : struct
		where TTenantEnum : struct
	{
		private TFeatureEnum[] _dependencies;
		private DateTime _endDtg;
		private FeatureState _featureState;
		private IDictionary<string, object> _settings;
		private DateTime _startDtg;
		private TTenantEnum[] _supportedTenants;

		public TFeatureEnum Feature { get; set; }

		public FeatureState FeatureState {
			get { return _featureState; }
			set {
				if (value != FeatureState.Enabled) {
					Ensure.That<FeatureConfigurationException<TFeatureEnum>>(!IsRequiredByFeatureSubsystem,
					                                                         string.Format(
					                                                         	"Feature state must be 'Enabled' (or simply not specified) for features marked 'IsRequiredByFeatureSubsystem'. Feature: '{0}'.",
					                                                         	Feature));
				}
				_featureState = value;
			}
		}

		public TFeatureEnum[] Dependencies {
			get { return _dependencies ?? new TFeatureEnum[0]; }
			set {
				if (value.Length > 0) {
					Ensure.That<FeatureConfigurationException<TFeatureEnum>>(!IsRequiredByFeatureSubsystem,
					                                                         string.Format(
					                                                         	"Dependencies may not be specified for features marked 'IsRequiredByFeatureSubsystem'. Feature: '{0}'.",
					                                                         	Feature));
				}

				_dependencies = value;
			}
		}

		public TTenantEnum[] SupportedTenants {
			get {
				return (_supportedTenants == null || _supportedTenants.Length == 0)
				       	? new[] {(TTenantEnum) Enum.ToObject(typeof (TTenantEnum), 0)}
				       	: _supportedTenants;
			}
			set {
				if (value.Length > 0) {
					Ensure.That<FeatureConfigurationException<TFeatureEnum>>(!IsRequiredByFeatureSubsystem,
					                                                         string.Format(
					                                                         	"Supported tenants may not be specified for features marked 'IsRequiredByFeatureSubsystem'. Feature: '{0}'.",
					                                                         	Feature));
				}
				_supportedTenants = value;
			}
		}

		public IDictionary<string, dynamic> Settings {
			get { return _settings ?? new Dictionary<string, object>(); }
			set { _settings = value; }
		}

		public DateTime StartDtg {
			get { return _startDtg; }
			set {
				if (value != DateTime.MinValue) {
					Ensure.That<FeatureConfigurationException<TFeatureEnum>>(!IsRequiredByFeatureSubsystem,
					                                                         string.Format(
					                                                         	"Feature start date may not be specified for features marked 'IsRequiredByFeatureSubsystem'. Feature: '{0}'.",
					                                                         	Feature));
				}

				if (_endDtg != DateTime.MinValue) {
					Ensure.That(value >= _startDtg,
					            string.Format("Feature '{0}' start date is the same as or before end date.",
					                          Feature));
				}

				_startDtg = value;
			}
		}

		/// <summary>
		/// 	NOTE 1: BA; if a value is not supplied for this field, the default value is DateTime.Min so we set this to the more appropriate DateTime.Max here (we are dealing with the *end*-date).
		/// </summary>
		public DateTime EndDtg {
			get { return _endDtg == DateTime.MinValue ? DateTime.MaxValue : _endDtg; } //see note 1
			set {
				if (value != DateTime.MinValue) {
					Ensure.That<FeatureConfigurationException<TFeatureEnum>>(!IsRequiredByFeatureSubsystem,
					                                                         string.Format(
					                                                         	"Feature end date may not be specified for features marked 'IsRequiredByFeatureSubsystem'. Feature: '{0}'.",
					                                                         	Feature));

					if (_startDtg != DateTime.MinValue) {
						Ensure.That(value >= _startDtg,
						            string.Format("Feature '{0}' end date is the same as or before start date.",
						                          Feature));
					}
				}

				_endDtg = value;
			}
		}

		public bool IsRequiredByFeatureSubsystem { get; set; }
	}
}