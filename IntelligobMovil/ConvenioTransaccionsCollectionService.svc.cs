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
// using RestDataService.tt template

namespace IntelligobMovil
{
	using System.Globalization;
	using System.Net;
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using Telerik.OpenAccess;
	using IntelligobMovil.Dto;
	using IntelligobMovil.Assemblers;
	using IntelligobMovil.Repositories;
	using IntelligobMovil.Services;
	using Intelligob.Entidades;
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

		/// <summary>
		/// ConvenioTransaccionsCollectionService service class handler.
		/// </summary> 
		[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
		public partial class ConvenioTransaccionsCollectionService : IConvenioTransaccionsCollectionService
		{
			#region Private Static Fields
	
			/// <summary>    
			/// The URI template to manipulate a particular item. 
			/// The URL is of the form http://<url-for-svc-file>/dtoKey
			/// </summary>
			private static readonly UriTemplate itemUriTemplate = new UriTemplate("{dtoKey}");
	
			/// <summary>
			/// The URI template to get all the items or add an item. 
			/// The URL is of the form http://<url-for-svc-file>/    
			/// </summary>    
			private static readonly UriTemplate itemsUriTemplate = new UriTemplate("");
	
			/// <summary>    
			/// The URI template to get a partial collection of items.
			/// The URL is of the form http://<url-for-svc-file>/?page=10
			/// </summary>
			private static readonly UriTemplate pagedItemsUriTemplate = new UriTemplate("?page={pageNumber}");
	
			#endregion
	
			#region Private Fields
	
			/// <summary>
			/// Maintains a list of objects affected by a business transaction and coordinates 
			/// the writing out of changes and the resolution of concurrency problems.
			/// </summary>
			private IModeloUnitOfWork unitOfWork;
	
			/// <summary>
			/// Returned entities per page.
			/// </summary>
			private int conveniotransaccionsPerPageCount = 5;
	
			/// <summary>
			/// A general purpose generated service used for all CRUD operations against ConvenioTransaccion entity.
			/// </summary>
			private IConvenioTransaccionService service;
	
			#endregion
	
			#region Public Static Methods
	
			/// <summary>
			/// Gets link to the entity with a specified dto key.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <param name="template">The URI template to be used.</param>
			/// <returns>A link to the entity.</returns>
			public static Uri GetItemLink(string dtoKey, UriTemplate template)
			{
				if (WebOperationContext.Current.IncomingRequest.UriTemplateMatch != null)
				{
					return template.BindByPosition(WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri, new string[] { dtoKey });
				}
	            
				return default(Uri);
			}
	
			#endregion
	
			#region IConvenioTransaccionsCollectionService Members
	
			/// <summary>
			/// Returns the item specified by its dto key.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <returns></returns>
			public ConvenioTransaccionDto ReadConvenioTransaccion(string dtoKey)
			{
				return this.HandleReadConvenioTransaccion(dtoKey);
			}
	
			/// <summary>
			/// Returns a collection of items, along with URI links to each item.
			/// </summary>
			/// <returns></returns>
			public ItemInfoList<ConvenioTransaccionDto> ReadConvenioTransaccions()
			{
				return this.HandleReadConvenioTransaccions(itemUriTemplate);
			}
	
	
			/// <summary>
			/// Adds the incoming item to the collection and returns the item along with a link to edit it.
			/// </summary>
			/// <param name="conveniotransaccion">The dto that holds the data to be inserted.</param>
			/// <returns></returns>
			public ItemInfo<ConvenioTransaccionDto> CreateConvenioTransaccion(ConvenioTransaccionDto conveniotransaccion)
			{
				return this.HandleCreateConvenioTransaccion(conveniotransaccion, itemUriTemplate);
			}
	
	
			/// <summary>
			/// Updates the item specified by its dto key based on the incoming data and returns the updated item.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <param name="conveniotransaccion">The dto that holds the data to be updated.</param>	
			/// <returns></returns>
			public ConvenioTransaccionDto UpdateConvenioTransaccion(string dtoKey, ConvenioTransaccionDto conveniotransaccion)
			{
				return this.HandleUpdateConvenioTransaccion(dtoKey, conveniotransaccion);
			}
	
	
			/// <summary>
			/// Deletes the item specified by its dto key.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			public void DeleteConvenioTransaccion(string dtoKey)
			{
				this.HandleDeleteConvenioTransaccion(dtoKey);
			}
	    
	
			#endregion
	
			#region Service Members
	
			/// <summary>
			/// Gets or sets the number of ConvenioTransaccions returned per page.
			/// </summary>
			public int ConvenioTransaccionsPerPageCount
			{
				get 
				{ 
					return this.conveniotransaccionsPerPageCount; 
				}
				set 
				{
					if (value != this.conveniotransaccionsPerPageCount)
					{
						this.conveniotransaccionsPerPageCount = value;
					}
				}
			}
	    
			/// <summary>
			/// Gets or sets the specific UnitOfWork. It maintains a list of objects affected 
			/// by a business transaction and coordinates the writing out of changes and the resolution of concurrency problems.
			/// </summary>
			public IModeloUnitOfWork UnitOfWork
			{
				get
				{
					if(this.unitOfWork == null)
					{
						this.unitOfWork = new Modelo();
					}
					return this.unitOfWork;
				}
				set
				{
					this.unitOfWork = value;
				}
			}
	
			/// <summary>
			/// Gets or sets the general purpose generated service used for all CRUD operations against ConvenioTransaccion entity.
			/// </summary>
			protected virtual IConvenioTransaccionService Service
			{
				get
				{
					if (this.service == null)
					{
						IConvenioTransaccionAssembler assembler = new ConvenioTransaccionAssembler();
						IConvenioTransaccionRepository repository = new ConvenioTransaccionRepository(this.UnitOfWork);
	
						this.service = new ConvenioTransaccionService(assembler, repository);
					}
					return this.service;
				}
			}
	
	
			/// <summary>
			/// Adds a conveniotransaccion to the database and return the conveniotransaccion with updated dto key. Returns null if adding the item failed.
			/// A null return value will result in a response status code of InternalServerError (500).
			/// </summary>
			/// <param name="conveniotransaccion">The dto that holds the data to be inserted.</param>
			/// <returns></returns>
			protected ConvenioTransaccionDto Create(ConvenioTransaccionDto conveniotransaccion)
			{
				try
				{
					string dtoKey = this.Service.Add(conveniotransaccion);
					this.UnitOfWork.SaveChanges();
					ConvenioTransaccionDto addedConvenioTransaccion = this.Read(dtoKey);
					if (addedConvenioTransaccion == null)
					{
						OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
						response.StatusCode = HttpStatusCode.InternalServerError;
						response.StatusDescription = "Could not load the newly created item.";
						return null;
					}
	
					return addedConvenioTransaccion;
				}
				catch (OpenAccessException)
				{
					OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.InternalServerError;
					response.StatusDescription = "Error occurred while persisting the new item.";
					return null;
				}
				catch (ArgumentException)
				{
					OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.BadRequest;
					response.StatusDescription = "";
					return null;
				}
			}
	
			/// <summary>
			/// Returns the ConvenioTransaccionDto with the given dto key. 
			/// A response status code of NotFound (404) will be the result if the item does not exist.
			/// A response status code of BadRequest (400) will be the result if an error occur.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <returns></returns>
			protected ConvenioTransaccionDto Read(string dtoKey)
			{
				try
				{
					ConvenioTransaccionDto conveniotransaccion = this.Service.GetByKey(dtoKey);
					if (conveniotransaccion == null)
					{
						OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
						response.StatusCode = HttpStatusCode.NotFound;
						response.StatusDescription = string.Format("Could not find item with the specified dtoKey ({0})", dtoKey);
						return null;
					}
	
					return conveniotransaccion;
				}
				catch (Telerik.OpenAccess.Exceptions.NoSuchObjectException)
	            {
	                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.NotFound;
					response.StatusDescription = string.Format("Could not find item with the specified dtoKey ({0})", dtoKey);
					return null;
	            }
				catch (Telerik.OpenAccess.OpenAccessException)
				{
					OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.InternalServerError;
					response.StatusDescription = string.Format("Error occurred while trying to read item with dtoKey ({0})", dtoKey);
					return null;
				}
			}
	
			/// <summary>
			/// Returns a collection of key-value pairs with key the dto key and value the entity itself.
			/// </summary>
			/// <param name="startRowIndex">Start row index.</param>
			/// <param name="maximumRows">The amount of entities returned.</param>
			/// <returns></returns>
			protected IEnumerable<KeyValuePair<string, ConvenioTransaccionDto>> Read(int startRowIndex, int maximumRows)
			{
				return this.Service.Find(startRowIndex, maximumRows)
					.ToDictionary(r => r.DtoKey).AsEnumerable();
			}
	
			/// <summary>
			/// Updates the conveniotransaccion with the specified dto key in the database. 
			/// A response status code of BadRequest (400) will be the result if the dto key parameter does not match the entity's dto key.
			/// A response status code of InternalServerError (500) will be the result if an error occur.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <param name="conveniotransaccion">The dto that holds the data to be updated.</param>
			/// <returns></returns>
			protected void Update(string dtoKey, ConvenioTransaccionDto conveniotransaccion)
			{
				try
				{
					if (dtoKey != conveniotransaccion.DtoKey && dtoKey != conveniotransaccion.Id.ToString(string.Empty, CultureInfo.InvariantCulture))
					{
						OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
						response.StatusCode = HttpStatusCode.BadRequest;
						response.StatusDescription = string.Format("Mismatch between provided dtoKey (Value: {0}) and the identity key of the payload", dtoKey);
						return;
					}
	
					this.Service.Update(conveniotransaccion);
	            
					this.UnitOfWork.SaveChanges();
				}
				catch (Telerik.OpenAccess.Exceptions.NoSuchObjectException)
	            {
					OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.NotFound;
					response.StatusDescription = string.Format("Could not find item with the specified dtoKey ({0})", dtoKey);
					return;
	            }
				catch (OpenAccessException)
				{
					OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.InternalServerError;
					response.StatusDescription = string.Format("Error occurred while trying to load item with dtoKey = {0}", dtoKey);
					return;
				}
			}
	
			/// <summary>
			/// Deletes the conveniotransaccion with the specified dto key from the database.
			/// A response status code of NotFound (404) will be the result if the item does not exist.
			/// A response status code of InternalServerError (500) will be the result if an error occur.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <returns>True if the delete operation succeed. Otherwise return False.</returns>
			protected bool Delete(string dtoKey)
			{
				try
				{
					ConvenioTransaccionDto conveniotransaccion = Read(dtoKey);
					if (conveniotransaccion == null)
					{
						OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
						response.StatusCode = HttpStatusCode.NotFound;
						response.StatusDescription = string.Format("Could not find item with the specified dtoKey ({0})", dtoKey);
						return false;
					}
	
					this.Service.Delete(conveniotransaccion);
					this.UnitOfWork.SaveChanges();
	            
					return true;
				}
				catch (OpenAccessException)
				{
					OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
					response.StatusCode = HttpStatusCode.InternalServerError;
					response.StatusDescription = string.Format("Error occurred while trying to load item with dtoKey = {0}", dtoKey);
					return false;
				}
			}
	
			/// <summary>
			/// Returns the conveniotransaccion count.
			/// </summary>
			/// <returns></returns>
			protected int Count()
			{
				return this.Service.Count();
			}
	
	
			/// <summary>
			/// Handles adding of conveniotransaccion with a specified URI template and return a wrapped item info object.
			/// </summary>
			/// <param name="conveniotransaccion">The dto that holds the data to be added.</param>
			/// <param name="template">The URI template to be used.</param>
			/// <returns></returns>
			protected ItemInfo<ConvenioTransaccionDto> HandleCreateConvenioTransaccion(ConvenioTransaccionDto conveniotransaccion, UriTemplate template)
			{
				ConvenioTransaccionDto addedConvenioTransaccion = this.Create(conveniotransaccion);
	
				WebOperationContext.Current.OutgoingResponse.SetStatusAsCreated(GetItemLink(addedConvenioTransaccion.DtoKey, template));
				ItemInfo<ConvenioTransaccionDto> itemInfo = new ItemInfo<ConvenioTransaccionDto>();
				itemInfo.Item = addedConvenioTransaccion;
				itemInfo.EditLink = GetItemLink(addedConvenioTransaccion.DtoKey, template);
	
				return itemInfo;
			}
	
			/// <summary>
			/// Handles reading of conveniotransaccion with a specified dto key and return a dto.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <returns></returns>
			protected ConvenioTransaccionDto HandleReadConvenioTransaccion(string dtoKey)
			{
				return this.Read(dtoKey);
			}
	
			/// <summary>
			/// Handles reading of ConvenioTransaccions with a specified URI template and return a wrapped item info object list.
			/// </summary>
			/// <param name="template">The URI template to be used.</param>
			/// <returns></returns>
			protected ItemInfoList<ConvenioTransaccionDto> HandleReadConvenioTransaccions(UriTemplate template)
			{
				int startIndex = 0;
				int maxEntities = this.Count();
	
				string pageQueryParam = null;
	
				if (WebOperationContext.Current.IncomingRequest.UriTemplateMatch != null)
				{
					pageQueryParam = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["page"];
				}
	
				Uri nextPageLink = null, previousPageLink = null;
	
				if (pageQueryParam != null)
				{
					int pageNumber = 1;
	
					if (!string.IsNullOrEmpty(pageQueryParam) &&
						!int.TryParse(pageQueryParam, out pageNumber))
					{
						OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
						response.StatusCode = HttpStatusCode.BadRequest;
						response.StatusDescription = "Could not parse query parameters.";
						return null;
					}
	
					if (pageNumber < 1)
					{
						OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
						response.StatusCode = HttpStatusCode.BadRequest;
						response.StatusDescription = "Page number query parameter should be non-negative.";
						return null;
					}
	
					if (this.ConvenioTransaccionsPerPageCount > maxEntities)
					{
						this.ConvenioTransaccionsPerPageCount = maxEntities;
					}
	
					bool hasMoreEntries = pageNumber * this.ConvenioTransaccionsPerPageCount < maxEntities;
	
					if (pageNumber > 0)
					{
						previousPageLink = GetPreviousPageLink(template, pageNumber - 1);
					}
	
					if (hasMoreEntries)
					{
						nextPageLink = GetNextPageLink(template, pageNumber + 1);
					}
	
					pageNumber--;
	
					startIndex = pageNumber * this.ConvenioTransaccionsPerPageCount;
				}
	
				IEnumerable<KeyValuePair<string, ConvenioTransaccionDto>> items =
					this.Read(
						startIndex,
						(pageQueryParam != null) ? this.ConvenioTransaccionsPerPageCount : maxEntities);
	
				if (items == null)
				{
					items = new List<KeyValuePair<string, ConvenioTransaccionDto>>();
	
					return null;
				}
				else
				{
					List<ItemInfo<ConvenioTransaccionDto>> itemsInfo = new List<ItemInfo<ConvenioTransaccionDto>>();
	
					foreach (KeyValuePair<string, ConvenioTransaccionDto> item in items)
					{
						ItemInfo<ConvenioTransaccionDto> itemInfo = new ItemInfo<ConvenioTransaccionDto>();
						itemInfo.Item = item.Value;
						itemInfo.EditLink = GetItemLink(item.Key, template);
						itemsInfo.Add(itemInfo);
					}
	
					return new ItemInfoList<ConvenioTransaccionDto>(itemsInfo, nextPageLink, previousPageLink);
				}
			}
	
			/// <summary>
			/// Handles updating of conveniotransaccion with a specified dto key and return a the same dto with updated dto key property.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			/// <param name="conveniotransaccion">The dto that holds the data to be updated.</param>
			/// <returns></returns>
			protected ConvenioTransaccionDto HandleUpdateConvenioTransaccion(string dtoKey, ConvenioTransaccionDto conveniotransaccion)
			{
				this.Update(dtoKey, conveniotransaccion);
	
				return conveniotransaccion;
			}
	
			/// <summary>
			/// Handles deleting of conveniotransaccion with a specified dto key.
			/// </summary>
			/// <param name="dtoKey">The specified dto key value.</param>
			protected void HandleDeleteConvenioTransaccion(string dtoKey)
			{
				this.Delete(dtoKey);
			}
	
	
			/// <summary>
			/// Gets a link to the previous page.
			/// </summary>
			/// <param name="template">The URI template to be used.</param>
			/// <param name="pageNumber">The current page number.</param>
			/// <returns>A link to the previous page.</returns>
			protected Uri GetPreviousPageLink(UriTemplate template, int pageNumber)
			{
				Uri uri;
				if (pageNumber == 0)
				{
					uri = itemsUriTemplate.BindByPosition(
						WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri,
						new string[] { });
				}
				else
				{
					uri = pagedItemsUriTemplate.BindByPosition(
						WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri,
						new string[] { pageNumber.ToString(CultureInfo.InvariantCulture) });
				}
	
				return uri;
			}
	
				/// <summary>
				/// Gets a link to the next page.
				/// </summary>
				/// <param name="template">The URI template to be used.</param>
				/// <param name="pageNumber">The current page number.</param>
				/// <returns>A link to the next page.</returns>
				protected Uri GetNextPageLink(UriTemplate template, int pageNumber)
				{
					return pagedItemsUriTemplate.BindByPosition(WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri,
																new string[] { pageNumber.ToString(CultureInfo.InvariantCulture) });
				}
	
			#endregion
	
			/// <summary>
			/// ConvenioTransaccionsCollectionService finalizer.
			/// </summary>
			~ConvenioTransaccionsCollectionService()
			{
				IDisposable context = this.unitOfWork as IDisposable;
				if(context != null)
				{
					context.Dispose();
				}
			}
		}
}
#pragma warning restore 1591
