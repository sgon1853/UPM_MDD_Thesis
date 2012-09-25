// 3.4.4.5
using System;

namespace SIGEM.Business
{
	/// <summary>
	/// ONErrorText
	/// </summary>
	public abstract class ONErrorText
	{
		#region Model
		#endregion

		#region Others
		public const string XMLFormat = "Format not allowed for type ${type}";

		// XML
		public const string XMLStructure = "Unexpected XML tag. Expected ${tag}";
		public const string XMLError = "XML Error: ${xml}";
		public const string XMLNavFilter = "Unexpected XML tag ${actualxmltag}. Expected ${expectedxmltags}";

		// OID
		public const string OIDProcessWrongClass = "Expected OID of ${class}, instead of ${wrongclass}";
		public const string OIDProcessWrongField = "Invalid OID field ${field}, in class ${class}";

		// Arguments
		public const string MissingArgument = "Missing argument ${argument}";
		public const string NotNullArgument = "Argument ${argument} can't be null in ${service} of ${class}.";
		public const string MaxLenghtArgument = "Argument ${argument} exceeds the maximum length ${length}";
		public const string ServiceArgument = "Error proccesing argument ${argument}";
		
		// STD
		public const string STDError = "Unable to execute ${service} in the current state of ${class}";

		// Cardinality
		public const string InstanceExists = "Instance of ${class} already exists";
		public const string InstanceNotExists = "Instance of ${class} does not exist";
		public const string RelatedExists = "Related instance ${role} already exists";
		public const string RelatedNotExists = "Related instance ${role} does not exist";
		public const string MinCardinality = "At least ${cardinality} instance(s) of ${role} need to be related with ${class}";
		public const string MaxCardinality = "Maximum cardinality for ${role} is not satisfied. At most ${cardinality} instances of ${role} are allowed";
		public const string Static = "${class} can't be deleted because it's related with ${role}";
		public const string StaticCreation = "${class} can't be created because it cannot be related with ${role}";

		// Services
		public const string ServiceArguments = "Error proccesing service arguments of ${service} of ${class}";
		
		// Check State
		public const string ChangeDetectionFailure = "State change detection error";

		// Precondition
		public const string PreconditionFailure = "${precondition}";

		// Alternate Key
		public const string AlternateKeyFailure = "${alternateKey}";
		public const string AlternateKeyNameFailure = "Alternate key ${alternateKey} does not exist for the ${class} class";
		public const string AlternateKeyFieldFailure = "Invalid Alternate Key field ${field}, in alternate key ${alternateKey} of class ${class}";

		// Constraint
		public const string StaticConstraintFailure = "${constraint}";
		
		// Null
		public const string NullNotAllowed = "Null not allowed";

		// Agent Validation
		public const string AgentValidationFailure = "Invalid authentication";
		public const string AccessAgentValidationFailure = "You don't have sufficient rights to access this information";

        // Legacy System
		public const string ErrorWithLegacySystem = "Error ocurred while attempting to comunicate with legacy system";
        #endregion
	}
}


