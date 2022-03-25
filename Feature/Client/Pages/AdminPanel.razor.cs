using Blazored.Modal;
using Blazored.Modal.Services;
using Feature.Client.Common;
using Feature.Shared;
using Microsoft.AspNetCore.Components;

namespace Feature.Client.Pages;

public partial class AdminPanel
{
    [CascadingParameter]
    public IModalService? Modal { get; set; }

    private List<FeatureItem>? _features;
    private string? _nameNewFeture;

    protected override async Task OnInitializedAsync()
    {
        _features = await _featureMaster.GetFeatures();
    }

    private void OnOffButton(FeatureItem item)
    {
        _ = _featureMaster.EditStatus(item.Name);
        _ = UpdateList();
    }

    private void Add(string newFeat)
    {
        _ = _featureMaster.Add(newFeat);
        _ = UpdateList();
    }

    private void Delete(string featName)
    {
        if (_featureMaster.Remove(featName) == ErrorCode.JsonUpdated)
        {
            _ = UpdateList();
            return;
        }
    }

    private void ShowEditFeature(FeatureItem featureItem)
    {
        var parameters = new ModalParameters();

        parameters.Add(nameof(EditFeature.Feature), featureItem);
        parameters.Add(nameof(EditFeature.Confirm), () =>
        {
            _ = _featureMaster.UpdateFeatureList(_features);
            StateHasChanged();
        });

        _ = Modal!.Show<EditFeature>("Edit Feature", parameters);
    }

    private async Task UpdateList()
    {
        _features = await _featureMaster.GetFeatures();
    }
}
