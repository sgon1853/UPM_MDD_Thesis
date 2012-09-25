// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;

using SIGEM.Client.Adaptor.Exceptions;

namespace SIGEM.Client.Adaptor.Serializer
{
	#region XML Error Deserializer
	/// <summary>
	/// Deserializes an XML Error file.
	/// </summary>
	internal class XMLErrorSerializer
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of XMLErrorSerializer.
		/// </summary>
		public XMLErrorSerializer()
		{}
		#endregion Constructors

		#region Serialize/Deserialize.
		/// <summary>
		/// Deserializes an XML Error file.
		/// </summary>
		/// <param name="reader">XML Error file.</param>
		/// <returns>ResponseException.</returns>
		public static ResponseException Deserialize(XmlReader reader )
		{
			ResponseException lResult=null;
			if(reader.IsStartElement(DTD.Error.TagError))
			{
				lResult = new ResponseException();
				// Read the Attributes from Error element.
				lResult.SetErrorType(reader.GetAttribute(DTD.Error.TagType));
				lResult.Number = int.Parse(reader.GetAttribute(DTD.Error.TagNumber));
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement(DTD.Error.TagError);
					do
					{
						#region Read Message Element.
						if (reader.IsStartElement(DTD.Error.TagErrorMessage))
						{
							if (!reader.IsEmptyElement)
							{
								lResult.SetMessage(reader.ReadString());
							}
						}
						#endregion Read Message Element.
						else
						{
							#region Read the Error.Params.
							if (reader.IsStartElement(DTD.Error.TagErrorParams))
							{
								lResult.Parameters = XMLErrorParamsSerialize.Deserialize(reader.ReadSubtree(), lResult.Parameters);
							}
							#endregion Read the Error.Params.
							else
							{
								#region Read the Trace Items.
								if (reader.IsStartElement(DTD.Error.TagErrorTrace))
								{
									lResult.Traces = XMLErrorTracesSerialize.Deserialize(reader.ReadSubtree(),null);
								}
								#endregion Read the Trace Items.
								else
								{
									#region Read the Changed Items.
									if (reader.IsStartElement(DTD.Error.TagChangedItems))
									{
										lResult.ChangedItems = XMLChangedItemsSerializer.Deserialize(reader.ReadSubtree()); ;
									}
									#endregion Read the Changed Items.
									else
									{
										#region Read the <?>.
										reader.Skip();
										if (reader.NodeType == XmlNodeType.None)
										{
											break;
										}
										else
										{
											continue;
										}
										#endregion Read the <?>.
									}
								}
							}
						}
					} while (reader.Read());
				}
			}
			return lResult;
		}
		#endregion Serialize/Deserialize.
	}
	#endregion XML Error Deserializer.

	#region XML Error Params Serialize.
	/// <summary>
	/// Deserializes parameters from an XML Error file.
	/// </summary>
	internal static class XMLErrorParamsSerialize
	{
		/// <summary>
		/// Deserializes parameters from an XML Error file.
		/// </summary>
		/// <param name="reader">XML Error file.</param>
		/// <param name="parameters">Parameters to obtain, if isn't null new elements are added.</param>
		/// <returns>Parameters.</returns>
		public static Parameters Deserialize(XmlReader reader, Parameters parameters)
		{
			if(reader.IsStartElement(DTD.Error.TagErrorParams))
			{
				if (parameters == null)
				parameters = new Parameters();

				// Read Attributes from Node.
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					// Proccess Sub nodes of "Params(Param)".
					do
					{
						if (reader.IsStartElement(DTD.Error.ErrorParams.TagErrorParam))
						{
							parameters.Add(XMLErrorParamSerialize.Deserialize(reader.ReadSubtree(), null));
						}
						else
						{
							reader.Skip();
							if (reader.NodeType == XmlNodeType.None)
							{
								break;
							}
							else
							{
								continue;
							}
						}
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			return parameters;
		}
	}
	#endregion XML Error Params Serialize.

	#region XML Error Param Serialize.
	/// <summary>
	/// Deserialize a parameter from an XML Error file.
	/// </summary>
	internal static class XMLErrorParamSerialize
	{
		/// <summary>
		/// Obtains the type and value of a parameter from an XML Error file.
		/// </summary>
		/// <param name="reader">XML Error file.</param>
		/// <param name="parameter">Parameter to obtain.</param>
		/// <returns>Parameter.</returns>
		public static Parameter Deserialize(XmlReader reader, Parameter parameter)
		{
			if (reader.IsStartElement(DTD.Error.ErrorParams.TagErrorParam))
			{
				if (parameter == null)
				{
					parameter = new Parameter();
				}

				// Read Attributes of Node.
				parameter.Key = reader.GetAttribute(DTD.Error.ErrorParams.TagKey);
				switch (reader.GetAttribute(DTD.Error.ErrorParams.TagType))
				{
					case ResponseException.ErrorKey:
						parameter.Type = ErrorParamType.Key;
						break;
					case ResponseException.ErrorLiteral:
						parameter.Type = ErrorParamType.Literal;
						break;
				}

				if (!reader.IsEmptyElement)
				{
					parameter.Text = reader.ReadString();
				}
				else
				{
					reader.Skip();
				}
			}
			return parameter;
		}
	}
	#endregion XML Error Param Serialize.

	#region XML Error Traces Serialize.
	/// <summary>
	/// Deserializes traces from an XML Error file.
	/// </summary>
	internal static class XMLErrorTracesSerialize
	{
		/// <summary>
		/// Deserializes traces from an XML Error file.
		/// </summary>
		/// <param name="reader">XML Error file.</param>
		/// <param name="traces">Traces to obtain, if isn't null new elements are added.</param>
		/// <returns>Traces.</returns>
		public static Traces Deserialize(XmlReader reader, Traces traces)
		{
			if (reader.IsStartElement(DTD.Error.TagErrorTrace))
			{
				if (traces == null)
				{
					traces = new Traces();
				}
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					// For each sub-Node.
					do
					{
						// Only ErrorTraceItem Nodes.
						if (reader.IsStartElement(DTD.Error.ErrorTrace.TagErrorTraceItem))
						{
							traces.Add(XMLErrorTraceSerialize.Deserialize(reader.ReadSubtree(), null));
						}
						else
						{
							reader.Skip();
							if (reader.NodeType == XmlNodeType.None)
							{
								break;
							}
							else
							{
								continue;
							}
						}
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			return traces;
		}
	}
	#endregion XML Traces Params Serialize.

	#region XML Error Trace Serialize.
	/// <summary>
	/// Deserializes a trace from an XML Error file.
	/// </summary>
	internal static class XMLErrorTraceSerialize
	{
		/// <summary>
		/// Deserializes a trace from an XML Error file.
		/// </summary>
		/// <param name="reader">XML Error file.</param>
		/// <param name="trace">Trace to obtain.</param>
		/// <returns>Trace.</returns>
		public static Trace Deserialize(XmlReader reader, Trace trace)
		{
			if (reader.IsStartElement(DTD.Error.ErrorTrace.TagErrorTraceItem))
			{
				if (trace == null)
				{
					trace = new Trace();
				}

				// Read Attributes.
				trace.Number = reader.GetAttribute(DTD.Error.ErrorTrace.TagNumber);
				switch (reader.GetAttribute(DTD.Error.ErrorTrace.TagType))
				{
				case ResponseException.ErrorExternal:
					trace.Type = ErrorTraceType.External;
					break;
				case ResponseException.ErrorModel:
					trace.Type = ErrorTraceType.Model;
					break;
				}

				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();

					do
					{
						if (reader.IsStartElement(DTD.Error.TagErrorMessage))
						{
							if (!reader.IsEmptyElement)
							{
								trace.Message = reader.ReadString();
							}
							else
							{
								reader.Skip();
								if (reader.NodeType == XmlNodeType.None)
								{
									break;
								}
								else
								{
									continue;
								}
							}
						}
						else
						{
							if (reader.IsStartElement(DTD.Error.TagErrorParams))
							{
								trace.Parameters = XMLErrorParamsSerialize.Deserialize(reader.ReadSubtree(), null);
							}
							else
							{
								reader.Skip();
								if (reader.NodeType == XmlNodeType.None)
								{
									break;
								}
								else
								{
									continue;
								}
							}
						}
					} while (reader.Read());
				}
				else
				{
					reader.Skip();
				}
			}
			return trace;
		}
	}
	#endregion XML Trace Params Serialize.
}

