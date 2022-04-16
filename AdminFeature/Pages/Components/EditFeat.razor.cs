using Microsoft.AspNetCore.Components;

namespace AdminFeature.Pages.Components;

public partial class EditFeat
{
    [Parameter]
    public EventCallback Delete { get; set; }

    [Parameter]
    public EventCallback<string> Add { get; set; }

    private string _newName;
    private bool _editButtonStatus;

    private void EditButtonStatus()
    {
        _editButtonStatus = !_editButtonStatus;
    }

    private async Task Edit()
    {
        await Add.InvokeAsync(_newName);

        _ = Delete.InvokeAsync();
        EditButtonStatus();
    }
}
