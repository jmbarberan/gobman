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
// using RestItemInfo.tt template
    
namespace IntelligobMovil
{
	using System.ServiceModel;
	using System.ServiceModel.Web;
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// Item info class handler. Used for wrapping and transporting an object of type TItem.
	/// </summary>
	/// <typeparam name="TItem">The type of the item.</typeparam>
	[DataContract(Name = "ItemInfo", Namespace = "")]
	public class ItemInfo<TItem> where TItem : class
	{
	    #region Private Fields
	
	    /// <summary>
	    /// The edit link of the wrapped object.
	    /// </summary>
	    private Uri _editLink;
	
	    /// <summary>
	    /// The wrapped object.
	    /// </summary>
	    private TItem _item;
	    
	    #endregion
	
	    #region Public Properties
	
	    /// <summary>
	    /// Gets or sets the edit link of the wrapped object.
	    /// </summary>
	    /// <value>The edit link.</value>
	    [DataMember]
	    public Uri EditLink
	    {
	        get
	        {
	            return this._editLink;
	        }
	        set
	        {
	            this._editLink = value;
	        }
	    }
	
	    /// <summary>
	    /// Gets or sets the wrapped object.
	    /// </summary>
	    /// <value>The wrapped object that is transported.</value>
	    [DataMember]
	    public TItem Item
	    {
	
	        get
	        {
	            return this._item;
	        }
	        set
	        {
	            this._item = value;
	        }
	    }
	
	    #endregion
	}
}
#pragma warning restore 1591
