// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace SIGEM.Client
{
	/// <summary>
	/// Class that manages the internationalization constant values (multilaguage feature).
	/// </summary>
	public static class LanguageConstantValues
	{
		#region Login scenario
		public static string L_LOGIN = "Login";
		public static string L_LOGIN_MSG = "Please, authenticate yourself to connect to the application";
		public static string L_LOGIN_PASSWORD = "Password";
		public static string L_LOGIN_ERROR = "Unable to authenticate user";
		public static string L_LOGIN_NOT_NULL = "Login argument cannot be null";
		public static string L_LOGIN_INCORRECT = "Login Incorrect";
		public static string L_LOGIN_LANGUAGE = "Language";
		public static string L_LOGIN_PROFILE = "Profile";
		#endregion Login scenario

		#region Change password scenario
		public static string L_PASSWORD = "Password";
		public static string L_PASSWORD_NOT_NULL = "Password cannot be null";
		public static string L_PASSWORD_CAPTION = "Change Password";
		public static string L_PASSWORD_CURRENT = "Old Password";
		public static string L_PASSWORD_NEW = "New Password";
		public static string L_PASSWORD_RETYPE = "Retype new password";
		public static string L_PASSWORD_CURRENT_NOT_NULL = "Current password cannot be null";
		public static string L_PASSWORD_NEW_NOT_NULL = "New password cannot be null";
		public static string L_PASSWORD_RETYPE_NEW_NOT_NULL = "Retyped password cannot be null";
		public static string L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL = "New password and retyped password must be equal";
		public static string L_PASSWORD_INCORRECT = "Current password incorrect";
		public static string L_PASSWORD_UPDATED = "Password updated";
		public static string L_PASSWORD_UPDATE_FAILED = "Password update failed";
		#endregion Change password scenario

		#region MainScenario scenario
		public static string L_MAIN_CHANGE_LANGUAGE = "Change Language";
		public static string L_MAIN_CLOSE_ALL_WINDOWS = "All windows will be closed.\r\nDo you want to continue discarding pending subjects?";
		public static string L_MAIN_CLOSE = "You still have opened windows.\r\nDo you want to close the application discarding pending subjects?";
		public static string L_MAIN_MENU_FILE = "File";
		public static string L_MAIN_MENU_WINDOW = "Window";
		public static string L_MAIN_MENU_HELP = "Help";
		public static string L_MAIN_MENU_FILE_EXIT = "Exit";
		public static string L_MAIN_MENU_HELP_ABOUT = "About ...";
		public static string L_MAIN_MENU_WINDOW_MAXIMIZE = "Maximize";
		public static string L_MAIN_MENU_WINDOW_MINIMIZE = "Minimize";
		public static string L_MAIN_MENU_WINDOW_CASCADE = "Cascade";
		public static string L_MAIN_MENU_WINDOW_HORIZONTAL = "Tile Horizontal";
		public static string L_MAIN_MENU_WINDOW_VERTICAL = "Tile Vertical";
		public static string L_MAIN_MENU_WINDOW_CLOSEALL = "Close All";
		public static string L_MAIN_LOGOUT = "Logout";
		#endregion MainScenario scenario

		#region Contextual Menu
		public static string L_POP_UP_MENU_HELP = "Help";
		public static string L_POP_UP_MENU_OPTIONS = "Options";
		public static string L_POP_UP_MENU_REFRESH = "Refresh";
		public static string L_POP_UP_MENU_RETRIEVE_ALL = "Retrieve All";
		public static string L_POP_UP_MENU_NAVIGATIONS = "Navigations";
		public static string L_POP_UP_MENU_EXPORT_TO_WORD = "Export To Word";
		public static string L_POP_UP_MENU_EXPORT_TO_EXCEL = "Export To Excel";
		public static string L_POP_UP_MENU_PREFERENCES = "Preferences";
		public static string L_POP_UP_MENU_SAVEPOSITION = "Save position";
		public static string L_POP_UP_MENU_SAVECOLUMNSWIDTH = "Save column width";
		#endregion Contextual Menu

		public static string L_CONFIRM = "Confirm";

		#region About scenario
		public static string L_ABOUT_VERSION = "Version";
		public static string L_ABOUT_ENGINEERED = "Engineered";
		public static string L_ABOUT_COPYRIGHT = "Copyright";
		public static string L_ABOUT_FORM_TITLE = "About Form Title";
		#endregion About scenario

		#region Bussines Logic Errors
		public static string L_ERROR_BL_CONNECTION = "An error was ocurred when conecting to a server";
		public static string L_ERROR_BL_INTERNALSERVER = "Internal server error. Contact with your System Administrator.";
		public static string L_ERROR_BL_CHANGE_LANGUAGE = "This language is not available. Please, contact your Administrator";
		#endregion Bussines Logic Errors

		#region Generic Errors
		public static string L_ERROR = "Error";
		public static string L_ERROR_ARG_NO_MATCH = "Argument's domain class does not match the expected one.";
		public static string L_ERROR_ARG_NO_VALUE = "The argument {0} must have a value.";
		public static string L_ERROR_ARG_NO_NATURAL = "The argument {0} must be a natural number.";
		public static string L_ERROR_BAD_IDENTITY = "Invalid authentication.";
		public static string L_ERROR_DIN_REL_EXIST = "Related instance {0} already exists.";
		public static string L_ERROR_DIN_REL_NO_EXIST = "Related instance {0} does not exist.";
		public static string L_ERROR_EXIST_INSTANCE = "Instance of {0} already exists.";
		public static string L_ERROR_EXPECT_SPE_ARG = "Unable to create {0} due to a lack of arguments.";
		public static string L_ERROR_HIGH_CARD = "Maximum cardinality for {1} is not satisfied. At most {0} instance(s) of {1} are needed.";
		public static string L_ERROR_INSTANCE_RELATED = "{0} cannot be deleted because it is related with {1}";
		public static string L_ERROR_INTERNALSERVERERROR = "Internal server error. Contact with your System Administrator.";
		public static string L_ERROR_INV_INHER_PATH = "Invalid inheritance path.";
		public static string L_ERROR_INV_ROLE = "Invalid role path.";
		public static string L_ERROR_LOW_CARD = "At least {0} instance(s) of {1} need to be related with {2}.";
		public static string L_ERROR_NO_AGENT = "The service cannot be executed by this agent.";
		public static string L_ERROR_NO_EXIST = "Instance of {0} does not exist.";
		public static string L_ERROR_NO_EXIST_INSTANCE = "The instance does not exist.";
		public static string L_ERROR_NO_FILTER_RIGHTS = "You do not have sufficient rights to access this information.";
		public static string L_ERROR_NOT_IMPLEMENTED = "The method or operation is not implemented.";
		public static string L_ERROR_NULL_ATTRIBUTE = "Attribute {1} cannot be null in class {0}.";
		public static string L_ERROR_NULL_ARGUMENT = "Outbound argument {2} cannot be null in service {0} of {1}.";
		public static string L_ERROR_STD = "Unable to execute {0} in the current state of {1}.";
		public static string L_ERROR_REQUEST = "Request Processing Error.";
		public static string L_ERROR_OCCURS = "An error has taken place.";
		public static string L_ERROR_LOADING_ATTR_REPORTSXML = "Error loading attribute '{0}' from '{1}' node in '{2}' file.";
		public static string L_ERROR_LOADING_REPORTSCONFIG = "Error loading reports configuration file.";
		public static string L_ERROR_SHOWING_REPORT = "Error showing ‘{0}’ report.";
		public static string L_ERROR_REPORT_GETTINGDATA = "Error getting report population.";
		public static string L_ERROR_DATASET_TABLENOTFOUND = "Table ‘{0}’ not found in DataSet ‘{1}’.";
		public static string L_ERROR_DATASET_GENERICLOAD = "Error loading DataSet file ‘{0}’.";
		public static string L_ERROR_DATASET_DEFINITION = "Error in DataSet definition. ‘{0}’ property not found.";
		public static string L_ERROR_DATASET_QUERYRELATED = "Error solving the query related: Class name: ‘{0}’, inverse role name: ‘{1}’.";
		#endregion Generic Errors

		#region Validation Messages
		public static string L_VALIDATION_NECESARY = "Field {0} is required";
		public static string L_VALIDATION_INTEGER_ERROR = "Field {0} must be an integer number";
		public static string L_VALIDATION_REAL_ERROR = "Field {0} must be a real number";
		public static string L_VALIDATION_SIZE = "Field {0} exceed maximum length:";
		public static string L_VALIDATION_SIZE_WITHOUT_ARGUMENT = "The field exceed maximum length:";
		public static string L_VALIDATION_NATURAL_ERROR = "Field {0} must be a natural number";
		public static string L_VALIDATION_NUMERIC_ERROR = "Field {0} must be a valid numeric value";
		public static string L_VALIDATION_DATE_ERROR = "Field {0} must be a valid date";
		public static string L_VALIDATION_DATETIME_ERROR = "Field {0} must be a valid datetime";
		public static string L_VALIDATION_TIME_ERROR = "Field {0} must be a valid time";
		public static string L_VALIDATION_FORMAT_ERROR = "Field {0} with format not correct";
		public static string L_VALIDATION_INVALID_FORMAT_MASK = "Format validation error ({0})";
		public static string L_VALIDATION_DEFSELECTION_ERROR = "Entered value must be contained in offered values";
		public static string L_VALIDATION_PRELOAD_ERROR = "Entered value must be contained in offered instances";
		#endregion Validation Messages

		#region MultiExection
		public static string L_MULTIEXE_EXECUTION = "Execution";
		public static string L_MULTIEXE_EXECUTION_REPORT = "Multiexecution Report of {0}";
		public static string L_MULTIEXE_PROCESSING = "Processing ...";
		public static string L_MULTIEXE_CANCEL_ACK = "Do you want to cancel the process?";
		public static string L_MULTIEXE_OK_PARAM = "{0} OK";
		public static string L_MULTIEXE_FAILURE = "Failure";
		public static string L_MULTIEXE_SUCESS = "Success";
		#endregion MultiExection

		#region Print scenario
		public static string L_PRINT = "Print";
		public static string L_PRINT_PREVIEW = "Preview";
		public static string L_PRINT_OPTIONS = "Print Options";
		public static string L_PRINT_SELECT_TEMPLATE = "Select template";
		public static string L_PRINT_SELECT_PRINTER = "Select printer";
		public static string L_PRINT_NUMBER_OF_COPIES = "Number of copies";
		public static string L_PRINT_OUTPUT = "Output";
		#endregion Print scenario

		#region General
		public static string L_WELCOME = "Welcome to application {0}";
		public static string L_NAVIGATIONS = "Navigations";
		public static string L_ACTIONS = "Actions";
		public static string L_SEARCH = "Search";

		public static string L_PAGINATION_BLOCKSIZE = "Elements per page";
		public static string L_PAGINATION_GOBEGINING = "Back to top";
		public static string L_PAGINATION_REFRESHDATA = "Refresh Data";
		public static string L_PAGINATION_GONEXT = "Next";
		public static string L_PAGINATION_GOPREVIOUS = "Previous";

		public static string L_EXECUTION_GONEXT = "Next";
		public static string L_EXECUTION_APPLYNEXT = "Apply Next";
		public static string L_EXECUTION_GOPREVIOUS = "Previous";
		public static string L_EXECUTION_APPLYPREVIOUS = "Apply Previous";

		public static string L_OK = "OK";
		public static string L_CANCEL = "Cancel";
		public static string L_EXECUTE = "Execute";
		public static string L_CLEAR = "Clear";
		public static string L_TRUE = "True";
		public static string L_FALSE = "False";
		public static string L_NONE = "None";
		public static string L_NULL = "Null";
		public static string L_WARNING = "Warning";
		public static string L_QUESTION = "Question";
		public static string L_EXIT = "Exit";
		public static string L_CLOSE = "Close";
		public static string L_AUTO = "Auto";
		public static string L_RETURN = "Return";
		public static string L_SELECT = "Select";
		public static string L_NO_SELECTION = "Please, select an instance first.";
		public static string L_ELEMENTSELECTED = "{0} selected elements";
		public static string L_FILE_NOT_EXIST = "The file does not exist";
		public static string L_FILE_NOT_LOADED = "The file has not been loaded";
		public static string L_FILE_NOT_PRIVILEGES = "You do not have read privileges";
		public static string L_NUMBER_OF_ELEMENTS = "Number of Elements";
		public static string L_IIU_COLUMN_NAME = "Name";
		public static string L_IIU_COLUMN_VALUE = "Value";
		public static string L_FILTER_ORDER_CRITERIA = "Order Criteria";
		public static string L_FILTERDEFINED = "Filter defined";
		public static string L_COPY = "Copy";
		public static string L_REPORT = "Report";
		public static string L_DETAILS = "Details";
		public static string L_HIDE = "Hide";

		public static string L_BOOL_NULL = "";
		public static string L_BOOL_TRUE = "Yes";
		public static string L_BOOL_FALSE = "No";

		public static string L_VALIDATION_AUTONUMERIC_TYPE = "Autonumeric";
		public static string L_VALIDATION_INTEGER_TYPE = "Integer";
		public static string L_CONFIRMDELETE = "Do you want to erase {0}?";
		public static string L_SAVE = "Save";
		public static string L_PENDINGCHANGES = "There are pending changes to be saved. If you continue, they will be lost. \r\nDo you want to continue?";

		#endregion General


		#region Preferences
		public static string L_PREFS_RENAMETITLE = "Changing name";
		public static string L_PREFS_NAME = "Name";
		public static string L_PREFS_ALIAS = "Alias";
		public static string L_PREFS_DATATYPE = "Data type";
		public static string L_PREFS_VISIBLE = "Visible";
		public static string L_PREFS_BLOCKSIZE = "Block size";
		public static string L_PREFS_ERR_NOTVALIDNAME = "Invalid name o null name";
		public static string L_PREFS_EDITITEM_TITLE = "Edit";
		public static string L_PREFS_TITLE = "Customization";
		public static string L_PREFS_TAB_DATA = "Data";
		public static string L_PREFS_TAB_OTHERS = "Others";
		public static string L_PREFS_BTN_COPYDS = "Copy";
		public static string L_PREFS_BTN_RENAMEDS = "Rename";
		public static string L_PREFS_BTN_DELDS = "Remove";
		public static string L_PREFS_BTN_UP = "Move up";
		public static string L_PREFS_BTN_DOWN = "Move down";
		public static string L_PREFS_BTN_ADDITEM = "Add";
		public static string L_PREFS_BTN_DELITEM = "Remove";
		#endregion Preferences

		#region State Change Detection
		public static string L_SCD_TITLE = "Warning";
		public static string L_SCD_MESSAGE = "Some values have changed. The process has not been executed. Please, close the window and check the data.";
		public static string L_SCD_MESSAGE_RETRY = "Some values have changed. The process has not been executed. You can retry the execution with the current values or close the window.";
		public static string L_SCD_ATR_ALIAS = "Item";
		public static string L_SCD_ATR_PREVIOUSVALUE = "Previous value";
		public static string L_SCD_ATR_CURRENTVALUE = "Current value";
		public static string L_SCD_BTN_RETRY = "Retry";
		#endregion State Change Detection
	}
}
