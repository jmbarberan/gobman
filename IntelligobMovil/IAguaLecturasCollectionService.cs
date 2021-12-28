#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// Code is generated by Telerik Data Access Service Wizard
// using RestIDataService.tt template

namespace IntelligobMovil
{
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using IntelligobMovil.Dto;
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// AguaLecturasCollectionService interface.
	/// </summary>
	[ServiceContract]
	public interface IAguaLecturasCollectionService
	{
	    /// <summary>
	    /// Returns the item specified by its dto key.
	    /// </summary>
	    /// <param name="dtoKey">The specified dto key value.</param>
	    /// <returns></returns>
	    [WebGet(UriTemplate = "{dtoKey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
	    [OperationContract]
	    AguaLecturaDto ReadAguaLectura(string dtoKey);
	
	    /// <summary>
	    /// Returns a collection of items, along with URI links to each item.
	    /// </summary>
	    /// <returns></returns>
	    [WebGet(UriTemplate = "", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
	    [OperationContract]
	    ItemInfoList<AguaLecturaDto> ReadAguaLecturas();
	
	    /// <summary>
	    /// Adds the incoming item to the collection and returns the item along with a link to edit it.
	    /// </summary>
	    /// <param name="agualectura">The dto that holds the data to be inserted.</param>
	    /// <returns></returns>
	    [WebInvoke(Method = "POST", UriTemplate = "", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
	    [OperationContract]	
	    ItemInfo<AguaLecturaDto> CreateAguaLectura(AguaLecturaDto agualectura);
	    
	    /// <summary>
	    /// Updates the item specified by its dto key based on the incoming data and returns the updated item.
	    /// </summary>
	    /// <param name="dtoKey">The specified dto key value.</param>
	    /// <param name="agualectura">The dto that holds the data to be updated.</param>	
	    /// <returns></returns>
	    [WebInvoke(Method = "PUT", UriTemplate = "{dtoKey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
	    [OperationContract]
	    AguaLecturaDto UpdateAguaLectura(string dtoKey, AguaLecturaDto agualectura);
	
	    /// <summary>
	    /// Deletes the item specified by its dto key.
	    /// </summary>
	    /// <param name="dtoKey">The specified dto key value.</param>
	    [WebInvoke(Method = "DELETE", UriTemplate = "{dtoKey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
	    [OperationContract]    
	    void DeleteAguaLectura(string dtoKey);
	}
}
#pragma warning restore 1591