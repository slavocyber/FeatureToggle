using Blazored.Modal;
using Blazored.Modal.Services;
using Feature.Shared;
using Microsoft.AspNetCore.Components;

namespace Feature.Client.Pages;

public partial class EditFeature
{
    [CascadingParameter]
    private BlazoredModalInstance? ModalInstance { get; set; }

    [Parameter]
    public Action? Confirm { get; set; }

    [Parameter]
    public FeatureItem? Feature { get; set; }

    private void SaveNewFeature()
    {
        Confirm();

        _ = ModalInstance!.CloseAsync(ModalResult.Ok(Feature!));
    }
}
