// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace SIGEM.Client.Adaptor.Serializer
{
	#region Query Request Serializer.
	/// <summary>
	///	Serializes QueryRequest to an XML stream.
	/// </summary>
	internal static class XMLQueryRequestSerializer
	{
		#region Serialize ServiceRequest.
		/// <summary>
		/// Serializes QueryRequest to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="queryRequest">QueryRequest.</param>
		/// <returns>XML stream with the QueryRequest.</returns>
		public static XmlWriter Serialize(XmlWriter writer, QueryRequest queryRequest)
		{
			writer.WriteStartElement(DTD.Request.TagQueryRequest);
			writer.WriteAttributeString(DTD.Request.QueryRequest.TagClass, queryRequest.Class);
			writer.WriteAttributeString(DTD.Request.QueryRequest.TagDisplaySet, queryRequest.DisplayItems);

			if (queryRequest.IsQueryInstance)
			{
				XMLQueryInstanceSerializer.Serialize(writer, queryRequest.QueryInstance);
			}
			else
			{
				if (queryRequest.IsQueryFilter)
				{
					XMLQueryFilterSerializer.Serialize(writer, queryRequest.QueryFilter);
				}
				else
				{
					if (queryRequest.IsQueryRelated)
					{
						XMLQueryRelatedSerializer.Serialize(writer, queryRequest.QueryRelated);
					}
				}
			}
			if (queryRequest.OrderCriteria.Length > 0)
			{
				writer.WriteStartElement(DTD.Request.QueryRequest.TagSort);
				writer.WriteAttributeString(DTD.Request.QueryRequest.TagSortCriterium, queryRequest.OrderCriteria);
				writer.WriteEndElement();
			}
			if (queryRequest.NavigationalFiltering != null)
			{
				XMLNavigationFilteringSerializer.Serialize(writer, queryRequest.NavigationalFiltering);
			}
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize ServiceRequest.
	}
	#endregion Query Request Serializer

	#region Query Instance Serializer
	/// <summary>
	/// Serializes QueryInstance to an XML stream.
	/// </summary>
	internal static class XMLQueryInstanceSerializer
	{
		#region Serialize Query Instance
		/// <summary>
		/// Serializes QueryInstance to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="queryInstance">QueryInstance.</param>
		/// <returns>XML stream with the QueryInstance.</returns>
		public static XmlWriter Serialize(XmlWriter writer, QueryInstance queryInstance)
		{
			writer.WriteStartElement(DTD.Request.QueryRequest.TagQueryInstance);
			if (queryInstance.Oid is Oids.AlternateKey)
			{
				XMLAlternateKeySerializer.Serialize(writer, queryInstance.GetAlternateKey());
			}
			else
			{
				XMLAdaptorOIDSerializer.Serialize(writer, queryInstance.Oid);
			}
			writer.WriteEndElement();
			return writer;
		}
	#endregion Serialize Query Instance
	}
	#endregion Query Instance Serializer

	#region Query Related Serializer for <StartRow> and <LinkedTo>
	/// <summary>
	/// Serializes a QueryRelated to an XML stream for StartRow and RelatedOids (LinkedTo).
	/// </summary>
	internal static class XMLStartRowLinkedToSerializer
	{
		#region Serialize <StartRow> and <LinkedTo>
		/// <summary>
		/// Serializes a QueryRelated to an XML stream for StartRow and RelatedOids (LinkedTo).
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="queryRelated">QueryRelated.</param>
		/// <returns>XML stream with the QueryRelated.</returns>
		public static XmlWriter Serialize(XmlWriter writer, QueryRelated queryRelated)
		{
			#region Serialize <StartRow>
			if (queryRelated.Oid != null)
			{
				writer.WriteStartElement(DTD.Request.QueryRequest.QueryRelated.TagStartRow);
				XMLAdaptorOIDSerializer.Serialize(writer, queryRelated.Oid);
				writer.WriteEndElement();
			}
			#endregion Serialize <StartRow>

			#region Serialize <LinkedTo>
			if (queryRelated.LinkedTo != null)
			{
				writer.WriteStartElement(DTD.Request.QueryRequest.QueryRelated.TagLinkedTo);
				foreach (string lRole in queryRelated.LinkedTo.Keys)
				{
					writer.WriteStartElement(DTD.Request.QueryRequest.QueryRelated.LinkedTo.TagLinkItem);
					writer.WriteAttributeString(DTD.Request.QueryRequest.QueryRelated.LinkedTo.TagRole, lRole);
					XMLAdaptorOIDSerializer.Serialize(writer, queryRelated.LinkedTo[lRole]);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			#endregion Serialize <LinkedTo>
			return writer;
		}
		#endregion Serialize <StartRow> and <LinkedTo>
	}
	#endregion Query Related Serializer for <StartRow> and <LinkedTo>

	#region Query Related Serializer
	/// <summary>
	/// Serializes a QueryRelated to an XML stream.
	/// </summary>
	internal static class XMLQueryRelatedSerializer
	{
		#region Serialize <Query.Related>.
		/// <summary>
		/// Serializes a QueryRelated to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="queryRelated">QueryRelated.</param>
		/// <returns>XML stream with the QueryRelated.</returns>
		public static XmlWriter Serialize(XmlWriter writer, QueryRelated queryRelated)
		{
			writer.WriteStartElement(DTD.Request.QueryRequest.TagQueryRelated);
			writer.WriteAttributeString(DTD.Request.QueryRequest.QueryRelated.TagBlockSize, queryRelated.BlockSize.ToString());
			XMLStartRowLinkedToSerializer.Serialize(writer, queryRelated);
			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize <Query.Related>.
	}
	#endregion Query Related Serializer

	#region Query Filter Serializer
	/// <summary>
	/// Serializes a QueryFilter to an XML stream.
	/// </summary>
	internal static class XMLQueryFilterSerializer
	{
		#region Serialize Filter Related.
		/// <summary>
		/// Serializes a QueryFilter to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="queryFilter">QueryFilter.</param>
		/// <returns>XML stream with the QueryFilter.</returns>
		public static XmlWriter Serialize(XmlWriter writer, QueryFilter queryFilter)
		{
			writer.WriteStartElement(DTD.Request.QueryRequest.TagQueryFilter);
			writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.TagName, queryFilter.Name);
			writer.WriteAttributeString(DTD.Request.QueryRequest.QueryFilter.TagBlockSize, queryFilter.BlockSize.ToString());

			XMLStartRowLinkedToSerializer.Serialize(writer, queryFilter as QueryRelated);

			#region Serialize <Filter.Variables>.
			if (queryFilter.Variables != null)
			{
				XMLFilterVariablesSerializer.Serialize(writer, queryFilter.Variables);
			}
			#endregion Serialize <Filter.Variables>.

			writer.WriteEndElement();
			return writer;
		}
		#endregion Serialize Query Related.
	}
	#endregion Query Related Serializer

	#region NavigationFiltering
	/// <summary>
	/// Serializes a NavigationalFiltering to an XML stream.
	/// </summary>
	internal static class XMLNavigationFilteringSerializer
	{
		#region Serialize NavigationFiltering.
		/// <summary>
		/// Serializes a NavigationalFiltering to an XML stream.
		/// </summary>
		/// <param name="writer">XML stream to write.</param>
		/// <param name="navigationalFiltering">NavigationalFiltering.</param>
		/// <returns>XML stream with the NavigationalFiltering.</returns>
		public static XmlWriter Serialize(XmlWriter writer, NavigationalFiltering navigationalFiltering)
		{
			#region <NavFilt>
			writer.WriteStartElement(DTD.Request.QueryRequest.TagNavFilt);

			if (navigationalFiltering.IsNavigationalArgument)
			{
			#region <NavFilt.Argument>
			writer.WriteStartElement(DTD.Request.QueryRequest.NavFilt.TagNavFiltArgument);
				writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.NaviFiltArgument.TagClass, navigationalFiltering.Argument.ClassName);
				writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.NaviFiltArgument.TagService, navigationalFiltering.Argument.ServiceName);
				writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.NaviFiltArgument.TagArgument, navigationalFiltering.Argument.ArgumentName);

			#region <Arguments>
				XMLNavigationalFilteringArgumentsListSerializer.Serialize(writer, navigationalFiltering.Argument.Arguments);
			#endregion <Arguments>

			writer.WriteEndElement();
			#endregion <NavFilt.Argument>
			}
			else if (navigationalFiltering.IsNavigationalFilterVariable)
			{
				#region <NavFilt.Variable>
				writer.WriteStartElement(DTD.Request.QueryRequest.NavFilt.TagNavFiltVariable);
				writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.NaviFiltVariable.TagClass, navigationalFiltering.FilterVariable.ClassName);
				writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.NaviFiltVariable.TagFilter, navigationalFiltering.FilterVariable.FilterName);
				writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.NaviFiltVariable.TagVariable, navigationalFiltering.FilterVariable.ArgumentName);

				#region <Filter.Variables>
				XMLNavigationalFilteringVariablesListSerializer.Serialize(writer, navigationalFiltering.FilterVariable.Arguments);
				#endregion <Filter.Variables>

				writer.WriteEndElement();
				#endregion <NavFilt.Variable>
			}
			else if(navigationalFiltering.IsNavigationalSelectObject)
			{
				#region <NavFilt.SelectedObject>
				writer.WriteStartElement(DTD.Request.QueryRequest.NavFilt.TagNavFiltSelectedObject);

				#region <NavFilt.SelectedObject NavFilterID Attribute>
				if (navigationalFiltering.SelectedObject.NavigationalFilterID.Length > 0)
				{
					writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.TagNavFilterID, navigationalFiltering.SelectedObject.NavigationalFilterID);
				}
				#endregion <NavFilt.SelectedObject NavFilterID Attribute>

				#region OID
				XMLAdaptorOIDSerializer.Serialize(writer, navigationalFiltering.SelectedObject.SelectedObjectOID);
				#endregion OID

				writer.WriteEndElement();
				#endregion <NavFilt.SelectedObject>
			}
			else if (navigationalFiltering.IsNavigationalServiceIU)
			{
			#region <NavFilt.ServiceIU>
				writer.WriteStartElement(DTD.Request.QueryRequest.NavFilt.TagNavFiltServiceIU);

				#region <NavFilt.ServiceIU NavFilterID Attribute>
				if (navigationalFiltering.ServiceIU.NavigationalFilterID.Length > 0)
				{
					writer.WriteAttributeString(DTD.Request.QueryRequest.NavFilt.TagNavFilterID, navigationalFiltering.ServiceIU.NavigationalFilterID);
				}
				#endregion <NavFilt.ServiceIU NavFilterID Attribute>

				#region <Arguments>
				XMLNavigationalFilteringArgumentsListSerializer.Serialize(writer, navigationalFiltering.ServiceIU.Arguments);
				#endregion <Arguments>

				writer.WriteEndElement();
			#endregion <NavFilt.ServiceIU>
			}

			writer.WriteEndElement();
			return writer;
			#endregion <NavFilt>
		}
		#endregion Serialize NavigationFiltering.
	}
	#endregion NavigationFiltering
}

