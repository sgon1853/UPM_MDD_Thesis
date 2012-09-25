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
	/// Class that manages the internationalization constant keys (multilaguage feature).
	/// </summary>
	public static class LanguageConstantKeys
	{
		#region Login scenario
		public static string L_LOGIN = "L_LOGIN";
		public static string L_LOGIN_MSG = "L_LOGIN_MSG";
		public static string L_LOGIN_PASSWORD = "L_LOGIN_PASSWORD";
		public static string L_LOGIN_ERROR = "L_LOGIN_ERROR";
		public static string L_LOGIN_NOT_NULL = "L_LOGIN_NOT_NULL";
		public static string L_LOGIN_INCORRECT = "L_LOGIN_INCORRECT";
		public static string L_LOGIN_LANGUAGE = "L_LOGIN_LANGUAGE";
		public static string L_LOGIN_PROFILE = "L_LOGIN_PROFILE";
		#endregion Login scenario

		#region Change password scenario
		public static string L_PASSWORD = "L_PASSWORD";
		public static string L_PASSWORD_NOT_NULL = "L_PASSWORD_NOT_NULL";
		public static string L_PASSWORD_CAPTION = "L_PASSWORD_CAPTION";
		public static string L_PASSWORD_CURRENT = "L_PASSWORD_CURRENT";
		public static string L_PASSWORD_NEW = "L_PASSWORD_NEW";
		public static string L_PASSWORD_RETYPE = "L_PASSWORD_RETYPE";
		public static string L_PASSWORD_CURRENT_NOT_NULL = "L_PASSWORD_CURRENT_NOT_NULL";
		public static string L_PASSWORD_NEW_NOT_NULL = "L_PASSWORD_NEW_NOT_NULL";
		public static string L_PASSWORD_RETYPE_NEW_NOT_NULL = "L_PASSWORD_RETYPE_NEW_NOT_NULL";
		public static string L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL = "L_PASSWORD_NEW_AND_RETYPED_NOT_EQUAL";
		public static string L_PASSWORD_INCORRECT = "L_PASSWORD_INCORRECT";
		public static string L_PASSWORD_UPDATED = "L_PASSWORD_UPDATED";
		public static string L_PASSWORD_UPDATE_FAILED = "L_PASSWORD_UPDATE_FAILED";
		#endregion Change password scenario

		#region MainScenario scenario
		public static string L_MAIN_CHANGE_LANGUAGE = "L_MAIN_CHANGE_LANGUAGE";
		public static string L_MAIN_CLOSE_ALL_WINDOWS = "L_MAIN_CLOSE_ALL_WINDOWS";
		public static string L_MAIN_CLOSE = "L_MAIN_CLOSE";
		public static string L_MAIN_MENU_FILE = "L_MAIN_MENU_FILE";
		public static string L_MAIN_MENU_WINDOW = "L_MAIN_MENU_WINDOW";
		public static string L_MAIN_MENU_HELP = "L_MAIN_MENU_HELP";
		public static string L_MAIN_MENU_FILE_EXIT = "L_MAIN_MENU_FILE_EXIT";
		public static string L_MAIN_MENU_HELP_ABOUT = "L_MAIN_MENU_HELP_ABOUT";
		public static string L_MAIN_MENU_WINDOW_MAXIMIZE = "L_MAIN_MENU_WINDOW_MAXIMIZE";
		public static string L_MAIN_MENU_WINDOW_MINIMIZE = "L_MAIN_MENU_WINDOW_MINIMIZE";
		public static string L_MAIN_MENU_WINDOW_CASCADE = "L_MAIN_MENU_WINDOW_CASCADE";
		public static string L_MAIN_MENU_WINDOW_HORIZONTAL = "L_MAIN_MENU_WINDOW_HORIZONTAL";
		public static string L_MAIN_MENU_WINDOW_VERTICAL = "L_MAIN_MENU_WINDOW_VERTICAL";
		public static string L_MAIN_MENU_WINDOW_CLOSEALL = "L_MAIN_MENU_WINDOW_CLOSEALL";
		public static string L_MAIN_LOGOUT = "L_MAIN_LOGOUT ";
		#endregion MainScenario scenario

		#region Contextual Menu
		public static string L_POP_UP_MENU_HELP = "L_POP_UP_MENU_HELP";
		public static string L_POP_UP_MENU_OPTIONS = "L_POP_UP_MENU_OPTIONS";
		public static string L_POP_UP_MENU_REFRESH = "L_POP_UP_MENU_REFRESH";
		public static string L_POP_UP_MENU_RETRIEVE_ALL = "L_POP_UP_MENU_RETRIEVE_ALL";
		public static string L_POP_UP_MENU_NAVIGATIONS = "L_POP_UP_MENU_NAVIGATIONS";
		public static string L_POP_UP_MENU_EXPORT_TO_WORD = "L_POP_UP_MENU_EXPORT_TO_WORD";
		public static string L_POP_UP_MENU_EXPORT_TO_EXCEL = "L_POP_UP_MENU_EXPORT_TO_EXCEL";
		public static string L_POP_UP_MENU_PREFERENCES = "L_POP_UP_MENU_PREFERENCES";
		public static string L_POP_UP_MENU_SAVEPOSITION = "L_POP_UP_MENU_SAVEPOSITION";
		public static string L_POP_UP_MENU_SAVECOLUMNSWIDTH = "L_POP_UP_MENU_SAVECOLUMNSWIDTH";
		#endregion Contextual Menu

		public static string L_CONFIRM = "L_CONFIRM";

		#region About scenario
		public static string L_ABOUT_VERSION = "L_ABOUT_VERSION";
		public static string L_ABOUT_ENGINEERED = "L_ABOUT_ENGINEERED";
		public static string L_ABOUT_COPYRIGHT = "L_ABOUT_COPYRIGHT";
		public static string L_ABOUT_FORM_TITLE = "L_ABOUT_FORM_TITLE";
		#endregion About scenario

		#region Bussines Logic Errors
		public static string L_ERROR_BL_CONNECTION = "L_ERROR_BL_CONNECTION";
		public static string L_ERROR_BL_INTERNALSERVER = "L_ERROR_BL_INTERNALSERVER";
		public static string L_ERROR_BL_CHANGE_LANGUAGE = "L_ERROR_BL_CHANGE_LANGUAGE";
		#endregion Bussines Logic Errors

		#region Generic Errors
		public static string L_ERROR = "L_ERROR";
		public static string L_ERROR_ARG_NO_MATCH = "L_ERROR_ARG_NO_MATCH";
		public static string L_ERROR_ARG_NO_VALUE = "L_ERROR_ARG_NO_VALUE";
		public static string L_ERROR_ARG_NO_NATURAL = "L_ERROR_ARG_NO_NATURAL";
		public static string L_ERROR_BAD_IDENTITY = "L_ERROR_BAD_IDENTITY";
		public static string L_ERROR_DIN_REL_EXIST = "L_ERROR_DIN_REL_EXIST";
		public static string L_ERROR_DIN_REL_NO_EXIST = "L_ERROR_DIN_REL_NO_EXIST";
		public static string L_ERROR_EXIST_INSTANCE = "L_ERROR_EXIST_INSTANCE";
		public static string L_ERROR_EXPECT_SPE_ARG = "L_ERROR_EXPECT_SPE_ARG";
		public static string L_ERROR_HIGH_CARD = "L_ERROR_HIGH_CARD";
		public static string L_ERROR_INSTANCE_RELATED = "L_ERROR_INSTANCE_RELATED";
		public static string L_ERROR_INTERNALSERVERERROR = "L_ERROR_INTERNALSERVERERROR";
		public static string L_ERROR_INV_INHER_PATH = "L_ERROR_INV_INHER_PATH";
		public static string L_ERROR_INV_ROLE = "L_ERROR_INV_ROLE";
		public static string L_ERROR_LOW_CARD = "L_ERROR_LOW_CARD";
		public static string L_ERROR_NO_AGENT = "L_ERROR_NO_AGENT";
		public static string L_ERROR_NO_EXIST = "L_ERROR_NO_EXIST";
		public static string L_ERROR_NO_EXIST_INSTANCE = "L_ERROR_NO_EXIST_INSTANCE";
		public static string L_ERROR_NO_FILTER_RIGHTS = "L_ERROR_NO_FILTER_RIGHTS";
		public static string L_ERROR_NOT_IMPLEMENTED = "L_ERROR_NOT_IMPLEMENTED";
		public static string L_ERROR_NULL_ATTRIBUTE = "L_ERROR_NULL_ATTRIBUTE";
		public static string L_ERROR_NULL_ARGUMENT = "L_ERROR_NULL_ARGUMENT";
		public static string L_ERROR_STD = "L_ERROR_STD";
		public static string L_ERROR_REQUEST = "L_ERROR_REQUEST";
		public static string L_ERROR_OCCURS = "L_ERROR_OCCURS";
		public static string L_ERROR_LOADING_ATTR_REPORTSXML = "L_ERROR_LOADING_ATTR_REPORTSXML";
		public static string L_ERROR_LOADING_REPORTSCONFIG = "L_ERROR_LOADING_REPORTSCONFIG";
		public static string L_ERROR_SHOWING_REPORT = "L_ERROR_SHOWING_REPORT";
		public static string L_ERROR_REPORT_GETTINGDATA = "L_ERROR_REPORT_GETTINGDATA";
		public static string L_ERROR_DATASET_TABLENOTFOUND = "L_ERROR_DATASET_TABLENOTFOUND";
		public static string L_ERROR_DATASET_GENERICLOAD = "L_ERROR_DATASET_GENERICLOAD";
		public static string L_ERROR_DATASET_DEFINITION = "L_ERROR_DATASET_DEFINITION";
		public static string L_ERROR_DATASET_QUERYRELATED = "L_ERROR_DATASET_QUERYRELATED";
		#endregion Generic Errors

		#region Validation Messages
		public static string L_VALIDATION_NECESARY = "L_VALIDATION_NECESARY";
		public static string L_VALIDATION_INTEGER_ERROR = "L_VALIDATION_INTEGER_ERROR";
		public static string L_VALIDATION_REAL_ERROR = "L_VALIDATION_REAL_ERROR";
		public static string L_VALIDATION_SIZE = "L_VALIDATION_SIZE";
		public static string L_VALIDATION_SIZE_WITHOUT_ARGUMENT = "L_VALIDATION_SIZE_WITHOUT_ARGUMENT";
		public static string L_VALIDATION_NATURAL_ERROR = "L_VALIDATION_NATURAL_ERROR";
		public static string L_VALIDATION_NUMERIC_ERROR = "L_VALIDATION_NUMERIC_ERROR";
		public static string L_VALIDATION_DATE_ERROR = "L_VALIDATION_DATE_ERROR";
		public static string L_VALIDATION_DATETIME_ERROR = "L_VALIDATION_DATETIME_ERROR";
		public static string L_VALIDATION_TIME_ERROR = "L_VALIDATION_TIME_ERROR";
		public static string L_VALIDATION_FORMAT_ERROR = "L_VALIDATION_FORMAT_ERROR";
		public static string L_VALIDATION_INVALID_FORMAT_MASK = "L_VALIDATION_INVALID_FORMAT_MASK";
		public static string L_VALIDATION_DEFSELECTION_ERROR = "L_VALIDATION_DEFSELECTION_ERROR";
		public static string L_VALIDATION_PRELOAD_ERROR = "L_VALIDATION_PRELOAD_ERROR";
		#endregion Validation Messages

		#region MultiExection
		public static string L_MULTIEXE_EXECUTION = "L_MULTIEXE_EXECUTION";
		public static string L_MULTIEXE_EXECUTION_REPORT = "L_MULTIEXE_EXECUTION_REPORT";
		public static string L_MULTIEXE_PROCESSING = "L_MULTIEXE_PROCESSING";
		public static string L_MULTIEXE_CANCEL_ACK = "L_MULTIEXE_CANCEL_ACK";
		public static string L_MULTIEXE_OK_PARAM = "L_MULTIEXE_OK_PARAM";
		public static string L_MULTIEXE_FAILURE = "L_MULTIEXE_FAILURE";
		public static string L_MULTIEXE_SUCESS = "L_MULTIEXE_SUCESS";
		#endregion MultiExection

		#region Print scenario
		public static string L_PRINT = "L_PRINT";
		public static string L_PRINT_PREVIEW = "L_PRINT_PREVIEW";
		public static string L_PRINT_OPTIONS = "L_PRINT_OPTIONS";
		public static string L_PRINT_SELECT_TEMPLATE = "L_PRINT_SELECT_TEMPLATE";
		public static string L_PRINT_SELECT_PRINTER = "L_PRINT_SELECT_PRINTER";
		public static string L_PRINT_NUMBER_OF_COPIES = "L_PRINT_NUMBER_OF_COPIES";
		public static string L_PRINT_OUTPUT = "L_PRINT_OUTPUT";
		#endregion Print scenario

		#region General
		public static string L_WELCOME = "L_WELCOME";
		public static string L_NAVIGATIONS = "L_NAVIGATIONS";
		public static string L_ACTIONS = "L_ACTIONS";
		public static string L_SEARCH = "L_SEARCH";

		public static string L_PAGINATION_BLOCKSIZE = "L_PAGINATION_BLOCKSIZE";
		public static string L_PAGINATION_GOBEGINING = "L_PAGINATION_GOBEGINING";
		public static string L_PAGINATION_REFRESHDATA = "L_PAGINATION_REFRESHDATA";
		public static string L_PAGINATION_GONEXT = "L_PAGINATION_GONEXT";
		public static string L_PAGINATION_GOPREVIOUS = "L_PAGINATION_GOPREVIOUS";

		public static string L_EXECUTION_GONEXT = "L_EXECUTION_GONEXT";
		public static string L_EXECUTION_APPLYNEXT = "L_EXECUTION_APPLYNEXT";
		public static string L_EXECUTION_GOPREVIOUS = "L_EXECUTION_GOPREVIOUS";
		public static string L_EXECUTION_APPLYPREVIOUS = "L_EXECUTION_APPLYPREVIOUS";

		public static string L_OK = "L_OK";
		public static string L_CANCEL = "L_CANCEL";
		public static string L_EXECUTE = "L_EXECUTE";
		public static string L_CLEAR = "L_CLEAR";
		public static string L_TRUE = "L_TRUE";
		public static string L_FALSE = "L_FALSE";
		public static string L_NONE = "L_NONE";
		public static string L_NULL = "L_NULL";
		public static string L_WARNING = "L_WARNING";
		public static string L_QUESTION = "L_QUESTION";
		public static string L_EXIT = "L_EXIT";
		public static string L_CLOSE = "L_CLOSE";
		public static string L_AUTO = "L_AUTO";
		public static string L_RETURN = "L_RETURN";
		public static string L_SELECT = "L_SELECT";
		public static string L_NO_SELECTION = "L_NO_SELECTION";
		public static string L_ELEMENTSELECTED = "L_ELEMENTSELECTED";
		public static string L_FILE_NOT_EXIST = "L_FILE_NOT_EXIST";
		public static string L_FILE_NOT_LOADED = "L_FILE_NOT_LOADED";
		public static string L_FILE_NOT_PRIVILEGES = "L_FILE_NOT_PRIVILEGES";
		public static string L_NUMBER_OF_ELEMENTS = "L_NUMBER_OF_ELEMENTS";
		public static string L_IIU_COLUMN_NAME = "L_IIU_COLUMN_NAME";
		public static string L_IIU_COLUMN_VALUE = "L_IIU_COLUMN_VALUE";
		public static string L_FILTER_ORDER_CRITERIA = "L_FILTER_ORDER_CRITERIA";
		public static string L_FILTERDEFINED = "L_FILTERDEFINED";
		public static string L_COPY = "L_COPY";
		public static string L_REPORT = "L_REPORT";
		public static string L_DETAILS = "L_DETAILS";
		public static string L_HIDE = "L_HIDE";

		public static string L_BOOL_NULL = "L_BOOL_NULL";
		public static string L_BOOL_TRUE = "L_BOOL_TRUE";
		public static string L_BOOL_FALSE = "L_BOOL_FALSE";

		public static string L_VALIDATION_AUTONUMERIC_TYPE = "L_VALIDATION_AUTONUMERIC_TYPE";
		public static string L_VALIDATION_INTEGER_TYPE = "L_VALIDATION_INTEGER_TYPE";
		public static string L_CONFIRMDELETE = "L_CONFIRMDELETE";
		public static string L_SAVE = "L_SAVE";
		public static string L_PENDINGCHANGES = "L_PENDINGCHANGES";
		#endregion General


		#region Preferences
		public static string L_PREFS_RENAMETITLE = "L_PREFS_RENAMETITLE";
		public static string L_PREFS_NAME = "L_PREFS_NAME";
		public static string L_PREFS_ALIAS = "L_PREFS_ALIAS";
		public static string L_PREFS_DATATYPE = "L_PREFS_DATATYPE";
		public static string L_PREFS_VISIBLE = "L_PREFS_VISIBLE";
		public static string L_PREFS_BLOCKSIZE = "L_PREFS_BLOCKSIZE";
		public static string L_PREFS_ERR_NOTVALIDNAME = "L_PREFS_ERR_NOTVALIDNAME";
		public static string L_PREFS_EDITITEM_TITLE = "L_PREFS_EDITITEM_TITLE";
		public static string L_PREFS_TITLE = "L_PREFS_TITLE";
		public static string L_PREFS_TAB_DATA = "L_PREFS_TAB_DATA";
		public static string L_PREFS_TAB_OTHERS = "L_PREFS_TAB_OTHERS";
		public static string L_PREFS_BTN_COPYDS = "L_PREFS_BTN_COPYDS";
		public static string L_PREFS_BTN_RENAMEDS = "L_PREFS_BTN_RENAMEDS";
		public static string L_PREFS_BTN_DELDS = "L_PREFS_BTN_DELDS";
		public static string L_PREFS_BTN_UP = "L_PREFS_BTN_UP";
		public static string L_PREFS_BTN_DOWN = "L_PREFS_BTN_DOWN";
		public static string L_PREFS_BTN_ADDITEM = "L_PREFS_BTN_ADDITEM";
		public static string L_PREFS_BTN_DELITEM = "L_PREFS_BTN_DELITEM";
		#endregion Preferences

		#region State Change Detection
		public static string L_SCD_TITLE = "L_SCD_TITLE";
		public static string L_SCD_MESSAGE = "L_SCD_MESSAGE";
		public static string L_SCD_MESSAGE_RETRY = "L_SCD_MESSAGE_RETRY"; 
		public static string L_SCD_ATR_ALIAS = "L_SCD_ATR_ALIAS";
		public static string L_SCD_ATR_PREVIOUSVALUE = "L_SCD_ATR_PREVIOUSVALUE";
		public static string L_SCD_ATR_CURRENTVALUE = "L_SCD_ATR_CURRENTVALUE";
		public static string L_SCD_BTN_RETRY = "L_SCD_BTN_RETRY";
		#endregion State Change Detection

	}
}
