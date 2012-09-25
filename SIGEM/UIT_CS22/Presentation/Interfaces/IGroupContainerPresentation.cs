// v3.8.4.5.b
using System;

namespace SIGEM.Client.Presentation
{
	/// <summary>
	/// Abstracts the behaviour of a common label control
	/// in the InteractionToolkit layer.
	/// </summary>
	public interface IGroupContainerPresentation : IPresentation
	{
		void AssignGroupId(int groupNumber, string groupId);
		void HideAllGroups();
		void Initialize();
		void SetGroupVisible(string groupId);
	}
}
