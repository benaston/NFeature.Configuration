namespace NFeature.Configuration.Exceptions
{
    using System;
    using NHelpfulException;

    public class FeatureConfigurationException<TFeatureEnum> : HelpfulException
        where TFeatureEnum : struct
    {
        public FeatureConfigurationException(TFeatureEnum feature, string message = "",
                                             string[] resolutionSuggestions = null, Exception innerException = null)
            : base(
                string.Format("{0}. Affected feature: \"{1}\".", message,
                              Enum.GetName(typeof (TFeatureEnum), feature)), resolutionSuggestions, innerException) {}
    }
}