<#
.SYNOPSIS
Test Kubernetes stuff
#>
function Test-AzureRmDevSpacesController
{
    # Setup
    $resourceGroupName = Get-RandomResourceGroupName
    $kubeClusterName = Get-RandomClusterName
	$devSpacesName = Get-DevSpacesControllerName
	
	$devSpaceController = New-AzureRmDevSpacesController -ResourceGroupName $resourceGroupName -Name $devSpacesName -TargetClusterName $kubeClusterName -TargetResourceGroupName $resourceGroupName

	Assert-AreEqual $devSpacesName $devSpaceController.Name
	Assert-AreEqual $resourceGroupName $devSpaceController.ResourceGroupName
	Assert-AreEqual "Succeeded" $devSpaceController.ProvisioningState
	Assert-AreEqual "eastus" $devSpaceController.Location

	$devSpaceControllerUpdated = $devSpaceController | Update-AzureRmDevSpacesController -Tag @{ apple="mango"}

	Assert-AreEqual $devSpacesName $devSpaceControllerUpdated.Name
	Assert-AreEqual $resourceGroupName $devSpaceControllerUpdated.ResourceGroupName
	Assert-AreEqual "Succeeded" $devSpaceControllerUpdated.ProvisioningState

	$devSpaceController = Get-AzureRmDevSpacesController -ResourceGroupName $resourceGroupName -Name $devSpacesName

	Assert-AreEqual $devSpacesName $devSpaceController.Name
	Assert-AreEqual $resourceGroupName $devSpaceController.ResourceGroupName
	Assert-AreEqual "Succeeded" $devSpaceController.ProvisioningState
	Assert-AreEqual "eastus" $devSpaceController.Location

	$deletedAzureRmDevSpace = $devSpaceController | Remove-AzureRmDevSpacesController -PassThru
	Assert-AreEqual "True" $deletedAzureRmDevSpace
}