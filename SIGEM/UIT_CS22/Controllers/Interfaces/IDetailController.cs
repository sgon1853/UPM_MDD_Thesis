// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Presentation;

namespace SIGEM.Client.Controllers
{
	/// <summary>
	/// Interface with the profile of a Deatil in any Master-Detail
	/// Instance, Population and Master-Deatil must satisfy this interface.
	/// </summary>
	public interface IDetailController
	{
        #region Properties
        /// <summary>
        /// Exchange information
        /// </summary>
        ExchangeInfo ExchangeInformation { get; set;}
        /// <summary>
        /// Detail context
        /// </summary>
        IUContext Context { get; set;}
        /// <summary>
        /// Scenario Id
        /// </summary>
        string IdXML { get;}
        /// <summary>
        /// Scenario Alias
        /// </summary>
        string Alias { get;}
        /// <summary>
        /// Scenario alias as Detail of a Master-Detail
        /// </summary>
        string DetailAlias { get; set;}
        /// <summary>
        /// Id of the Scenario alias as Detail
        /// </summary>
        string IdXMLDetailAlias { get; set;}
        /// <summary>
        /// Graphical container of the detail.
        /// </summary>
        ITriggerPresentation DetailContainer { get; set;}
        #endregion Properties

        #region Events
        /// <summary>
        /// Occurs when the Master must be refreshed.
        /// </summary>
        event EventHandler<RefreshRequiredMasterEventArgs> RefreshMasterRequired;
        /// <summary>
        /// Occurs when the Detail must be refreshed.
        /// </summary>
        event EventHandler<EventArgs> RefreshDetailRequired;
        /// <summary>
        /// Occurs when main scenario must be closed
        /// </summary>
        event EventHandler<EventArgs> CloseMainScenario;
        #endregion Events

        #region Methods
        /// <summary>
        /// Canfigures the Detail using the context information
        /// </summary>
        /// <param name="context"></param>
        void ConfigureByContext(IUContext context);
        /// Udpate the shown data
        /// </summary>
        /// <param name="refresh"></param>
        void UpdateData(bool refresh);
        /// <summary>
        /// Check if there is any change pending in the Display Set viewer.
        /// Returns False if the user wants to cancel the action
        /// </summary>
        /// <returns></returns>
        bool CheckPendingChanges(bool searchChangesInThisDS, bool searchChangesInAssociatedSIU);
        /// <summary>
        /// Initialize the controller
        /// </summary>
        void Initialize();
        /// <summary>
        /// Apply multilanguage
        /// </summary>
        void ApplyMultilanguage();
        /// <summary>
        /// Discard existing data and query again
        /// </summary>
        void Refresh();
        /// <summary>
        /// Returns true if the detail is active.
        /// </summary>
        bool IsActiveDetail();
        #endregion Methods

	}
}



